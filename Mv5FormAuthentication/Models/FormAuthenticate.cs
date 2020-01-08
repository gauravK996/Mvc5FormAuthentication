using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Mv5FormAuthentication.Models
{
    public class CustomFormAuthenticate
    {

        public void CheckAuth()
        {
            var v = FormsAuthentication.GetAuthCookie("gauravk", false);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(v.Value);
            var name = ticket.Name;
        }
    }
}