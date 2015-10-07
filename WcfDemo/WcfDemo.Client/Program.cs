using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            PingService.pingservice client = new PingService.pingserviceClient("BasicHttpBinding_ping-service");
            client.ping();

            Stopwatch sw = Stopwatch.StartNew();
            client.Slow();
            Console.WriteLine(sw.Elapsed);
        }
    }
}
