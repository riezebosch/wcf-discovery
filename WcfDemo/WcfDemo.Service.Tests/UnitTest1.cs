using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using WcfDemo.Contracts;

namespace WcfDemo.Service.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IntegratieTestDemoVanService()
        {
            ServiceHost host = new ServiceHost(typeof(PingService));
            host.AddServiceEndpoint(typeof(IService),
                new NetNamedPipeBinding(), 
                "net.pipe://localhost/ping");

            try
            {
                host.Open();

                var client = ChannelFactory<IService>.CreateChannel(
                    new NetNamedPipeBinding(), 
                    new EndpointAddress("net.pipe://localhost/ping"));

                var result = client.Hello("from a unit test");
                Assert.AreEqual("Hello from a unit test", result);
            }
            finally
            {
                host.Abort();
            }
        }

        [TestMethod]
        public void UntiTestDemoVanService()
        {
            var service = new PingService();

            var result = service.Hello("from a unit test");
            Assert.AreEqual("Hello from a unit test", result);
        }

        [TestMethod]
        public void DemoVanFalendeIntegratieTestOmdatServiceNietConformWcfWerkt()
        {
            ServiceHost host = new ServiceHost(typeof(PingService));
            host.AddServiceEndpoint(typeof(IService),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ping");

            try
            {
                host.Open();

                var client = ChannelFactory<IService>.CreateChannel(
                    new NetNamedPipeBinding(),
                    new EndpointAddress("net.pipe://localhost/ping"));

                client.GiveMeAllTheDataz(new Data { Amount = 1, Ignored = "", ZId = 3 });
            }
            finally
            {
                host.Abort();
            }
        }
    }
}
