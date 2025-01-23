using API.Db;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Configura los servicios para permitir servir archivos estáticos
builder.Services.AddRouting();


// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Permite solicitudes de cualquier origen
               .AllowAnyMethod() // Permite cualquier método (GET, POST, PUT, DELETE)
               .AllowAnyHeader(); // Permite cualquier encabezado
    });
});



builder.Services.AddControllers(); 


// Configurar MongoDbSettings desde appsettings.json
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
// Configurar la conexi�n de MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();
// Aplicar CORS
app.UseCors("AllowAllOrigins");  // Aplica la política de CORS

// Configura la ruta estática desde la carpeta que mencionas
var dicomDirectory = @"C:\Users\jorge\Downloads\Anonymized_20250120\series-00000";

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dicomDirectory),
    RequestPath = "/dicom"  // Podrás acceder a los archivos a través de "/dicom/{nombre del archivo}"
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.MapControllers(); 

app.Run();

