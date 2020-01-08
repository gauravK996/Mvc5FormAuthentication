using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mv5FormAuthentication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            var v = FormsAuthentication.GetAuthCookie("gauravk", false);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(v.Value);
            filters.Add(new HandleErrorAttribute());
        }
    }
}
