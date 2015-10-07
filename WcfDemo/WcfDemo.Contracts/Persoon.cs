using System.Runtime.Serialization;

namespace WcfDemo.Contracts
{
    [DataContract(Namespace = Constants.Namespace)]
    [KnownType(typeof(Student))]
    public class Persoon
    {
        [DataMember]
        public string Naam { get; set; }
    }
}