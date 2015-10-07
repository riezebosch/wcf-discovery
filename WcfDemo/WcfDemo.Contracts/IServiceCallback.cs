using System.ServiceModel;

namespace WcfDemo.Contracts
{
    [ServiceContract]
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void Update(int i);

        [OperationContract(IsOneWay = true)]
        void Ready();
    }
}