using System.Runtime.Serialization;

namespace Netflix.DataContracts
{
    [DataContract(Namespace = Constants.Namespace)]
    [KnownType(typeof(Movie))]
    public class Title
    {
    }
}