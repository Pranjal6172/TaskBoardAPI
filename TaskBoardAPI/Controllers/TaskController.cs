using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Service;
using TaskModel = Tasks.Model;

namespace TaskBoardAPI.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public ITaskService TaskService { get; set; }

        public TaskController(ITaskService taskService)
        {
            this.TaskService = taskService;
        }

        [HttpGet("{taskId}")]
        public TaskModel.Task GetTaskDetails(int taskId)
        {
            return this.TaskService.GetTaskDetails(taskId);
        }

        [HttpGet("sorted/tasks/state/{stateId}")]
        public List<TaskModel.Task> GetSortedTask(int stateId)
        {
            return this.TaskService.GetSortedTasks(stateId);
        }

        [HttpPost]
        public TaskModel.Task AddTask(TaskModel.Task taskDetails)
        {
            return this.TaskService.AddTask(taskDetails);
        }

        [HttpPut("{taskId}")]
        public TaskModel.Task UpdateTask(int taskId, TaskModel.Task taskDetails)
        {
            return this.TaskService.UpdateTask(taskId, taskDetails);
        }

        [HttpPut("{taskId}/state/{stateId}")]
        public bool UpdateTaskState(int taskId, int stateId)
        {
            return this.TaskService.UpdateTaskState(taskId, stateId);
        }

        [HttpDelete("{taskId}")]
        public bool DeleteTask(int taskId)
        {
            return this.TaskService.DeleteTask(taskId);
        }
    }
}
