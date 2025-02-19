namespace HRM_Management.API.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(config =>
            {
                config.AddDefaultPolicy(options =>
                {
                    options.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }
    }
}
