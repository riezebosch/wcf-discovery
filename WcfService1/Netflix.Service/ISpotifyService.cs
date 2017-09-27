using System.ServiceModel;

namespace Netflix.Service
{
    [ServiceContract]
    public interface ISpotifyService
    {
        [OperationContract]
        string[] GetTitles();

        [OperationContract]
        void ThrowException();
        [OperationContract]
        [FaultContract(typeof(SubscriberDetails))]
        void ThrowYouAreNotASubscriberException();
    }
}