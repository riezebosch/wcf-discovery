using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.ServiceModel;
using Netflix.ServiceContract;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Netflix.DataContracts;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml;

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
            client = CreateClient();
        }

        private static INetflixService CreateClient()
        {
            return ChannelFactory<INetflixService>.CreateChannel(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/netflix"));
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
            host.Description.Endpoints.ShouldAllBe(c => c.Contract.Namespace != "http://tempuri.org/");
        }

        [TestMethod]
        public void WatIsDeStandaardInstanceContextMode()
        {
            var data = Guid.NewGuid();
            client.SetState(data);

            Guid result = client.GetData();
            result.ShouldNotBe(data);
        }

        [TestMethod]
        public void MetDeInstanceContextModeOp_PerCall_EnConcurrencyModeOp_Single_NogSteedsMeerdereRequestsTegelijk()
        {
            var sw = Stopwatch.StartNew();

            Task.WaitAll(
                InvokeSlow(),
                InvokeSlow()
            );

            sw.Elapsed.ShouldBeGreaterThan(TimeSpan.FromSeconds(5));
            sw.Elapsed.ShouldBeLessThan(TimeSpan.FromSeconds(10));
        }

        [TestMethod]
        public void MetServiceThrottlingOp2MoetDeDerdeWachten()
        {
            var sw = Stopwatch.StartNew();

            Task.WaitAll(
                InvokeSlow(),
                InvokeSlow(),
                InvokeSlow()
            );

            sw.Elapsed.ShouldBeGreaterThan(TimeSpan.FromSeconds(10));
            sw.Elapsed.ShouldBeLessThan(TimeSpan.FromSeconds(15));
        }

        private static Task InvokeSlow()
        {
            return Task.Run(() =>
            {
                var client1 = CreateClient();
                client1.Slow();
            });
        }

        [TestMethod]
        public void WatGebeurtMetEenErrorOpDeService()
        {
            Should.Throw<FaultException<NetflixFault>>(() => client.Throw());
        }

        [TestMethod]
        public void DataContractNamespaceMoetEenWaardeHebben()
        {
            var types = typeof(NetflixFault).Assembly.GetTypes();
            types.ShouldAllBe(t => ContractsHaveNamespace(t));
        }

        private static bool ContractsHaveNamespace(Type t)
        {
            return t.GetCustomAttributes(typeof(DataContractAttribute), true)
                    .Cast<DataContractAttribute>()
                    .All(dc => dc.Namespace != null);
        }

        [TestMethod]
        public void AfgeleideClassesOnderdeelVanHetContractMaken()
        {
            IEnumerable<Title> titles = client.Top10();
            titles.ShouldContain(t => t is Movie);
        }

        [TestMethod]
        public void GraphsInJeDataContract()
        {
            var result = TestSerialize(new NetflixService().Top10());
            Console.WriteLine(result);

            IEnumerable<Title> titles = client.Top10();
            var serie = titles.OfType<Serie>().FirstOrDefault();

            serie.ShouldNotBeNull();
            serie.Episodes.ShouldNotBeNull();

            var episode = serie.Episodes.FirstOrDefault();
            episode.ShouldNotBeNull();

            // Testen van back reference.
            episode.Serie.ShouldNotBeNull();
        }

        /// <summary>
        /// Data Contract Serializer to the rescue!
        /// Voor als WCF je geen heldere foutmeldingen meer geeft.            
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        private static string TestSerialize<T>(T item)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true, IndentChars = "\t" }))
                {
                    var ser = new DataContractSerializer(typeof(T));
                    ser.WriteObject(writer, item);
                }

                stream.Seek(0, SeekOrigin.Begin);
                return new StreamReader(stream).ReadToEnd();

            }
        }
    }
}
