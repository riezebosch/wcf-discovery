using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using DiscoveryDemo.Contract;

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

        [TestInitialize]
        public void TestInit()
        {
            var factory = new ChannelFactory<IMagicOracle>(
                new NetNamedPipeBinding());
            client = factory.CreateChannel();
        }

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
            var result = client.Answer("Hoe laat is het?");
            Console.WriteLine(result);
        }
    }
}
