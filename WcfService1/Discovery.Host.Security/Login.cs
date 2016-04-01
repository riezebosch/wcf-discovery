using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Host.Security
{
    public class Login : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName != "discovery" || password != "tesla")
            {
                throw new FaultException("Username or password incorrect");
            }
        }
    }
}
