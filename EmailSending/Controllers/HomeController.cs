using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using EmailSending.Models;

namespace EmailSending.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EpostaModel model)
        {
            string server = ConfigurationManager.AppSettings["server"];
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            bool ssl = ConfigurationManager.AppSettings["ssl"].ToString() == "1" ? true : false;

            string from = ConfigurationManager.AppSettings["from"];
            string password = ConfigurationManager.AppSettings["password"];
            string fromname = ConfigurationManager.AppSettings["fromname"];
            string to = ConfigurationManager.AppSettings["to"];
            string epostacopy = ConfigurationManager.AppSettings["epostacopy"];

            var client = new SmtpClient();
            client.Host = server;
            client.Port = port;
            client.EnableSsl = ssl;
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(from, password);

            var email = new MailMessage();
            email.From = new MailAddress(from, fromname);
            email.To.Add(to);

            if (string.IsNullOrEmpty(epostacopy) ==false)
            {
                string[] mails = epostacopy.Split(',');

                foreach (var item in mails)
                {
                    email.Bcc.Add(item);
                }
            }

            email.Subject = model.konu;
            email.IsBodyHtml = true;
            email.Body = $"ad soyad : {model.mesaj} \n konu : {model.konu} \n mesaj : {model.mesaj} \n eposta : {model.email}";

            try
            {
                client.Send(email);
                ViewData["result"] = true;
            }
            catch (Exception e)
            {
                ViewData["result"] = false;
            }

            return View();
        }
    }
}