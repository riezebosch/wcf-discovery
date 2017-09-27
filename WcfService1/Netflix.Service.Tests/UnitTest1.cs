using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Netflix.Service.Tests
{
    [TestClass]
    public class UnitTest1
    {
        ServiceHost host = OpenHost();

        [TestCleanup]
        public void TestCleanup()
        {
            host.Close();
        }

        [TestMethod]
        public void Top10MeestBekekenTitels()
        {
            ISpotifyService channel = InitializeChannel();
            channel.GetTitles().ShouldBe(new[] { "Quattro Stagioni" });

            ((IDisposable)channel).Dispose();
        }

        private static ISpotifyService InitializeChannel()
        {


            var factory = new ChannelFactory<ISpotifyService>(
                new NetNamedPipeBinding(NetNamedPipeSecurityMode.None),
                new EndpointAddress("net.pipe://localhost/spotify")
                );
            var channel = factory.CreateChannel();
            return channel;
        }

        private static ServiceHost OpenHost()
        {
            var host = new ServiceHost(typeof(SpotifyService));
            host
                .AddServiceEndpoint(
                    typeof(ISpotifyService),
                    new NetNamedPipeBinding(
                        NetNamedPipeSecurityMode.None),
                    "net.pipe://localhost/spotify");
            host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
            host.Open();

            return host;
        }

        [TestMethod]
        public void WatNouAlsErOpDeServiceEenExceptionOptreed()
        {
            var channel = InitializeChannel();
            Should.Throw<FaultException>(() => channel.ThrowException());

            ((ICommunicationObject)channel).Abort();
        }

        [TestMethod]
        public void WatNouAlsIkVerwachteFountenWilDelenMetMijnClient()
        {
            var channel = InitializeChannel();
            try
            {
                var ex = Should.Throw<FaultException<SubscriberDetails>>(() => ((ISpotifyService)channel).ThrowYouAreNotASubscriberException());
                ex.Detail.Data.ShouldBe("hoi");
            }
            finally
            {
                ((ICommunicationObject)channel).Abort();
            }

            
        }
    }
}
