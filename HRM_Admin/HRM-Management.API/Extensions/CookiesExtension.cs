namespace HRM_Management.API.Extensions
{
    public static class CookiesExtension
    {
        public static IServiceCollection ConfigureCookies(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            return services;
        }
    }
}
