using System.Runtime.Serialization;

namespace WcfService1
{
    [DataContract]
    public class MyData
    {
        [DataMember]
        public int MyProperty { get; set; }

        public int DezeDusNiet { get; set; }

    }
}