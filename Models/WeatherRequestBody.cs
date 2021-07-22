using System.ComponentModel.DataAnnotations;

namespace WeatherFront.Models
{
    public class WeatherRequestBody
    {
        [Required]
        public string City { get; set; }


        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Country { get; set; }

        [Required]
        public string ApiKey { get; set; }
    }
}
