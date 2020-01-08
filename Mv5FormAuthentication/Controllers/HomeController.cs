using Mv5FormAuthentication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PayPal.Api;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Mv5FormAuthentication.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            FormsAuthentication.SetAuthCookie("gauravk", false);
            FormsAuthentication.GetAuthCookie("gauravk", false);
            var Vk = User.Identity.Name;
            var vvvvv=User.Identity.AuthenticationType;
            
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult About()
        {
            var Data = "";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:65356/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "gkk");
                var PostRequest = client.GetAsync("Api/Student/1");
                PostRequest.Wait();
                var Result = PostRequest.Result;
                if (Result.IsSuccessStatusCode)
                {
                    var ResultData = Result.Content.ReadAsAsync<string>();
                    ResultData.Wait();
                     Data = ResultData.Result;           
                }


            }
            //ViewBag.Message = "Your application description page.";
            //var v = FormsAuthentication.GetAuthCookie("gauravk", false);
            //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(v.Value);
            //var va = ticket.Name;

            //CustomFormAuthenticate customFormAuthenticate = new CustomFormAuthenticate();
            //customFormAuthenticate.CheckAuth();


            return View();
        }

        public ActionResult Contact()
        {
            APIContext apiContext = Configurationss.GetAPIContext();

            var payout = new Payout
            {
                sender_batch_header = new PayoutSenderBatchHeader
                {
                    sender_batch_id = "batch_" + System.Guid.NewGuid().ToString().Substring(0, 8),
                    email_subject = "You have a payment"
                },
                items = new List<PayoutItem>
                {
                    new PayoutItem
                    {
                        recipient_type = PayoutRecipientType.EMAIL,
                        amount = new Currency
                        {
                            value = "0.99",
                            currency = "USD"
                        },
                        receiver = "sb-qgrtv614211@business.example.com",
                        note = "Thank you.",
                        sender_item_id = "item_1"
                    },
                    new PayoutItem
                    {
                        recipient_type = PayoutRecipientType.EMAIL,
                        amount = new Currency
                        {
                            value = "0.90",
                            currency = "USD"
                        },
                        receiver = "sb-qgrtv614211@business.example.com",
                        note = "Thank you.",
                        sender_item_id = "item_2"
                    },
                    new PayoutItem
                    {
                        recipient_type = PayoutRecipientType.EMAIL,
                        amount = new Currency
                        {
                            value = "2.00",
                            currency = "USD"
                        },
                        receiver = "sb-4d3bq611367@personal.example.com",
                        note = "Thank you.",
                        sender_item_id = "item_3"
                    }
                }
            };
            try
            {
                var createdPayout = payout.Create(apiContext, false);
            }
            catch (PayPal.PayPalException ex)
            {
                var v = ex;
            }
            return View();
        } 

        
        [HttpPost]
        public ActionResult Login(string UserName,string Password)
        {
            if (UserName == "GK" && Password == "sanju")
            {
                User user = new User();
                user.Username = UserName;
                user.Password = Password;
                var userdata = JsonConvert.SerializeObject(user);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                (
                    2, UserName, DateTime.Now, DateTime.Now.AddMinutes(14400), false, userdata
                );
                string Encryptdata = FormsAuthentication.Encrypt(ticket);

                HttpCookie httpCookie = new HttpCookie("Tech", Encryptdata);
                Response.Cookies.Add(httpCookie);
                //FormsAuthentication.(ticket)
            }
            HttpCookie httpCookie2 = new HttpCookie("Studentsss");
            httpCookie2.Value = "Hello";
            Response.Cookies.Add(httpCookie2);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public int createorder()
        {
            int orderID = 21;
            return orderID;
        }
    }
}