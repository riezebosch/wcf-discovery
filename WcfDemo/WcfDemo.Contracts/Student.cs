using System.Runtime.Serialization;

namespace WcfDemo.Contracts
{
    [DataContract(Namespace = Constants.Namespace)]
    public class Student : Persoon
    {
        public int StudentNummer { get; set; }
    }
}