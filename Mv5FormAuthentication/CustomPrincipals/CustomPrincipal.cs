using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Mv5FormAuthentication.CustomPrincipals
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(string UserName)
        {
            Identity = new GenericIdentity(UserName);
        }
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}