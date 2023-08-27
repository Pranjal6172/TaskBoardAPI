using Newtonsoft.Json;
using Tasks.Service;

namespace UnitTests
{
    [TestFixture]
    public class TaskTests
    {
        private ITaskService _taskService;

        [SetUp]
        public void Setup()
        {
            _taskService = new TaskService();
        }

        #region Add Task

        [Test]
        public void AddTask_Insertion_ReturnsInsertedTaskDetails()
        {
            var temporaryTaskDetails = this.AddTemporaryTask();

            var result = _taskService.AddTask(temporaryTaskDetails);

            Assert.NotNull(result);
            Assert.AreEqual(result.Name, temporaryTaskDetails.Name);
            Assert.True(result.Id > 0);
        }

        [Theory]
        [TestCase(null, "The Description", 1)]
        [TestCase("The Task", null, 1)]
        [TestCase("The Task", "The Description", 0)]
        public void AddTask_NullValue_ReturnsNullArgumentException(string taskName, string description, int stateId)
        {
            var actual = new Tasks.Model.Task()
            {
                Name = taskName,
                Description = description,
                Deadline = DateTime.UtcNow,
                StateId = stateId
            };

            Assert.Throws<ArgumentNullException>(() => _taskService.AddTask(actual));
        }

        #endregion

        #region Update Task

        [Test]
        public void UpdateTask_Updation_ReturnsUpdatedTaskDetails()
        {
            var temporaryTaskDetails = this.AddTemporaryTask();

            var result = _taskService.UpdateTask(temporaryTaskDetails.Id, temporaryTaskDetails);

            Assert.NotNull(result);
            Assert.AreEqual(JsonConvert.SerializeObject(temporaryTaskDetails), JsonConvert.SerializeObject(result));
            Assert.True(result.Id > 0);
        }

        [Theory]
        [TestCase(null, "The Description", 1)]
        [TestCase("The Task", null, 1)]
        [TestCase("The Task", "The Description", 0)]
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

            Assert.Throws<ArgumentNullException>(() => _taskService.UpdateTask(1, actual));
        }

        [Test]
        public void UpdateTask_NotFound_ReturnsArgumentOutOfRangeException()
        {
            var actual = this.AddTemporaryTask();

            Assert.Throws<ArgumentOutOfRangeException>(() => _taskService.UpdateTask(50, actual));
        }

        #endregion

        #region Delete Task

        [Test]
        public void DeleteTask_Deletion_ReturnTrue()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            var result = _taskService.DeleteTask(tempTaskDetails.Id);

            Assert.True(result);
        }

        [Test]
        public void DeleteTask_NotFound_ReturnsArgumentOutOfRangeException()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            Assert.Throws<ArgumentOutOfRangeException>(() => _taskService.DeleteTask(50));
        }

        #endregion

        #region Get Task Details

        [Test]
        public void GetTask_AllDetails_ReturnsTaskDetails()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            var result = _taskService.GetTaskDetails(tempTaskDetails.Id);

            Assert.NotNull(result);
            Assert.AreEqual(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(tempTaskDetails));
        }

        [Test]
        public void GetTask_NotFound_ReturnsArgumentNullExeption()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            Assert.Throws<ArgumentNullException>(() => _taskService.GetTaskDetails(50));
        }

        [Test]
        public void GetSortedTasks_Sorting_ReturnsSortedTasks()
        {
            var allTasks = new List<Tasks.Model.Task>();
            for(int i = 0; i < 5; i++)
            {
                allTasks.Add(this.AddTemporaryTask("A" + i, "The Desc", 1, i % 2 != 0));
            }

            var results = _taskService.GetSortedTasks(1);

            Assert.AreEqual(results.Count, allTasks.Count);
            if (results.Any(r => r.IsFavourite))
            {
                Assert.IsTrue(results.FirstOrDefault().IsFavourite);
            }
        }

        #endregion

        #region Update Task State

        [Test]
        public void UpdateTaskState_Updation_ReturnTrue()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            var result = _taskService.UpdateTaskState(tempTaskDetails.Id, 3);

            Assert.True(result);
        }

        [Test]
        public void UpdateTaskState_NotFound_ReturnsArgumentOutOfRangeException()
        {
            var tempTaskDetails = this.AddTemporaryTask();

            Assert.Throws<ArgumentOutOfRangeException>(() => _taskService.UpdateTaskState(50, 50));
        }

        #endregion

        private Tasks.Model.Task AddTemporaryTask(string? taskName = null, string? description = null, int? stateId = null, bool isFavorite = false)
        {
            var temporaryTask = new Tasks.Model.Task()
            {
                Name = taskName ?? "The Temporary Task",
                Description = description ?? "The Temporary Description",
                Deadline = DateTime.UtcNow,
                StateId = stateId ?? 2,
                IsFavourite = isFavorite
            };

            return _taskService.AddTask(temporaryTask);
        }
    }
}