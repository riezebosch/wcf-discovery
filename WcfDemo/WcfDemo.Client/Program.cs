using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfDemo.Client.PingService;

namespace WcfDemo.Client
{
    class Program
    {
        class ServiceCallback : pingserviceCallback
        {
            public void Update(int i)
            {
                Console.WriteLine(i);
            }
        }

        static void Main(string[] args)
        {
            pingservice client = new pingserviceClient(
                new InstanceContext(
                    new ServiceCallback()));

            Console.WriteLine("Start Processing.");
            client.StartProcessing();
            Console.WriteLine("Processing started.");
            Console.WriteLine("Wait for the numbers to appear and press any key to continue.");

            Console.ReadKey();
        }

    }
}
