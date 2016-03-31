using System.ServiceModel;

namespace Discovery.Proxy.Tests
{
    [ServiceContract]
    internal class DummyService
    {
        [OperationContract]
        public void Foo()
        {
        }
    }
}