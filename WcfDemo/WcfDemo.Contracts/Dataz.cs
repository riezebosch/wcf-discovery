using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfDemo.Contracts
{
    [DataContract(Namespace = Constants.Namespace)]
    public class Dataz
    {
        [DataMember]
        public List<Data> Ids { get; set; }
    }
}