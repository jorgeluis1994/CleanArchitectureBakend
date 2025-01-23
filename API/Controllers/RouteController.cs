using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly string _dicomDirectory = @"C:\Users\jorge\Downloads\Anonymized_20250120\series-00000";


        [HttpGet("files")]
        public IActionResult GetDicomFiles()
        {
            if (!Directory.Exists(_dicomDirectory))
            {
                return NotFound("El directorio no existe.");
            }

            var files = Directory.GetFiles(_dicomDirectory, "*.dcm");

            // Generar las URLs y codificar el nombre de los archivos
            var fileUrls = files.Select(file => new
            {
                Name = Path.GetFileName(file),
                Url = $"{Request.Scheme}://{Request.Host}/api/Route/files/{Uri.EscapeDataString(Path.GetFileName(file))}"
            }).ToList();

            return Ok(fileUrls);
        }


        [HttpGet("files/{filename}")]
        public IActionResult GetDicomFile(string filename)
        {
            var filePath = Path.Combine(_dicomDirectory, filename);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("El archivo DICOM no existe.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/dicom", filename);
        }

        [HttpGet]
        public ActionResult GetRoutes()
        {
            try
            {
                var routes = new List<RoutesModels>
                {
                    new RoutesModels
                    {
                        Id = 1,
                        Name = "Route 1",
                        StartLocation = "Location 1",
                        EndLocation = "Location 2",
                        Distance = 10.5
                    },
                    new RoutesModels
                    {
                        Id = 2,
                        Name = "Route 2",
                        StartLocation = "Location 2",
                        EndLocation = "Location 3",
                        Distance = 20.5
                    },
                    new RoutesModels
                    {
                        Id = 3,
                        Name = "Route 3",
                        StartLocation = "Location 3",
                        EndLocation = "Location 4",
                        Distance = 30.5
                    }

                };

                return Ok(routes);


            }
            catch (System.Exception)
            {

                throw;
            }

        }
        // Add your actions here
    }
}