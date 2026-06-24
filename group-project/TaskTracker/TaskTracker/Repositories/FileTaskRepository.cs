/*
 * FILE          : FileTaskRepository.cs
 * PROJECT       : Task Tracker Application
 * PROGRAMMER    : Mehakpreet Singh, Elijah Atta-Konadu, Navjot Singh Bhullar, Chase Maccash
 * FIRST VERSION : 2025
 * DESCRIPTION   : Repository for managing task items stored in a file.
 */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskTracker.Models;

namespace TaskTracker.Repositories
{
    public class FileTaskRepository
    {
        // Path to the file storing tasks
        private readonly string filePath;
        private readonly object fileLock = new object();
        // Constructor that accepts the file path
        public FileTaskRepository(string filePath)
        {
            this.filePath = filePath;
            EnsureFileExists();
        }
        // Ensure the file exists, create if it doesn't
        private void EnsureFileExists()
        {
            // Create the file with header if it doesn't exist
            lock (fileLock)
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "Id,Title,Description,Assignee,Priority,CreatedDate,UpdatedDate,IsCompleted\n");
                }
            }
        }
        /// <summary>
        /// Get all task items from the file.
        /// </summary>
        /// <returns></returns>
        public List<TaskItem> GetAll()
        {
            // Read all lines from the file and parse them into TaskItem objects
            lock (fileLock)
            {
                if (!File.Exists(filePath))
                    return new List<TaskItem>();

                var lines = File.ReadAllLines(filePath).Skip(1);
                return lines.Select(ParseFromCsv).Where(task => task != null).ToList()!;
            }
        }
        /// <summary>
        /// Get a task item by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TaskItem? GetById(int id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }
        /// <summary>
        /// Get task items assigned to a specific assignee.
        /// </summary>
        /// <param name="assignee"></param>
        /// <returns></returns>
        public List<TaskItem> GetByAssignee(string assignee)
        {
            return GetAll().Where(t => t.Assignee.Equals(assignee, System.StringComparison.OrdinalIgnoreCase)).ToList();
        }
        /// <summary>
        /// Add a new task item to the file.
        /// </summary>
        /// <param name="task"></param>
        public void Add(TaskItem task)
        {
            // Assign a new ID and add the task to the file
            lock (fileLock)
            {
                var tasks = GetAll();
                task.Id = GetNextId();
                tasks.Add(task);
                SaveAll(tasks);
            }
        }
        /// <summary>
        /// Update an existing task item in the file.
        /// </summary>
        /// <param name="task"></param>
        public void Update(TaskItem task)
        {
            // Find the existing task and update it
            lock (fileLock)
            {
                // Get all tasks
                var tasks = GetAll();
                var existing = tasks.FirstOrDefault(t => t.Id == task.Id);
                // If found, update it
                if (existing != null)
                {
                    tasks.Remove(existing);
                    task.UpdatedDate = DateTime.Now;
                    tasks.Add(task);
                    SaveAll(tasks);
                }
            }
        }
        /// <summary>
        /// Delete a task item by its ID.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            // Find the task by ID and remove it
            lock (fileLock)
            {
                var tasks = GetAll();
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    tasks.Remove(task);
                    SaveAll(tasks);
                }
            }
        }
        /// <summary>
        /// Get the next available task ID.
        /// </summary>
        /// <returns></returns>
        public int GetNextId()
        {
            // Get the maximum existing ID and increment it
            var tasks = GetAll();
            return tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
        }
        /// <summary>
        /// Parse a CSV line into a TaskItem object.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private TaskItem? ParseFromCsv(string line)
        {
            // Simple CSV parsing, assumes no commas in fields
            try
            {
                var values = line.Split(',');
                if (values.Length < 8) return null;
                // Create and return TaskItem
                return new TaskItem
                {
                    Id = int.Parse(values[0]),
                    Title = values[1],
                    Description = values[2],
                    Assignee = values[3],
                    Priority = Enum.Parse<Priority>(values[4]),
                    CreatedDate = DateTime.Parse(values[5]),
                    UpdatedDate = string.IsNullOrEmpty(values[6]) ? null : DateTime.Parse(values[6]),
                    IsCompleted = bool.Parse(values[7])
                };
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Save all task items to the file.
        /// </summary>
        /// <param name="tasks"></param>
        private void SaveAll(List<TaskItem> tasks)
        {
            // Write all tasks to the file in CSV format
            var lines = new List<string> { "Id,Title,Description,Assignee,Priority,CreatedDate,UpdatedDate,IsCompleted" };
            lines.AddRange(tasks.Select(task => $"{task.Id},{EscapeCsv(task.Title)},{EscapeCsv(task.Description)},{EscapeCsv(task.Assignee)},{task.Priority},{task.CreatedDate:O},{task.UpdatedDate:O},{task.IsCompleted}"));
            // Write to file
            lock (fileLock)
            {
                File.WriteAllLines(filePath, lines);
            }
        }
        /// <summary>
        /// Escape CSV values that contain commas or quotes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string EscapeCsv(string value)
        {
            // Escape commas and quotes in CSV values
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return value.Contains(',') || value.Contains('"') || value.Contains('\n') ? $"\"{value.Replace("\"", "\"\"")}\"" : value;
        }
    }
}