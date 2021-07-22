using Microsoft.AspNetCore.Mvc;

namespace WeatherFront.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
