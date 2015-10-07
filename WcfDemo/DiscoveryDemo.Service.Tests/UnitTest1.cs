using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using DiscoveryDemo.Contract;
using System.ServiceModel.Discovery;

namespace DiscoveryDemo.Service.Tests
{
    [TestClass]
    public class UnitTest1
    {
        static ServiceHost _host = new ServiceHost(
            typeof(MagicOracleService), 
            new Uri("net.pipe://localhost/magic-oracle"));

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _host.Open();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _host.Abort();
        }

        IMagicOracle client;

        [TestCleanup]
        public void TestCleanup()
        {
            ICommunicationObject co = (ICommunicationObject)client;
            if (co.State == CommunicationState.Opened)
            {
                try
                {
                    co.Close();
                }
                catch (CommunicationException)
                {
                    co.Abort();
                }
                catch (TimeoutException)
                {
                    co.Abort();
                }
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var disco = new DiscoveryClient(new UdpDiscoveryEndpoint());
            var response = disco.Find(
                new FindCriteria(typeof(IMagicOracle))
                {
                    Duration = TimeSpan.FromSeconds(2)
                });

            Assert.AreNotEqual(0, response.Endpoints.Count);

            client = ChannelFactory<IMagicOracle>.CreateChannel(
                new NetNamedPipeBinding(),
                response.Endpoints[0].Address);

            var result = client.Answer("Hoe laat is het?");
            Console.WriteLine(result);
        }
    }
}
