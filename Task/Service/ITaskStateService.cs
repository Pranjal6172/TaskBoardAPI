using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Model;

namespace Tasks.Service
{
    public interface ITaskStateService
    {
        TaskState AddTaskState(TaskState taskStateDetails);
    }
}
