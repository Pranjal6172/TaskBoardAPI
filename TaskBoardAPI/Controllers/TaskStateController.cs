using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Model;
using Tasks.Service;

namespace TaskBoardAPI.Controllers
{
    [Route("api/taskstate")]
    [ApiController]
    public class TaskStateController : ControllerBase
    {
        private ITaskStateService TaskStateService { get; set; }

        public TaskStateController(ITaskStateService taskStateService)
        {
            this.TaskStateService = taskStateService;
        }

        [HttpPost]
        public TaskState AddTaskState(TaskState taskStateDetails)
        {
            return this.TaskStateService.AddTaskState(taskStateDetails);
        }
    }
}
