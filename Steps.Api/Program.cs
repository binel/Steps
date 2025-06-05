
namespace Steps.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // allow the database connection string to be passed in 
        if (args.Length == 1) {
            Console.WriteLine($"Using database connection override {args[0]}");
            builder.Services.AddSingleton<Database>((sp) => {
                return new Database(args[0]);
            });
        } else {
            builder.Services.AddSingleton<Database>();
        }
        builder.Services.AddSingleton<StepsRepository>();

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:3000") // temporary
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                policy.WithOrigins("http://homelab.io:3000") // docker compose setup
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                policy.WithOrigins("http://homelab.io") // k3s setup 
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.WebHost.ConfigureKestrel(serverOptions => {
            serverOptions.ListenAnyIP(80);
        });

        var app = builder.Build();

        // k3s base path, probably breaks docker compose setup
        app.UsePathBase("/api");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapControllers();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors();

        app.Run();
    }
}
