using System.Runtime.Serialization;

namespace WcfDemo.Contracts
{
    [DataContract]
    public class CustomFaultDetails
    {
        [DataMember]
        public string Bericht { get; set; }
    }
}