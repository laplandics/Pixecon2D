using System.Threading.Tasks;
using UnityEngine;

namespace Tools
{
    public class WaitForTask : CustomYieldInstruction
    {
        private readonly Task _task;

        public WaitForTask(Task task)
        { _task = task; }

        public override bool keepWaiting
        {
            get
            {
                if (!_task.IsCompleted) return true;
                if (_task.Exception != null)
                { Debug.LogException(_task.Exception); }
                return false;
            }
        }
    }
    
    public class WaitForTask<T> : CustomYieldInstruction
    {
        private readonly Task<T> _task;

        public T Result => _task.Result;

        public WaitForTask(Task<T> task) { _task = task; }

        public override bool keepWaiting
        {
            get
            {
                if (!_task.IsCompleted) return true;
                if (_task.Exception != null)
                { Debug.LogException(_task.Exception); }
                return false;
            }
        }
    }
}