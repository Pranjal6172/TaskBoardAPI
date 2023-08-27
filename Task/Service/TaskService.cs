using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks.Service
{
    public class TaskService : ITaskService
    {
        private static List<Model.Task> Tasks { get; set; }

        static TaskService()
        {
            Tasks = new List<Model.Task>();
        }

        public TaskService()
        {
        }

        public Model.Task GetTaskDetails(int taskId)
        {
            var taskDetails = Tasks.SingleOrDefault(t => t.Id == taskId);
            if (taskDetails != null)
            {
                return taskDetails;
            }

            throw new ArgumentNullException("Task not found.");
        }

        public List<Model.Task> GetSortedTasks(int stateId)
        {
            return Tasks.Where(t => t.StateId == stateId).OrderBy(t => !t.IsFavourite).ThenBy(t => t.Name).ToList();
        }

        public Model.Task AddTask(Model.Task taskDetails)
        {
            this.ValidateTaskDetails(taskDetails);
            taskDetails.Id = Tasks.Any() ? Tasks.LastOrDefault().Id + 1 : 1;
            Tasks.Add(taskDetails);
            return taskDetails;
        }

        public Model.Task UpdateTask(int taskId, Model.Task taskDetails)
        {
            this.ValidateTaskDetails(taskDetails);
            var existingTaskIndex = Tasks.FindIndex(t => t.Id == taskId);
            if (existingTaskIndex >= 0)
            {
                Tasks[existingTaskIndex] = taskDetails;
                return taskDetails;
            }

            throw new ArgumentOutOfRangeException("Task not found.");
        }

        public bool UpdateTaskState(int taskId, int stateId)
        {
            var existingTaskIndex = Tasks.FindIndex(t => t.Id == taskId);
            if (existingTaskIndex >= 0)
            {
                Tasks[existingTaskIndex].StateId = stateId;
                return true;
            }

            throw new ArgumentOutOfRangeException("Task not found.");
        }

        public bool DeleteTask(int taskId)
        {
            var existingTaskIndex = Tasks.FindIndex(t => t.Id == taskId);
            if (existingTaskIndex >= 0)
            {
                Tasks.RemoveAt(existingTaskIndex);
                return true;
            }

            throw new ArgumentOutOfRangeException("Task not found.");
        }

        private void ValidateTaskDetails(Model.Task taskDetails)
        {
            if (taskDetails.Name is null || taskDetails.Description is null || taskDetails.Deadline == default(DateTime) || taskDetails.StateId == 0)
            {
                throw new ArgumentNullException("Required properties are not passed");
            }
        }
    }
}
