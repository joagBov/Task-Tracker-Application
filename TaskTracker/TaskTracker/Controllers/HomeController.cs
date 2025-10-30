/*
 * FILE          : HomeController.cs
 * PROJECT       : Task Tracker Application
 * PROGRAMMER    : Mehakpreet Singh, Elijah Atta-Konadu, Navjot Singh Bhullar
 * FIRST VERSION : 2025
 * DESCRIPTION   : MVC Controller for managing tasks in the Task Tracker application.
 */

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskService _taskService;

        // Constructor
        public HomeController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: /
        public IActionResult Index()
        {
            // Get all tasks
            var tasks = _taskService.GetAllTasks();
            return View(tasks);
        }

        // GET: /Details/{id}
        public IActionResult Details(int id)
        {
            // Get task by ID
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                ViewBag.SearchMessage = "404 - Task not found";
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // GET: /Create
        [HttpGet]
        public IActionResult Create()
        {
            // Show create task form
            return View();
        }

        // POST: /Create
        [HttpPost]
        public IActionResult Create(string title, string description)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("Title", "Title is required");
                ViewBag.SearchMessage = "400 - Title is required";
                return View();
            }
            // Create new task
            _taskService.CreateTask(title, description);
            ViewBag.SearchMessage = "201 - Task created successfully";
            return RedirectToAction("Index");
        }

        // GET: /Edit/{id}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Get task by ID
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                ViewBag.SearchMessage = "404 - Task not found";
                return RedirectToAction("Index");
            }
            // Show edit task form
            return View(task);
        }

        // POST: /Edit
        [HttpPost]
        public IActionResult Edit(TaskItem task)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                ModelState.AddModelError("Title", "Title is required");
                ViewBag.SearchMessage = "400 - Title is required";
                return View(task);
            }
            // Update task
            _taskService.UpdateTask(task);
            ViewBag.SearchMessage = "200 - Task updated successfully";
            return RedirectToAction("Index");
        }

        // POST: /Delete/{id}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Delete task
            _taskService.DeleteTask(id);
            ViewBag.SearchMessage = "204 - Task deleted successfully";
            return RedirectToAction("Index");
        }

        // POST: /Assign
        [HttpPost]
        public IActionResult Assign(int id, string assignee)
        {
            if (string.IsNullOrWhiteSpace(assignee))
            {
                ViewBag.SearchMessage = "400 - Assignee name is required";
                return RedirectToAction("Details", new { id });
            }

            // Assign task to user
            _taskService.AssignTask(id, assignee);
            ViewBag.SearchMessage = "200 - Task assigned successfully";
            return RedirectToAction("Details", new { id });
        }

        // POST: /RemoveAssignee
        [HttpPost]
        public IActionResult RemoveAssignee(int id)
        {
            // Remove assignee from task
            _taskService.RemoveAssignee(id);
            ViewBag.SearchMessage = "200 - Assignee removed successfully";
            return RedirectToAction("Details", new { id });
        }

        // POST: /SetPriority
        [HttpPost]
        public IActionResult SetPriority(int id, Priority priority)
        {
            // Set task priority
            _taskService.SetPriority(id, priority);
            ViewBag.SearchMessage = "200 - Priority set successfully";
            return RedirectToAction("Details", new { id });
        }

        // POST: /RemovePriority
        [HttpPost]
        public IActionResult RemovePriority(int id)
        {
            // Remove task priority
            _taskService.RemovePriority(id);
            ViewBag.SearchMessage = "200 - Priority removed successfully";
            return RedirectToAction("Details", new { id });
        }

        // GET: /Search
        [HttpGet]
        public IActionResult Search(string searchTerm, string searchType)
        {
            // If no search term, show all tasks with message
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                ViewBag.SearchMessage = "400 - Please enter a search term";
                var allTasks = _taskService.GetAllTasks();
                return View("Index", allTasks);
            }

            // Search by assignee
            if (searchType == "assignee")
            {
                var tasks = _taskService.GetTasksByAssignee(searchTerm);
                if (tasks.Any())
                {
                    ViewBag.SearchMessage = $"200 - Found {tasks.Count} task(s) for assignee '{searchTerm}'";
                    return View("Index", tasks);
                }
                else
                {
                    ViewBag.SearchMessage = $"404 - No tasks found for assignee '{searchTerm}'";
                    var allTasks = _taskService.GetAllTasks();
                    return View("Index", allTasks);
                }
            }
            // Search by ID
            else if (searchType == "id" && int.TryParse(searchTerm, out int taskId))
            {
                var task = _taskService.GetTaskById(taskId);
                if (task != null)
                {
                    // For ID search, we show the details page if task is found
                    return View("Details", task);
                }
                else
                {
                    ViewBag.SearchMessage = $"404 - Task with ID {taskId} not found";
                    var allTasks = _taskService.GetAllTasks();
                    return View("Index", allTasks);
                }
            }
            else
            {
                ViewBag.SearchMessage = "400 - Invalid search. Please search by ID or Assignee";
                var allTasks = _taskService.GetAllTasks();
                return View("Index", allTasks);
            }
        }
    }
}