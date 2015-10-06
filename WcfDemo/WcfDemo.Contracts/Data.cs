using System.Runtime.Serialization;

namespace WcfDemo.Contracts
{
    [DataContract(Namespace = "urn:www-infosupport-com:wcfdemo:v1:datacontracts")]
    public class Data
    {
        [DataMember]
        public int ZId { get; set; }

        [DataMember(Order = 25)]
        public string Ignored { get; set; }

        [DataMember(Order = 26, EmitDefaultValue = true, IsRequired = false)]
        public decimal? Amount { get; set; }
    }
}