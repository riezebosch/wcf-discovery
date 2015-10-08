using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfDemo.Service.Tests
{
    public class UserNamePasswordValidator 
        : System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName != "Manuel" || password != "secure")
            {
                throw new Exception();
            }
        }
    }
}
