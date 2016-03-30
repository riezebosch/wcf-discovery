using System.Runtime.Serialization;

namespace Netflix.DataContracts
{
    [DataContract(Namespace = Constants.Namespace)]
    public class Episode : Title
    {
        [DataMember]
        public Serie Serie { get; set; }
    }
}