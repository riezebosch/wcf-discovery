using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfDemo.Contracts
{
    [DataContract]
    public class Dataz
    {
        [DataMember]
        public List<int> Ids { get; set; }
    }
}