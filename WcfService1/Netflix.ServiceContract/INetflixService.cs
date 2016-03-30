using Netflix.DataContracts;
using System;
using System.ServiceModel;

namespace Netflix.ServiceContract
{
    [ServiceContract(Namespace = "urn:www-netflix-com:wcf-demo",
        CallbackContract = typeof(INetflixCallback))]
    public interface INetflixService
    {
        [OperationContract]
        Title[] Top10();

        [OperationContract]
        void SetState(Guid data);

        [OperationContract]
        Guid GetData();

        [OperationContract]
        void Slow();

        [OperationContract]
        [FaultContract(typeof(NetflixFault))]
        void Throw();

        [OperationContract(IsOneWay = true)]
        void Search();
    }
}