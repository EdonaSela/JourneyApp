namespace JourneyApp.Models.DTOs.Journeys
{
    public class FilterDto
    {

        public Guid? UserId { get; set; }
        public Guid? TransportationTypeId { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? ArrivalDate { get; set; }
    }
}
