using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using WcfDemo.Contracts;
using System.Diagnostics;
using System.Linq;
using WcfDemo.DataModel;
using System.Transactions;
using System.IO;
using System.Data.Entity;

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

            // Prevent EF from trying to auto-create the database.
            Database.SetInitializer<SchoolContext>(null);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _host.Abort();

            // Cleanup for subsequent testrun.
            LocalDbHelper.Delete();
            File.Delete("School_log.ldf");
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

        [TestMethod]
        public void BijNietCommitOpClientOokRollbackOpService()
        {
            var name = Guid.NewGuid().ToString();
            using (new TransactionScope())
            {
                client.TransactionSupported(1, name);

                using (var context = new SchoolContext())
                {
                    Assert.IsTrue(context.People.Any(p => p.FirstName == name), "De service moet gedurende de transaction natuurlijk wel wat in de database doen.");
                }
            }

            using (var context = new SchoolContext())
            {
                Assert.IsFalse(context.People.Any(p => p.FirstName == name), "Blijkbaar werkt de transaction nog niet want de data staat gewoon in de database.");
            }

        }
    }
}
