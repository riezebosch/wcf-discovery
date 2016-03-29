using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.ServiceModel;
using Netflix.ServiceContract;

namespace Netflix.Service.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Top10MeestBekekenTitels()
        {
            var host = new ServiceHost(typeof(NetflixService));
            try
            {
                host.AddServiceEndpoint(typeof(INetflixService),
                    new NetNamedPipeBinding(),
                    "net.pipe://localhost/netflix");
                host.Open();

                var client = ChannelFactory<INetflixService>.CreateChannel(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/netflix"));
                client.Top10().Length.ShouldBe(10);
                ((ICommunicationObject)client).Close();
            }
            finally
            {
                host.Close();
            }


           
        }
    }
}
