using System.ServiceModel;

namespace DiscoveryDemo.Contract
{
    [ServiceContract(Namespace = "urn:www-infosupport-com:discovery-demo")]
    public interface IMagicOracle
    {
        [OperationContract]
        string Answer(string question);
    }
}
