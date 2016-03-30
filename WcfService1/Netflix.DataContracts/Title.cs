using System.Runtime.Serialization;

namespace Netflix.DataContracts
{
    [DataContract(Namespace = Constants.Namespace, IsReference = true)]
    [KnownType(typeof(Movie))]
    [KnownType(typeof(Serie))]
    public class Title
    {
    }
}