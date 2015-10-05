using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            PingService.ServiceClient client = new PingService.ServiceClient();
            client.Ping();

        }
    }
}
