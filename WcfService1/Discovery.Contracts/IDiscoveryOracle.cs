using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Contracts
{
    [ServiceContract(
        Name = "Oracle",
        Namespace = "urn:www-infosupport-com:wsdiscovery-demo")]
    public interface IDiscoveryOracle
    {
        [OperationContract]
        string Ask(string question);
    }
}
