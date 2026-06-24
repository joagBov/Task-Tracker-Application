/*
 * FILE          : ErrorViewModel.cs
 * PROJECT       : Task Tracker Application
 * PROGRAMMER    : Mehakpreet Singh, Elijah Atta-Konadu, Navjot Singh Bhullar, Chase Mccash , chase Maccash
 * FIRST VERSION : 2025
 * DESCRIPTION   : Model for error view in the Task Tracker application.
 */
namespace TaskTracker.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
