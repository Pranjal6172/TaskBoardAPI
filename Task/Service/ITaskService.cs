using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks.Service
{
    public interface ITaskService
    {
        Model.Task GetTaskDetails(int taskId);

        List<Model.Task> GetSortedTasks(int stateId);

        Model.Task AddTask(Model.Task taskDetails);

        Model.Task UpdateTask(int taskId, Model.Task taskDetails);

        bool UpdateTaskState(int taskId, int stateId);

        bool DeleteTask(int taskId);
    }
}
