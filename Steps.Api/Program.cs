
namespace Steps.Api;

public class Program {
    public static void Main(string[] args) {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        _ = builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        _ = builder.Services.AddOpenApi();

        // allow the database connection string to be passed in 
        if (args.Length == 1) {
            Console.WriteLine($"Using database connection override {args[0]}");
            _ = builder.Services.AddSingleton((sp) => {
                return new Database(args[0]);
            });
        }
        else {
            _ = builder.Services.AddSingleton<Database>();
        }
        _ = builder.Services.AddSingleton<StepsRepository>();

        _ = builder.Services.AddControllers();

        _ = builder.Services.AddCors(options => {
            options.AddDefaultPolicy(policy => {
                _ = policy.WithOrigins("http://localhost:3000") // temporary
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                _ = policy.WithOrigins("http://homelab.io:3000") // docker compose setup
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                _ = policy.WithOrigins("http://homelab.io") // k3s setup 
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        _ = builder.WebHost.ConfigureKestrel(serverOptions => {
            serverOptions.ListenAnyIP(80);
        });

        WebApplication app = builder.Build();

        // k3s base path, probably breaks docker compose setup
        _ = app.UsePathBase("/api");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            _ = app.MapOpenApi();
        }

        _ = app.MapControllers();

        _ = app.UseHttpsRedirection();

        _ = app.UseAuthorization();

        _ = app.UseCors();

        app.Run();
    }
}
