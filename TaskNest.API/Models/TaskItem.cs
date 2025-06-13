namespace TaskNest.API.Models // Namespace beschreibt, wo die Klasse im Projekt liegt (wichtig für Namenskonflikte) = Zugehörigkeit
{
    public class TaskItem // Public = überall nutzbar , Klasse ist über nutzbar --> Gegenteil wäre private = nicht einsehbar
    {
        public int Id { get; set; } // Eindeutige ID für die Aufgabe == Autoinkrement , bei jeder neuen Task +1 , Primary Key

        public string Title { get; set; } // Titel / Beschreibung der Aufgabe

        public bool IsCompleted { get; set; } // Status (erledigt oder nicht = true or false)
    }
}
