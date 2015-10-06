using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfDemo.Contracts
{
    [DataContract(Namespace = "urn:www-infosupport-com:wcfdemo:v1:datacontracts")]
    public class Dataz
    {
        [DataMember]
        public List<int> Ids { get; set; }
    }
}