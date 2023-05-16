using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserDataService.UserService;

namespace VehicleService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
   private userService _service;
    private readonly IConfiguration _config;

    private readonly string _imagePath;
    public WeatherForecastController(ILogger<WeatherForecastController> logger,IConfiguration config, userService service)
    {
        _logger = logger;
        _service = service; 
        _config = config; 
        _imagePath =  "/Users/placeholder/Desktop/billed";//config["imagepath"];
    }


    [Authorize]
    [HttpGet("getCars")]
    public List<Vehicle> GetVehicles()
    {
        return _service.GetAsync();
    }
    [Authorize]
    [HttpPost("postCar")]
    public void PostVehicle([FromBody]Vehicle newVehicle)
    {
        _service.PostVehicle(newVehicle);
    }
[Authorize]    
[HttpPost("uploadpic"), DisableRequestSizeLimit]
public IActionResult UploadImage()
{
List<Uri> images = new List<Uri>();
try
{
foreach (var formFile in Request.Form.Files)
{
// Validate file type and size

if (formFile.ContentType != "image/jpeg" && formFile.ContentType !=
"image/png")
{
return BadRequest($"Invalid file type for file{formFile.FileName}. Only JPEG and PNG files are allowed.");
}
if (formFile.Length > 1048576) // 1MB
{
return BadRequest($"File {formFile.FileName} is too large. Maximumfile size is 1MB.");
}
if (formFile.Length > 0)
{
var fileName = "image-" + Guid.NewGuid().ToString() + ".jpg";
var fullPath = _imagePath + Path.DirectorySeparatorChar +
fileName;
using (var stream = new FileStream(fullPath,FileMode.Create))
{
formFile.CopyTo(stream);
}
var imageURI = new Uri(fileName, UriKind.RelativeOrAbsolute);
images.Add(imageURI);
}
else
{
return BadRequest("Empty file submited.");
}
}
}
catch (Exception ex)
{
return StatusCode(500, $"Internal server error.");
}
return Ok(images);
}
[Authorize]
[HttpGet("listImages")]
public IActionResult ListImages()
{
List<Uri> images = new List<Uri>();
if(Directory.Exists(_imagePath))
{
string [] fileEntries = Directory.GetFiles(_imagePath);
foreach( var file in fileEntries)
{
var imageURI = new Uri(file, UriKind.RelativeOrAbsolute);
images.Add(imageURI);
}
}
return Ok(images);
}
}
