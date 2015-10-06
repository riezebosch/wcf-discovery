using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using WcfDemo.Contracts;

namespace WcfDemo.Service.Tests
{
    [TestClass]
    public class UnitTest1
    {
        static ServiceHost _host = new ServiceHost(typeof(PingService));

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // Dit is nu een beetje dubbelop, 
            // want dit hebben we ook al in config staan.
            _host
                .Description
                .Behaviors
                .Find<ServiceBehaviorAttribute>()
                .IncludeExceptionDetailInFaults = true;
            _host.Open();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _host.Abort();
        }

        [TestMethod]
        public void IntegratieTestDemoVanService()
        {
            var client = ChannelFactory<IService>.CreateChannel(
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/ping"));

            var result = client.Hello("from a unit test");
            Assert.AreEqual("Hello from a unit test", result);
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
            var client = ChannelFactory<IService>.CreateChannel(
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/ping"));

            try
            {
                client.GiveMeAllTheDataz(new Data { Amount = 1, Ignored = "", ZId = 3 });
            }
            finally
            {
                if (((ICommunicationObject)client).State == CommunicationState.Opened)
                {
                    ((ICommunicationObject)client).Close();
                }
            }
        }

        [TestMethod]
        public void ServiceThrowsException()
        {
            var client = ChannelFactory<IService>.CreateChannel(
                           new NetNamedPipeBinding(),
                           new EndpointAddress("net.pipe://localhost/ping"));

            try
            {
                client.Throw();
                Assert.Fail("tot zover komt de test niet.");
            }
            catch (FaultException ex)
            {
                StringAssert.Contains(ex.Message, "aangeroepen");
                StringAssert.Contains(ex.Message, "moeten");
                StringAssert.Contains(ex.Message, "mogen");
                StringAssert.Contains(ex.Message, "worden");
            }
            finally
            {
                if (((ICommunicationObject)client).State == CommunicationState.Opened)
                {
                    ((ICommunicationObject)client).Close();
                }
            }
        }
    }
}
