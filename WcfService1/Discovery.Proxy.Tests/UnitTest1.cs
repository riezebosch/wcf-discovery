using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using Shouldly;
using System.Linq;

namespace Discovery.Proxy.Tests
{
    [TestClass]
    public class UnitTest1
    {
        ServiceHost host;
        DiscoveryProxy proxy;
      
        [TestInitialize]
        public void TestInitialize()
        {
            host = new ServiceHost(proxy = new DiscoveryProxy());
            host.AddServiceEndpoint(
                new AnnouncementEndpoint(
                    new NetTcpBinding(),
                    new EndpointAddress("net.tcp://localhost:9021/Announcement")));

            host.Open();
        }


        [TestCleanup]
        public void TestCleanup()
        {
            host.Abort();
        }

        [TestMethod]
        public void DiscoveryProxyShouldReceiveAnnouncements()
        {
            var host = new ServiceHost(typeof(DummyService));
            var discovery = new ServiceDiscoveryBehavior();
            discovery.AnnouncementEndpoints.Add(new AnnouncementEndpoint(
                    new NetTcpBinding(),
                    new EndpointAddress("net.tcp://localhost:9021/Announcement")));

            host.Description.Behaviors.Add(discovery);

            host.AddServiceEndpoint(
                typeof(DummyService), 
                new NetNamedPipeBinding(), 
                "net.pipe://localhost/dummy");

            try
            {
                host.Open();
                proxy.Services.Count().ShouldBeGreaterThan(0);
            }
            finally
            {
                host.Abort();
            }
        }
    }
}
