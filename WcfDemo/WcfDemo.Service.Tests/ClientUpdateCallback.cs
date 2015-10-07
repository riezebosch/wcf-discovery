using System;
using System.Threading;
using WcfDemo.Contracts;

namespace WcfDemo.Service.Tests
{
    public class ClientUpdateCallback : 
        IServiceCallback, 
        IDisposable
    {
        AutoResetEvent wait = new AutoResetEvent(false);

        public int Progress { get; private set; }

        public void Dispose()
        {
            wait.Dispose();
        }

        public void Update(int i)
        {
            Progress = i;
        }

        public void Ready()
        {
            wait.Set();
        }

        public void Wait()
        {
            wait.WaitOne();
        }
    }
}