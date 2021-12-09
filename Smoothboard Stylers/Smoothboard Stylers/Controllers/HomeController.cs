using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smoothboard_Stylers.Data;
using Smoothboard_Stylers.Factories;
using Smoothboard_Stylers.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Smoothboard_StylersContext _context;
        public readonly MailTaskFactory _mailTaskFactory;

        public HomeController(ILogger<HomeController> logger, Smoothboard_StylersContext context, MailTaskFactory mailTaskFactory)
        {
            _logger = logger;
            _context = context;
            _mailTaskFactory = mailTaskFactory;
        }

        public IActionResult Index()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult RedirectTest()
        {
            TempData["ec"] = MyExtentions.GetEc("TestS");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Contact()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }

            return View();
        }

        [HttpPost]
        public IActionResult ContactMail(string firstname, string lastname, string email, string phone, string message)
        {
            if(string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(message))
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Contact", "Home");
            }

            string msg = "Dear Mr/Mrs, \n You recieved a mail from Smoothboard Stylers by " + firstname + " " + lastname + ". \n Email: " + email + "\n Tel: " + phone + "\n " + message;

            _mailTaskFactory.CreateCustomMailTasks("Smoothboard Stylers Website Contact", msg, new MailAddress("colin.bloemendaal@gmail.com"), false);

            TempData["ec"] = MyExtentions.GetEc("ContactFormSend");
            return RedirectToAction("Contact", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
