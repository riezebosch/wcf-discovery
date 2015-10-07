using System.Runtime.Serialization;

namespace WcfDemo.Contracts
{
    [DataContract(Namespace = Constants.Namespace)]
    public class Data
    {
        [DataMember]
        public int ZId { get; set; }

        [DataMember(Order = 25, EmitDefaultValue = false, IsRequired = true)]
        public string Ignored { get; set; }

        [DataMember(Order = 26, EmitDefaultValue = true, IsRequired = false)]
        public decimal? Amount { get; set; }
    }
}