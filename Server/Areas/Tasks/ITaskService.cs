using System.Linq;
using System;
using System.Collections.Generic;

namespace Occumetric.Server.Areas.Tasks
{
    public interface ITaskService
    {
        JobTask CloneMasterTaskToTask(int id);
    }
}