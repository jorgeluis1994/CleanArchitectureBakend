using API.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MyApp.Namespace
{
    [ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMongoCollection<Object> _collection;

    public UserController(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<Object>("users");
    }

    // Acción GET
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _collection.Find(_ => true).ToListAsync();
        
        if (usuarios == null || usuarios.Count == 0)
        {
            return NotFound("No users found.");
        }
        
        return Ok(usuarios);
    }
}
}

