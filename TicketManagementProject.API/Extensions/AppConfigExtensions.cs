using TicketManagementProject.API.Settings;

namespace TicketManagementProject.API.Extensions
{
    public static class AppConfigExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorClient",
                    policy =>
                    {
                        policy.WithOrigins(config["AppSettings:Client_URL"]!) // Blazor port
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            return services;
        }
        public static WebApplication AddCors(this WebApplication app)
        {
            app.UseCors("AllowBlazorClient");
            return app;
        }

        public static IServiceCollection AddAppConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDbSettings>(
            config.GetSection(nameof(MongoDbSettings)));
            return services;
        }
    }
}
