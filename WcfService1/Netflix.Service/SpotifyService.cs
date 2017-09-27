using System.ServiceModel;

namespace Netflix.Service
{
    public class SpotifyService : ISpotifyService
    {
        public string[] GetTitles()
        {
            return new[]
            {
                "Quattro Stagioni"
            };
        }

        public void ThrowException()
        {
            throw new System.NotImplementedException();
        }

        public void ThrowYouAreNotASubscriberException()
        {
            throw new FaultException<SubscriberDetails>(new SubscriberDetails { Data = "hoi" });
        }
    }
}