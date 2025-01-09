using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Simulamos una base de datos con una lista en memoria
        private static List<string> users = new List<string> { "John Doe", "Jane Smith" };

        // Acción GET
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(users);  // Devuelve la lista de usuarios
        }
    }
}
