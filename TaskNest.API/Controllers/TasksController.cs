using Microsoft.AspNetCore.Mvc;           // Für Controller, Routing, HTTP-Antworten
using Microsoft.EntityFrameworkCore;      // Für EF Core-Funktionen wie ToListAsync()
using TaskNest.API.Data;                  // Zugriff auf den AppDbContext
using TaskNest.API.Models;                // Zugriff auf das TaskItem-Modell
using System.Collections.Generic;         // Für Listen
using System.Threading.Tasks;

namespace TaskNest.API.Controllers
{
    [ApiController]                   // Markiert: Dies ist ein Web-API-Controller
    [Route("api/[controller]")]       // Die Route ist /api/tasks  Sagt ASP.NET: „Leite alle Anfragen an /api/tasks an diese Klasse weiter!“
    public class TasksController : ControllerBase // Erbt von ControllerBase (ohne View-Logik)
    {
        private readonly AppDbContext _context;

        //Immer wenn ASP.NET eine Instanz von TasksController baut, gibt es den AppDbContext automatisch mit.
        public TasksController(AppDbContext context)
        {
            _context = context; // Der Kontext kommt “von außen” (DI-Container)!
        }


        // GET: api/tasks  --> tasks weil unsere Entität tasks heißt == Resource
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }


        // GET: api/tasks/id --> GET Anfrage um ein Task mit bestimmter ID zu erhalten
        [HttpGet("{id}")] // "{id}" Nimmt einen Parameter aus der URL
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id); // Sucht in der Datenbank nach der Aufgabe mit dieser ID
            if (task == null)
            {
                return NotFound(); // Wenn nicht gefunden → NotFound() (HTTP 404)
            }
            return task; // Sonst: Gibt die Aufgabe zurück
        }

        // Post: api/tasks --> Post Methode welches einen neuen Datensatz in die Datenbanktabelle einfüge
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(TaskItem newTask)
        {
            _context.Tasks.Add(newTask);          // Zur DB hinzufügen (aber noch nicht speichern!)
            await _context.SaveChangesAsync();    // Jetzt wirklich in die DB schreiben

            return CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, newTask); // HTTP 201, mit Link zur neuen Ressource
        }

        // PUT: api/tasks/1 [HttpPut("{id}")] public ActionResult UpdateTask(int id, TaskItem updatedTask)
        // Sucht eine Aufgabe per ID und aktualisiert deren Inhalt.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItem updatedTask)
        {
            if (id != updatedTask.Id) // Prüft, ob die ID aus der URL und aus dem gesendeten Objekt gleich ist.
            {
                return BadRequest();  // Fehler: ID stimmt nicht überein
            }

            _context.Entry(updatedTask).State = EntityState.Modified; // Markiert das Objekt als geändert
            try
            {
                await _context.SaveChangesAsync(); // Speichert Änderungen
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound(); // Falls die Aufgabe inzwischen gelöscht wurde
                }
                else
                {
                    throw; // Anderer Fehler: wird weitergereicht
                }
            }
            return NoContent(); // HTTP 204 (kein Inhalt, aber erfolgreich)
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id); // Private Hilfsmethode, die prüft, ob es die Aufgabe mit der angegebenen ID gibt.
        }

        // DELETE: api/tasks/1 [HttpDelete("{id}")] public ActionResult DeleteTask(int id)
        // Löscht die Aufgabe mit der entsprechenden ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id); // Sucht die Aufgabe nach ID.
            if (task == null)
            {
                return NotFound(); // Wenn es die Aufgabe nicht gibt
            }

            _context.Tasks.Remove(task); // Löscht die Aufgabe
            await _context.SaveChangesAsync(); // Speichert die Änderung
            return NoContent(); // Erfolg, kein Inhalt
        }
    }   

};