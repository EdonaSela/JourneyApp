using JourneyApp.Models.Entities.Roles;
using JourneyApp.Models.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using JourneyApp.Models.Entities.journeys;
using static System.Net.Mime.MediaTypeNames;
using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Service.Foundations.Journeys;
using JourneyApp.Models.DTOs.Journeys;

namespace JourneyApp.Journey.Storages
{
    public partial class StoragJourney : IdentityDbContext<User, Role, Guid>, IStorageJourney
    {
        private readonly IConfiguration configuration;


        public StoragJourney(IConfiguration configuration)
        {
            this.configuration = configuration;

            //this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SetIdentityTableNames(builder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration.GetConnectionString(name: "DefaultConnection");

            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseNpgsql("Server=host.docker.internal;Port=5432;Database=JourneyDb;User Id=postgres;Password=edona;");
                optionsBuilder.UseNpgsql(connectionString);

            }
        }

        private static void SetIdentityTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(name: "Users");
            modelBuilder.Entity<Role>().ToTable(name: "Roles");
        }

        public DbSet<JourneyDto> journeys { get; set; }

        public async ValueTask<JourneyDto> InsertjourneyAsync(JourneyDto journey)
        {
            using var ident = new StoragJourney(this.configuration);
            if (journey != null && journey.distance > 20)
            {
                journey.DailyGoal = true;
            }
            else
            {
                journey.DailyGoal = false;
            }

            EntityEntry<JourneyDto> journeyEntityEntry = await ident.journeys.AddAsync(journey);
            await ident.SaveChangesAsync();

            return journeyEntityEntry.Entity;
        }
        private async ValueTask<JourneyDto> SelectJourneyByIdAsync(Guid Id)
        {
            using var ident = new StoragJourney(this.configuration);
            ident.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await ident.journeys.FirstOrDefaultAsync(x => x.Id == Id);

        }
        public async ValueTask<Boolean>  DeletejourneyAsync(Guid Id)
        {
            using var ident = new StoragJourney(this.configuration);

            var journey = this.SelectJourneyByIdAsync(Id);
           if (journey !=null)
            {
                journey.Result.Invalidated = true;
                JourneyDto jrn = journey.Result;
                EntityEntry<JourneyDto> journeyEntityDelete = ident.journeys.Update(jrn);
                await ident.SaveChangesAsync();
                return true;
            }
            else {
                return false;
            }

            
        }
        public async ValueTask<Boolean> UpdateDailyAchievement(Guid UserId)
        {
            using var ident = new StoragJourney(this.configuration);
            var journey = await ident.journeys.Where(x => x.UserId == UserId).LastOrDefaultAsync();
            var journeys = await ident.journeys.Where(x => x.UserId == UserId && x.StartTime.Date == DateTime.Now.Date).Select(x => x.distance).ToListAsync();

            if (journeys.Sum() > 20)
            {
                journey.DailyGoal = true;
            }
           
            EntityEntry<JourneyDto> journeyEntityDelete = ident.journeys.Update(journey);
            await ident.SaveChangesAsync();

            return true;


        }
        


        public IQueryable<JourneyDto> SelectAlljourneys() => this.journeys;


        public async ValueTask<JourneyDto> SelectjourneyByIdAsync(Guid journeyId)
        {
            using var ident = new StoragJourney(this.configuration);
            ident.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await ident.journeys.FindAsync(journeyId);
        }

        public async ValueTask<List<JourneyDto>> SelectjourneyByUserIdAsync(Guid userId)
        {
            using var ident = new StoragJourney(this.configuration);
            ident.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await ident.journeys.Where(x => x.UserId == userId).ToListAsync();
        }
        public async ValueTask<List<JourneyDto>> FilterJourneys(FilterDto filter)
        {
            using var ident = new StoragJourney(this.configuration);
            ident.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await ident.journeys.Where(x => x.UserId == filter.UserId || x.ArrivalTime == filter.ArrivalDate
            || x.StartTime == filter.StartDate || x.transportationType.Id == filter.TransportationTypeId).ToListAsync();
        }

        public async ValueTask<List<JourneyRouteFilterDto>> MonthlyRoute(JourneyMonthlyRouteDto filter)
        {
            using var ident = new StoragJourney(this.configuration);
            ident.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            IQueryable<JourneyDto> stastic =  ident.journeys.Where(x => (x.UserId == filter.UserId || filter.UserId ==null) && 
            x.ArrivalTime.Year == filter.Year && x.ArrivalTime.Month == (int)filter.Month);
            return await stastic.GroupBy(x => x.UserId).Select(stat => new JourneyRouteFilterDto
            {
                UserId = stat.Key,
                journeyCount = stat.Count(),
                totalDistance = stat.Sum(x =>x.distance)
            }).ToListAsync();


        }





    }
}
