namespace JourneyApp.Infrastructure.Extensions.Startup
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder builder)
        {
            builder.UseSwagger();

            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: "JourneyApp v1");
            });
            return builder;
        }
        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder builder)
        {
            builder.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("https://localhost:4200"));

            return builder;
        }
    }
}
