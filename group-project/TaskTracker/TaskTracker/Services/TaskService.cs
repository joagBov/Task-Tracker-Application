/*
 * FILE          : TaskService.cs
 * PROJECT       : Task Tracker Application
 * PROGRAMMER    : Mehakpreet Singh, Elijah Atta-Konadu, Navjot Singh Bhullar
 * FIRST VERSION : 2025
 * DESCRIPTION   : Service layer for managing task items in the Task Tracker application.
 */
using TaskTracker.Models;
using TaskTracker.Repositories;

namespace TaskTracker.Services
{
    public class TaskService
    {
        private readonly FileTaskRepository taskRepository;
        // Constructor that initializes the repository
        public TaskService()
        {
            taskRepository = new FileTaskRepository("tasks.csv");
        }
        /// <summary>
        /// Get all tasks.
        /// </summary>
        /// <returns></returns>
        public List<TaskItem> GetAllTasks()
        {
            return taskRepository.GetAll();
        }
        /// <summary>
        /// Get a task by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TaskItem? GetTaskById(int id)
        {
            return taskRepository.GetById(id);
        }
        /// <summary>
        /// Get tasks assigned to a specific user.
        /// </summary>
        /// <param name="assignee"></param>
        /// <returns></returns>
        public List<TaskItem> GetTasksByAssignee(string assignee)
        {
            return taskRepository.GetByAssignee(assignee);
        }
        /// <summary>
        /// Create a new task.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public TaskItem CreateTask(string title, string description)
        {
            // Create a new TaskItem object
            var task = new TaskItem
            {
                Title = title,
                Description = description,
                CreatedDate = DateTime.Now,
                Priority = Priority.None
            };
            // Add the task to the repository
            taskRepository.Add(task);
            return task;
        }
        /// <summary>
        /// Update an existing task.
        /// </summary>
        /// <param name="task"></param>
        public void UpdateTask(TaskItem task)
        {
            taskRepository.Update(task);
        }
        /// <summary>
        /// Delete a task by its ID.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTask(int id)
        {
            taskRepository.Delete(id);
        }
        /// <summary>
        /// Assign a task to a user.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="assignee"></param>
        public void AssignTask(int taskId, string assignee)
        {
            // Retrieve the task by ID
            var task = taskRepository.GetById(taskId);
            if (task != null)
            {
                task.Assignee = assignee;
                taskRepository.Update(task);
            }
        }
        /// <summary>
        /// Remove the assignee from a task.
        /// </summary>
        /// <param name="taskId"></param>
        public void RemoveAssignee(int taskId)
        {
            // Retrieve the task by ID
            var task = taskRepository.GetById(taskId);
            if (task != null)
            {
                task.Assignee = string.Empty;
                taskRepository.Update(task);
            }
        }
        /// <summary>
        /// Set the priority of a task.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="priority"></param>
        public void SetPriority(int taskId, Priority priority)
        {
            // Retrieve the task by ID
            var task = taskRepository.GetById(taskId);
            if (task != null)
            {
                task.Priority = priority;
                taskRepository.Update(task);
            }
        }
        /// <summary>
        /// Remove the priority from a task.
        /// </summary>
        /// <param name="taskId"></param>
        public void RemovePriority(int taskId)
        {
            // Retrieve the task by ID
            var task = taskRepository.GetById(taskId);
            if (task != null)
            {
                task.Priority = Priority.None;
                taskRepository.Update(task);
            }
        }
    }
}