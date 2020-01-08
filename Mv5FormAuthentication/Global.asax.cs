using Mv5FormAuthentication.CustomPrincipals;
using Mv5FormAuthentication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Mv5FormAuthentication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs args)
        {
            HttpCookie cookie = Request.Cookies["Tech"];
            if (cookie != null)
            {
                FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthentication.Decrypt(cookie.Value);
                //formsAuthenticationTicket.UserData
                var Logindata = JsonConvert.DeserializeObject<User>(formsAuthenticationTicket.UserData);
                CustomPrincipal principal = new CustomPrincipal(Logindata.Username);
                HttpContext.Current.User = principal;
                
            }
        }
        protected void Application_Error(object Sender, EventArgs eventArgs)
        {
            var Error = Server.GetLastError();

        }

        protected void Application_BeginRequest(object Sender, EventArgs eventArgs)
        {

        }
    }
}
