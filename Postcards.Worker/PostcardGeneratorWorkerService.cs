using Postcards.Models;
using Postcards.Worker.Data;

namespace Postcards.Worker;

public class PostcardGeneratorWorkerService(
    ILogger<PostcardGeneratorWorkerService> logger,
    PostcardRepository repository,
    IPostcardGenerator generator,
    IUpdateNotifier updateNotifier)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var postcards = await repository.GetEmptyPostcards(stoppingToken);
            List<Postcard> successfullyUpdated = [];
            logger.LogInformation("Found {Count} postcards to generate", postcards.Count);
            foreach (var postcard in postcards)
            {
                // Generate the postcard
                var generateResult = await generator.GeneratePostcard(postcard.Prompt);
                if (generateResult.IsError)
                {
                    logger.LogError("Failed to generate postcard {PostcardId} with prompt {Prompt}: {Error}",
                        postcard.Id, postcard.Prompt, generateResult.Errors.ToString());
                    continue;
                }

                // Update the postcard with the generated image URL
                var updateResult = await repository.UpdatePostcard(postcard.Id, generateResult.Value);
                if (updateResult.IsError)
                {
                    logger.LogError("Failed to update postcard {PostcardId}: {Error}", postcard.Id,
                        updateResult.Errors.ToString());
                    continue;
                }

                // Add the postcard to the list of successfully updated postcards
                successfullyUpdated.Add(updateResult.Value);

                logger.LogInformation("Generated postcard {PostcardId} with prompt {Prompt}", postcard.Id,
                    postcard.Prompt);
            }

            if (successfullyUpdated.Count > 0 )
            {
                // Notify core project about the updated postcards
                var notifyResult =
                    await updateNotifier.SendUpdateNotification(successfullyUpdated);
                if (notifyResult.IsError)
                {
                    logger.LogError("Failed to send update notification: {Error}", notifyResult.Errors.ToString());
                }
                else
                {
                    logger.LogInformation("Sent update notification for postcards {PostcardIds}",
                        string.Join(", ", successfullyUpdated.Select(p => p.Id)));
                }
            }
            
            logger.LogInformation("Waiting for 30 seconds before checking for more postcards");
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}