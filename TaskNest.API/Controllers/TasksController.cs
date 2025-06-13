
using Microsoft.AspNetCore.Mvc;         // Für API-Controller und Routing
using TaskNest.API.Models;              // Für das TaskItem-Modell
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;


namespace TaskNest.API.Controllers
{
    [ApiController]     // Sagt ASP.NET: Das ist ein API-Controller
    [Route("api/[controller]")]     // Der Endpunkt ist dann: api/tasks
  
    public class TasksController : ControllerBase // Erbt die API Eigenschaften / Methoden von Controllerbase
    {
        // Statische Datenbank als PseudeDatenbank 
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Erste Aufgabe anlegen", IsCompleted = false }
        };
    

    // GET: api/tasks  --> tasks weil unsere Entität tasks heißt == Resource
    [HttpGet]
    public ActionResult<IEnumerable<TaskItem>> GetTasks()
    {
        return Ok(tasks);   // Gibt die Liste als HTTP 200 OK + JSON zurück
    }


    // GET: api/tasks/id --> GET Anfrage um ein Task mit bestimmter ID zu erhalten
    [HttpGet("{id}")]
    public ActionResult<TaskItem> GetTask(int id)
    {
        var task = tasks.Find(t => t.Id == id);    // Speicher über die Find Methode die gefundene Task mit der entsprechenden ID in variable
        if (task == null)
        {
            return NotFound(); // HTTP 404 nicht gefunden
        }
        return Ok(task);
    }

    // Post: api/tasks --> Post Methode welches einen neuen Datensatz in die Datenbanktabelle einfüge
    [HttpPost]
    public ActionResult<TaskItem> CreateTask(TaskItem newTask)
    {
        // Neue ID vergeben (größte vorhandene ID+1)
        int newId = tasks.Count > 0 ? tasks[^1].Id + 1 : 1;
            newTask.Id = newId;
        tasks.Add(newTask);
        return CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, newTask);
    }

    // PUT: api/tasks/1 [HttpPut("{id}")] public ActionResult UpdateTask(int id, TaskItem updatedTask)
    // Sucht eine Aufgabe per ID und aktualisiert deren Inhalt.
    [HttpPut("{id}")]
    public ActionResult UpdateTask(int id, TaskItem updatedTask)
    {
        var task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }
        task.Title = updatedTask.Title;
        task.IsCompleted = updatedTask.IsCompleted;
        return NoContent();  // HTTP 204
    }

    // DELETE: api/tasks/1 [HttpDelete("{id}")] public ActionResult DeleteTask(int id)
    // Löscht die Aufgabe mit der entsprechenden ID.
        [HttpDelete("{id}")]
    public ActionResult DeleteTask(int id)
    {
        var task = tasks.Find(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }
        tasks.Remove(task);
        return NoContent();
    }
}   

};