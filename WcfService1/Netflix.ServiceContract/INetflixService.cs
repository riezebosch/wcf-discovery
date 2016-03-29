using System.ServiceModel;

namespace Netflix.ServiceContract
{
    [ServiceContract]
    public interface INetflixService
    {
        [OperationContract]
        string[] Top10();
    }
}