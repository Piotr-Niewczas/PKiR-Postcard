namespace Postcards;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSignalR();
        builder.Services.AddSingleton<IPostcardRequestHandler>(
            new PostcardRequestHandler(builder.Configuration["grpcAddress"] ??
                                       throw new InvalidOperationException(
                                           "grpcAddress is not set in appsettings.json")));


        var app = builder.Build();

        app.UseCors("AllowAllOrigins");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // app.UseAuthorization();


        app.MapControllers();

        app.MapHub<PostcardHub>("hubs/postcard");

        app.Run();
    }
}