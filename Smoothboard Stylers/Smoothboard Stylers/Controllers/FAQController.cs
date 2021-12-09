using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Smoothboard_Stylers.Areas.Identity.Data;
using Smoothboard_Stylers.Data;
using Smoothboard_Stylers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Controllers
{
    public class FAQController : Controller
    {
        private readonly Smoothboard_StylersContext _context;

        public FAQController(Smoothboard_StylersContext context)
        {
            _context = context;
        }

        public IActionResult FAQ()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }
            int page = 1;
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["Page"]))
                page = Int32.Parse(HttpContext.Request.Query["Page"]);


            var FAQs = _context.FAQs.GetPaged(page, 15);
            if (FAQs == null)
            {
                TempData["ec"] = MyExtentions.GetEc("FAQEmpty");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.CurrentPage = page;
            return View(FAQs);
        }
    }
}
