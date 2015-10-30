using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using EaaaWebDeveloper.Models;
using System.Net.Mail;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace EaaaWebDeveloper.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        
        public ActionResult Index()
        {

            return PartialView("ContactForm", new ContactForm());
        }
        

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (ModelState.IsValid) {

                // Parameters – name, parentId, contentTypeAlias
                IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "MailComment");
                // assign values
                comment.SetValue("name", model.Name);
                comment.SetValue("email", model.Email);
                comment.SetValue("subject", model.Subject);
                comment.SetValue("message", model.Message);

                // save to Umbraco
                Services.ContentService.Save(comment);
                // Services.ContentService.SaveAndPublishWithStatus(comment);


                MailMessage message = new MailMessage();

                message.To.Add("Moeller120589@gmail.com");
                message.Subject = model.Subject;
                message.From = new MailAddress(model.Email, model.Name);
                message.Body = model.Message + "\n My Mail is: " + model.Email;

                using (SmtpClient smtp = new SmtpClient())
                {

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network; smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.Com"; smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("Moeller120589@gmail.com", "");
                    smtp.EnableSsl = true;

                    TempData["success"] = true;
                }

                return RedirectToCurrentUmbracoPage();


            } else { return CurrentUmbracoPage(); }

            
            
        }
    }

    
}