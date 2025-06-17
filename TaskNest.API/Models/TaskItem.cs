using System.ComponentModel.DataAnnotations;

namespace TaskNest.API.Models // Namespace beschreibt, wo die Klasse im Projekt liegt (wichtig für Namenskonflikte) = Zugehörigkeit
{
    public class TaskItem // Public = überall nutzbar , Klasse ist über nutzbar --> Gegenteil wäre private = nicht einsehbar
    {
        public int Id { get; set; } // Eindeutige ID für die Aufgabe == Autoinkrement , bei jeder neuen Task +1 , Primary Key
        
        [Required(ErrorMessage = "Titel ist erforderlich.")]
        [StringLength(100, ErrorMessage = "Titel darf maximal 100 Zeichen lang sein.")]
        public string Title { get; set; } // Titel / Beschreibung der Aufgabe

        public bool IsCompleted { get; set; } // Status (erledigt oder nicht = true or false)
    }
}
