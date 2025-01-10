using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Usuario
{
    [BsonId]  // Esto indica que es el campo de clave primaria (_id)
    public ObjectId Id { get; set; }

    [BsonElement("nombre")]  // Mapea el campo "nombre" de MongoDB a la propiedad Nombre
    public string Nombre { get; set; }

    [BsonElement("correo")]  // Mapea el campo "correo" de MongoDB a la propiedad Correo
    public string Correo { get; set; }

    [BsonElement("password")]  // Mapea el campo "correo" de MongoDB a la propiedad Correo
    public string Password { get; set; }

    [BsonElement("edad")]  // Mapea el campo "edad" de MongoDB a la propiedad Edad
    public int Edad { get; set; }
    [BsonElement("direccion")]
    public Direccion Direccion { get; set; }  // Subdocumento
}

public class Direccion
{
    [BsonElement("calle")]  // Mapea el campo "calle" de MongoDB a la propiedad Calle
    public string Calle { get; set; }

    [BsonElement("ciudad")]  // Mapea el campo "ciudad" de MongoDB a la propiedad Ciudad
    public string Ciudad { get; set; }

    [BsonElement("pais")]  // Mapea el campo "pais" de MongoDB a la propiedad Pais
    public string Pais { get; set; }
}
