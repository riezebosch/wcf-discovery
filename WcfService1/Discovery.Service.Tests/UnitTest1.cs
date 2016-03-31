using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using Discovery.Contracts;
using System.ServiceModel.Description;
using Shouldly;

namespace Discovery.Service.Tests
{
    [TestClass]
    public class UnitTest1
    {
        static ServiceHost host;

        [ClassInitialize]
        public static void Initialize(TestContext unused)
        {
            host = new ServiceHost(typeof(DiscoveryOracle));
            host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
            host.AddServiceEndpoint(typeof(IDiscoveryOracle),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/discovery");

            host.Open();
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            host.Abort();
        }


        IDiscoveryOracle client;

        [TestInitialize]
        public void TestInitialize()
        {
            client = CreateClient();
        }

        private static IDiscoveryOracle CreateClient()
        {
            return ChannelFactory<IDiscoveryOracle>.CreateChannel(
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/discovery"));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var co = (ICommunicationObject)client;
            if (co.State == CommunicationState.Opened)
            {
                co.Close();
            }
            else
            {
                co.Abort();
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var result = client.Ask("something");
            result.ShouldNotBeNull();
        }
    }
}
