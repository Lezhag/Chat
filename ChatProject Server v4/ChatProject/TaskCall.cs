using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatProject
{
    public static class TaskCall
    {
        #region Declarations and Definitions
        //The snippet is courtesy of Roger Lipscombe (http://stackoverflow.com/questions/7472013/how-to-create-a-thread-task-with-a-continuous-loop)
        
        public static Task Interval(TimeSpan pollInterval, Action action, CancellationToken token)
        {
            // We don't use Observable.Interval:
            // If we block, the values start bunching up behind each other.
            return Task.Factory.StartNew(() =>
                {
                    for (; ; )
                    {
                        if (token.WaitCancellationRequested(pollInterval))
                            break;

                        action();
                    }
                }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public static bool WaitCancellationRequested(this CancellationToken token, TimeSpan timeout)
        {
            return token.WaitHandle.WaitOne(timeout);
        }

        //Usage scenario
        /*  var cancellationTokenSource = new CancellationTokenSource();
            var task = TaskCall.Interval(TimeSpan.FromSeconds(15),
                () => YourAction(), cancellationTokenSource.Token);
        */
        #endregion
    }
}
