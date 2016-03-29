using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.ServiceModel;
using Netflix.ServiceContract;
using System.Linq;

namespace Netflix.Service.Tests
{
    [TestClass]
    public class UnitTest1
    {
        static ServiceHost host;

        [ClassInitialize]
        public static void Initialize(TestContext unused)
        {
            host = new ServiceHost(typeof(NetflixService));
            host.AddServiceEndpoint(typeof(INetflixService),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/netflix");

            host.Open();
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            host.Abort();
        }


        INetflixService client;
        [TestInitialize]
        public void TestInitialize()
        {
            client = ChannelFactory<INetflixService>.CreateChannel(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/netflix"));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ((ICommunicationObject)client).Close();
        }

        [TestMethod]
        public void Top10MeestBekekenTitels()
        {
            client.Top10().Length.ShouldBe(10);
        }

        [TestMethod]
        public void ServiceContractNamespaceMoetEenWaardeHebben()
        {
            var contract = typeof(INetflixService);
            var attr = contract
                .GetCustomAttributes(typeof(ServiceContractAttribute), true)
                .Cast<ServiceContractAttribute>()
                .FirstOrDefault();

            attr.ShouldNotBeNull("Geen service contract attribute gevonden");
            attr.Namespace.ShouldNotBeNull();
        }

        [TestMethod]
        public void ServiceContractNamespaceMoetEenWaardeHebben_TestOpServiceHost()
        {
            host.Description.Namespace.ShouldNotBe("http://tempuri.org/");
        }
    }
}
