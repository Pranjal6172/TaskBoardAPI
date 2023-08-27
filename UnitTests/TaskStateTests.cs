using Newtonsoft.Json;
using Tasks.Service;

namespace UnitTests
{
    [TestFixture]
    public class TaskStateTests
    {
        private ITaskStateService _taskStateService;

        [SetUp]
        public void Setup()
        {
            _taskStateService = new TaskStateService();
        }

        #region Add Task

        [Test]
        public void AddTaskState_Insertion_ReturnsInsertedTaskStateDetails()
        {
            var temporaryTaskStateDetails = this.AddTemporaryTaskState();

            var result = _taskStateService.AddTaskState(temporaryTaskStateDetails);

            Assert.NotNull(result);
            Assert.AreEqual(result.Name, temporaryTaskStateDetails.Name);
            Assert.True(result.Id > 0);
        }

        [Test]
        public void AddTask_NullValue_ReturnsNullArgumentException()
        {
            var actual = new Tasks.Model.TaskState()
            {
                Name = null,
            };

            Assert.Throws<ArgumentNullException>(() => _taskStateService.AddTaskState(actual));
        }

        #endregion

        private Tasks.Model.TaskState AddTemporaryTaskState()
        {
            var temporaryTaskState = new Tasks.Model.TaskState()
            {
                Name = "The Temporary Task State",
            };

            return _taskStateService.AddTaskState(temporaryTaskState);
        }
    }
}