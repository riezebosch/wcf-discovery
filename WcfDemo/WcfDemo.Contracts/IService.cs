using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfDemo.Contracts
{
    [ServiceContract(Namespace = "urn:www-infosupport-com:wcfdemo:v1", Name = "ping-service")]
    public interface IService
    {
        [OperationContract(Name = "ping")]
        void Ping();

        [OperationContract]
        string Hello(string input);

        [OperationContract]
        Dataz GiveMeAllTheDataz(Data data);

        [OperationContract]
        void Throw();

        [OperationContract]
        void Put(Guid data);

        [OperationContract]
        Guid Get();
    }
}
