using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Proxy
{
    static class TaskHelpers
    {
        /// <summary>
        /// Borrowed from: https://msdn.microsoft.com/en-us/library/hh873178(v=vs.110).aspx
        /// From TAP to APM
        /// </summary>
        /// <param name="task"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static IAsyncResult AsApm(this Task task,
                                   AsyncCallback callback,
                                   object state)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            var tcs = new TaskCompletionSource<object>(state);
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                    tcs.TrySetException(t.Exception.InnerExceptions);
                else if (t.IsCanceled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult(null);

                if (callback != null)
                    callback(tcs.Task);
            }, TaskScheduler.Default);
            return tcs.Task;
        }
    }
}
