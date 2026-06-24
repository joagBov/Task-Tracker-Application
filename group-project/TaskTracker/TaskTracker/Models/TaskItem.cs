/*
 * FILE          : TaskItem.cs
 * PROJECT       : Task Tracker Application
 * PROGRAMMER    : Mehakpreet Singh, Elijah Atta-Konadu, Navjot Singh Bhullar , chase Maccash
 * FIRST VERSION : 2025
 * DESCRIPTION   : Model for task items in the Task Tracker application.
 */
namespace TaskTracker.Models
{
    public class TaskItem
    {
        // Unique identifier for the task
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Assignee { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsCompleted { get; set; }
    }
    /// <summary>
    /// Enumeration for task priority levels.
    /// </summary>
    public enum Priority
    {
        None = 0,
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

}
