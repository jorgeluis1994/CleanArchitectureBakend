using API.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

using BCrypt.Net;

namespace MyApp.Namespace
{
    [ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMongoCollection<Usuario> _collection;

    public UserController(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<Usuario>("users");
    }

    // Acción GET
    [HttpGet]
     public async Task<List<Usuario>> GetUsers()
    {
        var response=await _collection.Find(_ => true).ToListAsync(); // Obtiene todos los usuarios

        return response;
    }

    // POST: api/user
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] Usuario usuario)
    {
        if (usuario == null)
        {
            return BadRequest("El usuario no puede ser nulo.");
        }

        // Cifra la contraseña
        usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);


        // Inserta el nuevo usuario en la colección
        await _collection.InsertOneAsync(usuario);

        // Devuelve un código de estado 201 (Creado) con el usuario recién creado
        return CreatedAtAction(nameof(GetUserById), new { id = usuario.Id }, usuario);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUserById(string id)
    {
        // Convertir el string a ObjectId
        if (!ObjectId.TryParse(id, out var objectId))
        {
            return BadRequest("El formato del ID es inválido.");
        }

        var usuario = await _collection.Find(u => u.Id == objectId).FirstOrDefaultAsync();

        if (usuario == null)
        {
            return NotFound(); // Si no se encuentra el usuario, devolver 404.
        }

        return Ok(usuario); // Si se encuentra, devolver 200 OK con el usuario.
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        // Buscar al usuario por correo (suponiendo que el correo es único)
        var usuario = await _collection
            .Find(u => u.Correo == loginRequest.Correo)
            .FirstOrDefaultAsync();

        // Si el usuario no existe o el correo no coincide, retorna un error
        if (usuario == null)
        {
            return Unauthorized("Correo o contraseña incorrectos.");
        }

        // Si el usuario existe, devuelve los detalles del usuario
        return Ok(new
        {
            id = usuario.Id,
            nombre = usuario.Nombre,
            correo = usuario.Correo,
            edad = usuario.Edad,
            direccion = usuario.Direccion
        });
    }



}
}

