using JourneyApp.Models.Entities.journeys;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace JourneyApp.Models.Entities.Users
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public Journey_ journey { get; set; }
    }
}
