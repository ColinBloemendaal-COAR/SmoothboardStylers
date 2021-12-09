using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Smoothboard_Stylers.Data;
using Smoothboard_Stylers.Factories;
using Smoothboard_Stylers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Controllers
{
    public class NewsletterController : Controller
    {
        public readonly Smoothboard_StylersContext _context;

        public NewsletterController(Smoothboard_StylersContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddSubscriber(string Name, string Email)
        {
            _context.NewsletterSubscribers.Add(new NewsletterSubscriber() { Name = Name, Email = Email });
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
