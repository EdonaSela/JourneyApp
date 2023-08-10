using JourneyApp.Models.DTOs.Register;
using JourneyApp.Models.DTOs.Roles;
using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Models.DTOs.Users;
using JourneyApp.Models.Entities.Roles;
using JourneyApp.Models.Entities.journeys;
using JourneyApp.Models.Entities.Users;

namespace JourneyApp.Infrastructure.Mappings
{
    public static class ModelMapping
    {

        public static User ToEntity(this UserDto userDto)
        {
            return new User
            {
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName
             
            };
        }

        public static User ToEntity(this RegisterDto registerDto)
        {
            return new User
            {
                UserName = registerDto.UserName.ToLower(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
            };
        }

        public static Role ToEntity(this RolesDto roleDto)
        {
            return new Role
            {
                Name = roleDto.Name
            };
        }

        public static Models.Entities.journeys.Journey_ ToEntity(this JourneyDto journeyDto)
        {
            return new Models.Entities.journeys.Journey_
            {
                StartLocation = journeyDto.StartLocation,
                StartTime = journeyDto.StartTime,
                ArrivalLocation= journeyDto.ArrivalLocation,
                ArrivalTime = journeyDto.ArrivalTime,
                transportationType = journeyDto.transportationType,
                distance = journeyDto.distance,
                UserId = journeyDto.UserId
            };
        }

        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static UserDto ToDto(this User user, string token)
        {
            return new UserDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };
        }

        public static RolesDto ToDto(this Role role)
        {
            return new RolesDto
            {
                Name = role.Name
            };
        }

        public static JourneyDto ToDto(this Models.Entities.journeys.Journey_ journey)
        {
            return new JourneyDto
            {
                StartLocation = journey.StartLocation,
                StartTime = journey.StartTime,
                ArrivalLocation = journey.ArrivalLocation,
                ArrivalTime = journey.ArrivalTime,
                transportationType = journey.transportationType,
                distance = journey.distance,
                UserId = journey.UserId
            };
        }
    }
}
