using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Newtonsoft.Json;
using TaskBoardAPI.Controllers;
using Tasks.Service;

namespace Tests
{
    public class TaskTests
    {
        private ITaskService _taskService;
        public TaskTests()
        {
            _taskService = new TaskService();
        }

        #region Add Task

        [Fact]
        public void AddTask_Insertion_ReturnsInsertedTaskDetails()
        {
            var actual = new Tasks.Model.Task()
            {
                Name = "The Task",
                Description = "The Description",
                Deadline = DateTime.UtcNow,
                StateId = 2
            };

            var result = _taskService.AddTask(actual);

            Assert.NotNull(result);
            Assert.Equal(result.Name, actual.Name);
            Assert.True(result.Id > 0);
        }

        [Theory]
        [InlineData(null, "The Description", 1)]
        [InlineData("The Task", null, 1)]
        [InlineData("The Task", "The Description", 0)]
        public void AddTask_NullValue_ReturnsNullArgumentException(string taskName, string description, int stateId)
        {
            var actual = new Tasks.Model.Task()
            {
                Name = taskName,
                Description = description,
                Deadline = DateTime.UtcNow,
                StateId = stateId
            };

            Action result = () => _taskService.AddTask(actual);

            Assert.Throws<ArgumentNullException>(result);
        }

        #endregion

        #region Update Task

        [Fact]
        public void UpdateTask_Updation_ReturnsUpdatedTaskDetails()
        {
            var actual = new Tasks.Model.Task()
            {
                Id = 1,
                Name = "The Updated Task",
                Description = "The Description",
                Deadline = DateTime.UtcNow,
                StateId = 2
            };

            var result = _taskService.UpdateTask(1, actual);

            Assert.NotNull(result);
            Assert.Equal(JsonConvert.SerializeObject(actual), JsonConvert.SerializeObject(result));
            Assert.True(result.Id > 0);
        }

        [Theory]
        [InlineData(null, "The Description", 1)]
        [InlineData("The Task", null, 1)]
        [InlineData("The Task", "The Description", 0)]
        public void UpdateTask_NullValue_ReturnsNullArgumentException(string taskName, string description, int stateId)
        {
            var actual = new Tasks.Model.Task()
            {
                Id = 1,
                Name = taskName,
                Description = description,
                Deadline = DateTime.UtcNow,
                StateId = stateId
            };

            Action result = () => _taskService.UpdateTask(1, actual);

            Assert.Throws<ArgumentNullException>(result);
        }

        [Fact]
        public void UpdateTask_NotFound_ReturnsArgumentOutOfRangeException()
        {
            var actual = new Tasks.Model.Task()
            {
                Id = 1,
                Name = "The Task",
                Description = "The Description",
                Deadline = DateTime.UtcNow,
                StateId = 2
            };

            Action result = () => _taskService.UpdateTask(4, actual);

            Assert.Throws<ArgumentOutOfRangeException>(result);
        }

        #endregion

        #region Delete Task

        [Fact]
        public void DeleteTask_Deletion_ReturnTrue()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            var result = _taskService.DeleteTask(tempTaskDetails.Id);

            Assert.True(result);
        }

        [Fact]
        public void DeleteTask_NotFound_ReturnsArgumentOutOfRangeException()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            Action result = () => _taskService.DeleteTask(50);

            Assert.Throws<ArgumentOutOfRangeException>(result);
        }

        #endregion

        #region Get Task Details

        [Fact]
        public void GetTask_AllDetails_ReturnsTaskDetails()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            var result = _taskService.GetTaskDetails(tempTaskDetails.Id);

            Assert.NotNull(result);
            Assert.Equal(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(tempTaskDetails));
        }

        [Fact]
        public void GetTask_NotFound_ReturnsArgumentNullExeption()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            Action result = () => _taskService.GetTaskDetails(50);

            Assert.Throws<ArgumentNullException>(result);
        }

        #endregion

        #region Update Task State

        [Fact]
        public void UpdateTaskState_Updation_ReturnTrue()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            var result = _taskService.UpdateTaskState(tempTaskDetails.Id, 3);

            Assert.True(result);
        }

        [Fact]
        public void UpdateTaskState_NotFound_ReturnsArgumentOutOfRangeException()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            Action result = () => _taskService.UpdateTaskState(50, 50);

            Assert.Throws<ArgumentOutOfRangeException>(result);
        }

        #endregion

        private Tasks.Model.Task AddTemporaryTask()
        {
            var temporaryTask = new Tasks.Model.Task()
            {
                Name = "The Temporary Task",
                Description = "The Temporary Description",
                Deadline = DateTime.UtcNow,
                StateId = 2
            };

            return _taskService.AddTask(temporaryTask);
        }
    }
}