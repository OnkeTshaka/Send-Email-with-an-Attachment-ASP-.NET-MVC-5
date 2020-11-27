using Email.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Email.Controllers
{
    public class SendMailController : Controller
    {
        // GET: SendMail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Thanks()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(MailModel _objModelMail, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress("Enter your email address", "Enter company name");
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                if (upload != null)
                {

                    string filename = Path.GetFileName(upload.FileName);
                    mail.Attachments.Add(new Attachment(upload.InputStream, filename));
                }


                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential

                ("Enter your email address", "Enter your password");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("Thanks");
            }
            else
            {
                return View();
            }
        }
    }
}