using Discovery.Console.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Discovery.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                var client = new OracleClient();
                try
                {
                    var result = client.Ask("random");
                    WriteLine(result);
                }
                finally
                {
                    if (client.State == CommunicationState.Opened)
                    {
                        client.Close();
                    }
                    else
                    {
                        client.Abort();
                    }
                }
            }
        }
    }
}
