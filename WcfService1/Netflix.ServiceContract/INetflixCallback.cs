using Netflix.DataContracts;
using System.ServiceModel;

namespace Netflix.ServiceContract
{
    [ServiceContract]
    public interface INetflixCallback
    {
        [OperationContract(IsOneWay = true)]
        void Result(Title[] titles);

        [OperationContract(IsOneWay = true)]
        void Done();
    }
}