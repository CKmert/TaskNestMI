// DB Context = Herzstück , es steuert welche Datenbank Tabelle es gibt und wie sie heißen --> 
// stellt eine Sitzung mit der Datenbank dar und kann verwendet werden, um Instanzen Ihrer Entitäten abzufragen und zu speichern
using Microsoft.EntityFrameworkCore;
using TaskNest.API.Models;


namespace TaskNest.API.Data
{
    public class AppDbContext : DbContext // Erbt und ist damit EF Core Kontext / Eigenschaften 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // Im Konstruktor werden die Konfigurationsoptionen übergeben (u.a. Connection String).
        { 
        
        }

        public DbSet<TaskItem> Tasks { get; set; }  // Die Zeile sagt EF Core -> Lege eine Tabelle namens Tasks an, deren Zeile TaskItemObjekte sind

    }
}
