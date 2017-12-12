using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebConsume.Models;
using System.Net;
using System.Net.Mail;
namespace WebConsume.Controllers
{
    public class EmaiSetupController : Controller
    {
        // GET: EmaiSetup
        public ActionResult Mail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Mail(WebConsume.Models.gmail model)
        {
            MailMessage mm = new MailMessage("rania.mhadhbi@esprit.tn", model.To);
            mm.Subject = model.Subject;
            mm.Body = model.Body;
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("rania.mhadhbi@esprit.tn", "R09811805");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "Mail Has Been Sent Successfully";

            return View();
        }
    }
}