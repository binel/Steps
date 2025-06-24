
namespace Steps.Api;

/// <summary>
/// Entrypoint
/// </summary>
public class Program {
    /// <summary>
    /// Entrypoint
    /// </summary>
    /// <param name="args">The only accepted argument is a database connection string</param>
    public static void Main(string[] args) {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        _ = builder.Services.AddAuthorization();
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
                _ = policy.WithOrigins("http://localhost:3000") // local setup
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                _ = policy.WithOrigins("http://steps.homelab.io") // k3s setup 
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        _ = builder.WebHost.ConfigureKestrel(serverOptions => {
            if (builder.Environment.IsDevelopment()) {
                serverOptions.ListenAnyIP(3001);
            }
            else {
                serverOptions.ListenAnyIP(80);
            }
        });

        WebApplication app = builder.Build();

        if (!app.Environment.IsDevelopment()) {
            _ = app.UsePathBase("/api"); // for k8s setup 
        }

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
