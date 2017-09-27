using System.Runtime.Serialization;

namespace Netflix.Service
{
    [DataContractAttribute]
    public class SubscriberDetails
    {
        [DataMember]
        public string Data { get; internal set; }
    }
}