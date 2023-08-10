namespace JourneyApp.Models.DTOs.Journeys
{
    public class JourneyRouteFilterDto
    {

        public Guid UserId { get; set; }
        public double totalDistance { get; set; }
        public double journeyCount { get; set; }
    }
}
