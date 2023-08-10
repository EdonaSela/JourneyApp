using JourneyApp.Models.Entities.journeys;

namespace JourneyApp.Models.DTOs.journeys
{
    public class JourneyDto
    {
        public Guid Id { get; set; }
        public PositionDto StartLocation { get; set; }

        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public PositionDto ArrivalLocation { get; set; }
        public TransportationTypeDto transportationType { get; set; }
        public double distance { get; set; }

        public Guid UserId { get; set; }
        public Boolean DailyGoal { get; set; }
        public Boolean Invalidated { get; set; }

    }

    
}
