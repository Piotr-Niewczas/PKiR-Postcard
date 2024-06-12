namespace Postcards.Generator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddTransient<IPostcardGenerator>(serviceProvider => new LandscapePostcardGenerator(
            builder.Configuration["urls"] ?? throw new InvalidOperationException("urls not found"),
            builder.Configuration["mainServiceUrl"] ??
            throw new InvalidOperationException("mainServiceUrl not found in configuration")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles(); // For the wwwroot/img folder
        app.UseAuthorization();

        app.MapGet("/generate", async (string baseImgName, string text, int requestId) =>
        {
            var generator = app.Services.GetRequiredService<IPostcardGenerator>();
            var result = await generator.GeneratePostcard(baseImgName, text, requestId);
            return result.Match(
                success => Results.Ok(result.Value),
                error => Results.BadRequest(error.ToString()));
        });

        app.Run();
    }
}