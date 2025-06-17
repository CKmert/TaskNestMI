using TaskNest.API.Data; // Modelle Integrieren
using Microsoft.EntityFrameworkCore; // EF Core Framework für Datenbankverwaltung, Bearbeitung etc..


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection für DbContext einrichten, sag ASP.NET Core, dass der AppDbContext mit der Connection-String
// DefaulConnection aus deiner appsettings.json verwendet werden soll. --> Nach Einrichtung MIGRATION ERSTELLEN  CODE FIRST!!

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// EF Core sucht nach einem Connection String mit genau dem Namen, den du in GetConnectionString() angibst.
//Gibt es den nicht, kann EF Core keinen DbContext bauen und wirft die von dir gepostete Fehlermeldung.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
