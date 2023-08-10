using JourneyApp.Infrastructure.Extensions.Startup;
using JourneyApp.Journey.Storages;
using JourneyApp.Journey.TokenManagement;
using StackExchange.Redis;

namespace JourneyApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration) =>
            this.Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllerWithFilters();
            services.AddLogging();
            services.AddDbContext<StoragJourney>();
            services.AddEntrance();
            services.AddIdentityServices(Configuration);
            services.AddTransient<TokenManagerMiddleware>();
            services.AddServices();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedRedisCache(r =>
            {
                r.Configuration = Configuration["redis:connectionString"];
            });

            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder builder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            }

            builder.UseSwaggerService();
            builder.UseHttpsRedirection();
            builder.UseRouting();
            builder.UseCustomCors();
            builder.UseAuthentication();
            builder.UseAuthorization();
            builder.UseCors("MyPolicy");
            builder.UseMiddleware<TokenManagerMiddleware>();
            builder.UseEndpoints(endpoints => endpoints.MapControllers());
        }

    }
}