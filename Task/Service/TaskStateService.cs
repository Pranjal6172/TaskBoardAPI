using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Model;

namespace Tasks.Service
{
    public class TaskStateService: ITaskStateService
    {
        private static List<TaskState> TaskStates { get; set; }

        static TaskStateService()
        {
            TaskStates = new List<TaskState>();
        }

        public TaskStateService()
        {
        }

        public TaskState AddTaskState(TaskState taskStateDetails)
        {
            if (taskStateDetails.Name is null)
            {
                throw new ArgumentNullException("Task State Name Is Required");
            }

            taskStateDetails.Id = TaskStates.Any() ? TaskStates.LastOrDefault().Id + 1 : 1;
            TaskStates.Add(taskStateDetails);
            return taskStateDetails;
        }
    }
}
