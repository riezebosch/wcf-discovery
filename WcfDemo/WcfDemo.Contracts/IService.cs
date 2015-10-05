using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfDemo.Contracts
{
    [ServiceContract(Namespace = "urn:www-infosupport-com:wcfdemo:v1")]
    public interface IService
    {
        [OperationContract]
        void Ping();

        [OperationContract]
        string Hello(string input);

        [OperationContract]
        Dataz GiveMeAllTheDataz(Data data);
    }
}
