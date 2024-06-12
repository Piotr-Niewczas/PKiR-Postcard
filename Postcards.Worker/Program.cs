using Microsoft.AspNetCore.SignalR.Client;
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
                .WithUrl(builder.Configuration["mainServiceUrl"] + "/hubs/postcard")
                .Build());

        builder.Services.AddTransient<PostcardRepository>();
        builder.Services.AddTransient<IUpdateNotifier, SignalRUpdateNotifier>();
        builder.Services.AddTransient<IPostcardGenerator>(serviceProvider => new RemotePostcardGenerator(
            builder.Configuration["generatorServiceUrl"] ??
            throw new InvalidOperationException("generatorServiceUrl not found in configuration"),
            builder.Configuration["mainServiceUrl"] ??
            throw new InvalidOperationException("mainServiceUrl not found in configuration")));


        builder.Services.AddHostedService<PostcardGeneratorWorkerService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.Run();
    }
}