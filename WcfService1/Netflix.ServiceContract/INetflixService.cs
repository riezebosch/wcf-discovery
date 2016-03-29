using System;
using System.ServiceModel;

namespace Netflix.ServiceContract
{
    [ServiceContract(Namespace = "urn:www-netflix-com:wcf-demo")]
    public interface INetflixService
    {
        [OperationContract]
        string[] Top10();

        [OperationContract]
        void SetState(Guid data);

        [OperationContract]
        Guid GetData();
    }
}