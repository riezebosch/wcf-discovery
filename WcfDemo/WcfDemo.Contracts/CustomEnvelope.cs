using System.ServiceModel;

namespace WcfDemo.Contracts
{
    [MessageContract]
    public class CustomEnvelope
    {
        [MessageHeader]
        public int Value { get; set; }

        [MessageBodyMember]
        public string Description { get; set; }
    }
}