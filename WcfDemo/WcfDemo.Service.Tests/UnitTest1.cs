using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using WcfDemo.Contracts;
using System.Diagnostics;

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

        IService client;
        ClientUpdateCallback callback;

        [TestInitialize]
        public void TestInit()
        {
            callback = new ClientUpdateCallback();
            var factory = new DuplexChannelFactory<IService>(callback,
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/ping"));
            client = factory.CreateChannel();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            callback.Dispose();

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
                catch(TimeoutException)
                {
                    co.Abort();
                }
            }
        }

        [TestMethod]
        public void IntegratieTestDemoVanService()
        {

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
        [ExpectedException(typeof(CommunicationException))]
        public void DemoVanFalendeIntegratieTestOmdatServiceNietConformWcfWerkt()
        {
            client.GiveMeAllTheDataz(new Data { Amount = 1, Ignored = "", ZId = 3 });
        }

        [TestMethod]
        public void ServiceThrowsException()
        {
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
        }

        [TestMethod]
        public void MakeSureInstanceContextModeIsPerCall()
        {
            var data = Guid.NewGuid();
            client.Put(data);

            var result = client.Get();
            Assert.AreNotEqual(data, result);
        }

        [TestMethod]
        public void ValidateInstanceContextModeOnServiceHost()
        {
            Assert.AreEqual(InstanceContextMode.PerCall,
                _host.Description.Behaviors.Find<ServiceBehaviorAttribute>().InstanceContextMode);
        }

        [TestMethod]
        public void EenFoutmeldingVanuitDeServiceDoorsturenNaarDeClient()
        {
            try
            {
                client.ThrowCustomException();
                Assert.Fail("dit punt had niet bereikt mogen worden.");
            }
            catch (FaultException<CustomFaultDetails> ex)
            {
                StringAssert.Contains(ex.Detail.Bericht, "dat mag dus niet");
            }
        }

        [TestMethod]
        public void OneWayMessagePatternIsNotBlocking()
        {
            Stopwatch sw = Stopwatch.StartNew();
            client.Slow();

            Assert.IsTrue(sw.Elapsed < TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        [Timeout(15000)]
        public void DuplexMessagePatternDemoReceiveUpdatesFromService()
        {
            client.StartProcessing();

            // Wait for the service to finish.
            callback.Wait();

            Assert.AreEqual(100, callback.Progress);
        }

        [TestMethod]
        public void InheritanceInDataContracts()
        {
            Persoon p = new Student { Naam = "Pietje", StudentNummer = 213 };
            client.Save(p);
        }
    }
}
