using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Postcards.Worker.Data;

namespace Postcards.Worker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<HubConnection>(
            _ => new HubConnectionBuilder()
            .WithUrl( builder.Configuration["mainServiceUrl"] + "/hubs/postcard")
            .Build());

        builder.Services.AddTransient<PostcardRepository>();
        builder.Services.AddTransient<IUpdateNotifier, SignalRUpdateNotifier>();
        builder.Services.AddTransient<IPostcardGenerator, DummyPostcardGenerator>();
        
        // // Read the OpenAI API key from a file
        // string? apiKey = null;
        // try
        // {
        //     apiKey  = File.ReadAllText("OpenAIApiKey.txt");
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine("API key file not found. Please create a file named 'OpenAIApiKey.txt' in the root directory of the project and add your OpenAI API key to it.");
        //     Console.WriteLine(e.Message);
        //     Environment.Exit(1);
        // }
        // // check if the API key is not empty
        // if (string.IsNullOrEmpty(apiKey))
        // {
        //     Console.WriteLine("API key is empty. Please add your OpenAI API key to the 'OpenAIApiKey.txt' file.");
        //     Environment.Exit(1);
        // }
        // // Register the DallEPostcardGenerator as a service
        // builder.Services.AddTransient<IPostcardGenerator>(serviceProvider =>
        // {
        //     var logger = serviceProvider.GetRequiredService<ILogger<DallEPostcardGenerator>>();
        //     return new DallEPostcardGenerator(logger, apiKey);
        // });
        
        builder.Services.AddHostedService<PostcardGeneratorWorkerService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}