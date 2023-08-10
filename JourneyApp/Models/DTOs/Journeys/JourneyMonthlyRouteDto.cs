namespace JourneyApp.Models.DTOs.Journeys
{
    public class JourneyMonthlyRouteDto
    {
        public Guid? UserId { get; set; }
        public Months Month { get; set; }
        public int Year { get; set; } = DateTime.UtcNow.Year;
    }
    public enum Months
    {
        Jan =1,
        Feb = 2,
        Mar =3,
        Apr =4,
        May =5,
        Jun =6,
        Jul =7,
        Aug =8,
        Sep =9,
        Oct =10,
        Nov =11,
        Dec =12
    }
}
