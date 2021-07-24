namespace WeatherFront.Models
{
    public class WeatherState
    {
        public string City { get; init; }
        public string Country { get; set; }
        public double Temperature { get; init; }
        public string Description { get; init; }
    }
}
