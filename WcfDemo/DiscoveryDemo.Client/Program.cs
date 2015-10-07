using DiscoveryDemo.Client.MagicOracleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscoveryDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Action run = RunDiscoveryClient;
            Task.Run(run);

            Console.ReadKey();
        }

        private static void RunDiscoveryClient()
        {
            while (true)
            {
                var client = new MagicOracleClient();

                try
                {
                    var result = client.Answer("Hoe laat is het?");
                    Console.WriteLine(result);

                }
                catch (Exception ex)
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
                finally
                {
                    CloseClient((ICommunicationObject)client);
                }

                Console.WriteLine();
            }
        }

        private static void CloseClient(ICommunicationObject client)
        {
            if (client.State == CommunicationState.Opened)
            {
                try
                {
                    client.Close();
                }
                catch (CommunicationException)
                {
                    client.Abort();
                }
                catch (TimeoutException)
                {
                    client.Abort();
                }
            }
        }
    }
}
