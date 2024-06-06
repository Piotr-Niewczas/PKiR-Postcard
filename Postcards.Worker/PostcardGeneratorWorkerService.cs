using Postcards.Worker.Data;

namespace Postcards.Worker;

public class PostcardGeneratorWorkerService(
    ILogger<PostcardGeneratorWorkerService> logger,
    PostcardRepository repository,
    IPostcardGenerator generator)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var postcards = await repository.GetEmptyPostcards(stoppingToken);
            logger.LogInformation("Found {Count} postcards to generate", postcards.Count);
            foreach (var postcard in postcards)
            {
                var generateResult = await generator.GeneratePostcard(postcard.Prompt);
                if (generateResult.IsError)
                {
                    logger.LogError("Failed to generate postcard {PostcardId} with prompt {Prompt}: {Error}",
                        postcard.Id, postcard.Prompt, generateResult.Errors.ToString());
                    continue;
                }

                var updateResult = await repository.UpdatePostcard(postcard.Id, generateResult.Value);
                if (updateResult.IsError)
                {
                    logger.LogError("Failed to update postcard {PostcardId}: {Error}", postcard.Id,
                        updateResult.Errors.ToString());
                    continue;
                }

                logger.LogInformation("Generated postcard {PostcardId} with prompt {Prompt}", postcard.Id,
                    postcard.Prompt);
            }

            logger.LogInformation("Waiting for 30 seconds before checking for more postcards");
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}