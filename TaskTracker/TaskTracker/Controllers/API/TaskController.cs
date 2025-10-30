/*
 * FILE          : TaskController.cs
 * PROJECT       : Task Tracker Application
 * PROGRAMMER    : Mehakpreet Singh, Elijah Atta-Konadu, Navjot Singh Bhullar
 * FIRST VERSION : 2025
 * DESCRIPTION   : API Controller for managing tasks in the Task Tracker application.
 */
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers.Api
{

    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        // Dependency injection of TaskService
        private readonly TaskService taskService;
        // Constructor
        public TasksController(TaskService taskService)
        {
            this.taskService = taskService;
        }
        // GET: api/tasks
        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = taskService.GetAllTasks();
            return Ok(tasks);
        }
        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Get task by ID
            var task = taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        // GET: api/tasks/assignee/{assignee}
        [HttpGet("assignee/{assignee}")]
        public IActionResult GetByAssignee(string assignee)
        {
            // Get tasks by assignee
            var tasks = taskService.GetTasksByAssignee(assignee);
            return Ok(tasks);
        }
        // POST: api/tasks
        [HttpPost]
        public IActionResult Create([FromBody] CreateTaskRequest request)
        {
            // Validate request
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Title is required");
            }
            // Create new task
            var task = taskService.CreateTask(request.Title, request.Description);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }
        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskItem task)
        {
            // Validate ID
            if (id != task.Id)
            {
                return BadRequest("ID mismatch");
            }
            var existingTask = taskService.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            taskService.UpdateTask(task);
            return Ok(task);
        }
        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Validate existence
            var task = taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }

            taskService.DeleteTask(id);
            return NoContent();
        }
        // POST: api/tasks/{id}/assign
        [HttpPost("{id}/assign")]
        public IActionResult Assign(int id, [FromBody] AssignRequest request)
        {
            // Validate existence
            var task = taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            // Assign task
            taskService.AssignTask(id, request.Assignee);
            return Ok();
        }
        // POST: api/tasks/{id}/remove-assignee
        [HttpPost("{id}/remove-assignee")]
        public IActionResult RemoveAssignee(int id)
        {
            // Validate existence
            var task = taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            // Remove assignee
            taskService.RemoveAssignee(id);
            return Ok();
        }
        // POST: api/tasks/{id}/priority
        [HttpPost("{id}/priority")]
        public IActionResult SetPriority(int id, [FromBody] SetPriorityRequest request)
        {
            // Validate existence
            var task = taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            // Set priority
            taskService.SetPriority(id, request.Priority);
            return Ok();
        }
        // DELETE: api/tasks/{id}/priority
        [HttpDelete("{id}/priority")]
        public IActionResult RemovePriority(int id)
        {
            // Validate existence 
            var task = taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            // Remove priority
            taskService.RemovePriority(id);
            return Ok();
        }
    }
    /// <summary>
    /// Request model for creating a new task.
    /// </summary>
    public class CreateTaskRequest
    {
        // Title of the task
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
    /// <summary>
    /// Request model for assigning a task to a user.
    /// </summary>
    public class AssignRequest
    {
        // Assignee username
        public string Assignee { get; set; } = string.Empty;
    }
    /// <summary>
    /// Request model for setting the priority of a task.
    /// </summary>
    public class SetPriorityRequest
    {
        // Priority level
        public Priority Priority { get; set; }
    }
}