using System;
using Netflix.DataContracts;
using Netflix.ServiceContract;
using System.Threading;
using Newtonsoft.Json;

namespace Netflix.Service.Tests
{
    internal class NetflixCallback :
        INetflixCallback,
        IDisposable
    {
        public void Result(Title[] titles)
        {
            Console.WriteLine($"--- Result @ { DateTime.Now }");
            var settings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            Array.ForEach(titles, t => Console.WriteLine(JsonConvert.SerializeObject(t, Formatting.Indented, settings)));

            Console.WriteLine("---");
        }

        AutoResetEvent wait = new AutoResetEvent(false);

        public void WaitForAllResults()
        {
            if (!wait.WaitOne(TimeSpan.FromSeconds(30)))
            {
                throw new TimeoutException("Waited for too long!");
            }
        }

        public void Done()
        {
            wait.Set();
        }

        public void Dispose()
        {
            wait.Dispose();
        }
    }
}