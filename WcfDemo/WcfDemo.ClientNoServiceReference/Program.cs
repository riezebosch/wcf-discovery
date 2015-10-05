using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfDemo.Contracts;

namespace WcfDemo.ClientNoServiceReference
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = ChannelFactory<IService>.CreateChannel(new BasicHttpBinding(),
                new EndpointAddress("http://localhost:2350/PingService.svc"));

            client.Ping();

            Console.WriteLine(client.Hello("Manuel"));
        }
    }
}
