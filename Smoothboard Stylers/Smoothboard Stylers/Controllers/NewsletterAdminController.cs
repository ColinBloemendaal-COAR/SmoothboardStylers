using Microsoft.AspNetCore.Authorization;
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
    [Route("Admin/Newsletters/")]
    [Authorize(Roles = "Admin")]
    public class NewsletterAdminController : Controller
    {
        public readonly Smoothboard_StylersContext _context;
        public readonly MailTaskFactory _mailTaskFactory;

        public NewsletterAdminController(Smoothboard_StylersContext context, MailTaskFactory mailTaskFactory)
        {
            _context = context;
            _mailTaskFactory = mailTaskFactory;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
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

            var newsletters = _context.Newsletters.GetPaged(page, 20);
            if (newsletters == null)
            {
                TempData["ec"] = MyExtentions.GetEc("ArticlesEmpty");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

            ViewBag.CurrentPage = page;
            return View("~/Views/Admin/Newsletters/Index.cshtml", newsletters);
        }
        
        [Route("Add")]
        public IActionResult Add()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }
            return View("~/Views/Admin/Newsletters/Add.cshtml");
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create(string Subject, string Text, bool Send, bool IsHtml)
        {
            // Check if all form values have been filled
            if (string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(Text))
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

            Newsletter n = new Newsletter()
            {
                Subject = Subject,
                Text = Text,
                IsHtml = IsHtml
            };

            _context.Newsletters.Add(n);
            _context.SaveChanges();

            if(Send)
            {
                return RedirectToAction("Send", "NewsletterAdmin", new { NewsletterId = n.Id });
            } else
            {
                TempData["ec"] = MyExtentions.GetEc("NewsLetterAdded");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

        }

        [Route("Edit")]
        public IActionResult Edit()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }

            int newsletterId = 0;
            if (string.IsNullOrEmpty(HttpContext.Request.Query["NewsletterId"]))
            {
                TempData["ec"] = MyExtentions.GetEc("ArticleDoesntExists");
                return RedirectToAction("Index", "NewsletterAdmin");
            }
            bool IsInt = Int32.TryParse(HttpContext.Request.Query["NewsletterId"], out newsletterId);
            if (!IsInt)
            {
                TempData["ec"] = MyExtentions.GetEc("NotAValidArticle");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

            var tempA = _context.Newsletters.Where(x => x.Id == newsletterId);
            if (!tempA.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("ArticleDoesntExists");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

            Newsletter nl = tempA.First();

            ViewBag.Newsletter = nl;
            return View("~/Views/Admin/Newsletters/Edit.cshtml");
        }

        [Route("Update")]
        [HttpPost]
        public IActionResult Update(int NewsletterId, string Subject, string Text, bool Send)
        {
            if (NewsletterId == 0 || string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(Text))
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

            Newsletter n = new Newsletter()
            {
                Id = NewsletterId,
                Subject = Subject,
                Text = Text
            };

            _context.Newsletters.Update(n);
            _context.SaveChanges();

            if(Send)
            {
                return RedirectToAction("Send", "NewsletterAdmin", new { NewsletterId = NewsletterId });
            } else
            {
                TempData["ec"] = MyExtentions.GetEc("ArticleUpdated");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

        }

        [Route("Delete")]
        public IActionResult Delete()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }

            int NewsletterId = 0;
            if (string.IsNullOrEmpty(HttpContext.Request.Query["NewsletterId"]))
            {
                TempData["ec"] = MyExtentions.GetEc("ArticleDoesntExists");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

            bool IsInt = Int32.TryParse(HttpContext.Request.Query["NewsletterId"], out NewsletterId);
            if (!IsInt)
            {
                TempData["ec"] = MyExtentions.GetEc("NotAValidArticle");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

            ViewBag.NewsletterId = NewsletterId;
            return View("~/Views/Admin/Newsletters/Delete.cshtml");
        }

        [HttpPost]
        public IActionResult ConfirmDeletetion(int NewsletterId)
        {
            if (NewsletterId == 0)
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Index", "NewsletterAdmin");
            }

            _context.RemoveRange(_context.Newsletters.Where(x => x.Id == NewsletterId));
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("ArticleDeleted");
            return RedirectToAction("Index", "NewsletterAdmin");
        }

        [Route("Send")]
        public IActionResult Send(int NewsletterId)
        {
            _mailTaskFactory.CreateNewsletterTasks(_context.Newsletters.First(x => x.Id == NewsletterId));
            return RedirectToAction("Index", "NewsletterAdmin");
        }
    }
}
