using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WcfDemo.Service.Tests
{
    public class TurnIdentityIntoPrincipalAuthorizationPolicy
        : IAuthorizationPolicy
    {
        public string Id => "N/A";

        public ClaimSet Issuer => ClaimSet.System;

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            evaluationContext.Properties["Principal"] = new GenericPrincipal(GetClientIdentity(evaluationContext), new string[] { });
            return true;
        }

        private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new Exception("No Identity found");

            return identities[0];
        }
    }
}
