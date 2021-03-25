using FSDP.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace FSDP.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                string body = $"{cvm.FirstName} {cvm.LastName} has sent you the following message: <br />" + $"{cvm.Message} <strong>from the email address:</strong> {cvm.Email}";

                MailMessage m = new MailMessage("admin@davidmuolo.com", "davidmuolo1@gmail.com", cvm.Subject, body);

                //Allows for br and strong HTML formatting in email
                m.IsBodyHtml = true;

                m.Priority = MailPriority.High;

                m.ReplyToList.Add(cvm.Email);

                SmtpClient client = new SmtpClient("mail.davidmuolo.com");

                client.Credentials = new NetworkCredential("admin@davidmuolo.com", "Stubble848!");

                try
                {
                    client.Send(m);
                }
                catch (Exception e)
                {
                    ViewBag.Message = e.StackTrace;
                }
                return View("EmailConfirmation");
            }
            //If does not pass model validation, will return view with cvm objet to repopulate form
            return View(cvm);
        }
    }
}
