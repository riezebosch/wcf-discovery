using System.Runtime.Serialization;

namespace Netflix.DataContracts
{
    [DataContract(Namespace = Constants.Namespace)]
    public class Serie : Title
    {
        [DataMember]
        public Episode[] Episodes { get; set; }
    }
}