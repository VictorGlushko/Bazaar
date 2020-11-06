using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Bazaar.Domain.Dtos;
using Bazaar.Domain.Entities;
using Bazaar.Domain.ViewModel;
using Bazaar.Repository;

namespace Bazaar.Controllers
{
    public class FaqController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public FaqController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // GET: Faq
        public ActionResult Index()
        {
            var faqItemViewModels = _unitOfWork.FaqItem.GetFaqItems().Select(Mapper.Map<FaqItem, FaqItemViewModel>).OrderBy(f =>f.Order);

            FaqPageViewModel viewModel = new FaqPageViewModel();
            viewModel.FaqItemViewModels = faqItemViewModels;


            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("recipient@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("sender@outlook.com");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, contactForm.Name, contactForm.Email, contactForm.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "user@outlook.com",  // replace with valid value
                        Password = "password"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.SendMailAsync(message);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}