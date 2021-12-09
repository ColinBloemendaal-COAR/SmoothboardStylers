using Microsoft.AspNetCore.Mvc;
using Smoothboard_Stylers.Data;
using Smoothboard_Stylers.Factories;
using Smoothboard_Stylers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Controllers
{
    public class ShopController : Controller
    {
        private readonly Smoothboard_StylersContext _context;
        public readonly MailTaskFactory _mailTaskFactory;

        public ShopController(Smoothboard_StylersContext context, MailTaskFactory mailTaskFactory)
        {
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

            int page = 1;
            if (!string.IsNullOrEmpty(HttpContext.Request.Query["Page"]))
                page = Int32.Parse(HttpContext.Request.Query["Page"]);

            var Articles = _context.Artikels.GetPaged(page, 18);
            if (Articles == null)
            {
                TempData["ec"] = MyExtentions.GetEc("ArticlesEmpty");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.CurrentPage = page;
            return View(Articles);
        }


        public IActionResult Article()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }
            int ArticleId = 0;
            if (string.IsNullOrEmpty(HttpContext.Request.Query["ArticleId"]))
            {
                TempData["ec"] = MyExtentions.GetEc("ArticleDoesntExists");
                return RedirectToAction("Index", "Shop");
            }

            bool ValidArticle = Int32.TryParse(HttpContext.Request.Query["ArticleId"], out ArticleId);
            if(!ValidArticle)
            {
                TempData["ec"] = MyExtentions.GetEc("NotAValidArticle");
                return RedirectToAction("Index", "Shop");
            }

            Artikel A = _context.Artikels.Where(x => x.Id == ArticleId).First();

            ViewBag.Article = A;
            return View();
        }


        public IActionResult Order()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }

            int ArticleId = 0;
            if (string.IsNullOrEmpty(HttpContext.Request.Query["ArticleId"]))
            {
                TempData["ec"] = MyExtentions.GetEc("ArticleDoesntExists");
                return RedirectToAction("Index", "Shop");
            }
            bool IsInt = Int32.TryParse(HttpContext.Request.Query["ArticleId"], out ArticleId);
            if (!IsInt)
            {
                TempData["ec"] = MyExtentions.GetEc("NotAValidArticle");
                return RedirectToAction("Index", "Shop");
            }

            var tempA = _context.Artikels.Where(x => x.Id == ArticleId);
            if (!tempA.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("ArticleDoesntExists");
                return RedirectToAction("Index", "Shop");
            }

            Artikel A = tempA.First();
            if(A.TotalInStock <= 0 )
            {
                TempData["ec"] = MyExtentions.GetEc("OutOfStock");
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }
                

            ViewBag.Article = A;
            return View();
        }
        
        [HttpPost]
        public IActionResult PlaceOrder(int ArticleId, string FirstName, string Lastname, string Email, string Cellphone, string Message)
        {
            // Check if all field of the form have been filled out and have a valid value
            if (ArticleId == 0)
            {
                TempData["ec"] = MyExtentions.GetEc("Unknown");
                return RedirectToAction("Index", "Shop");
            }
            if(string.IsNullOrWhiteSpace(FirstName) ||
               string.IsNullOrWhiteSpace(Lastname) ||
               string.IsNullOrWhiteSpace(Email))
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Order", "Shop", new { ArticleId = ArticleId });
            }

            // Retrieve the article
            var tempA = _context.Artikels.Where(x => x.Id == ArticleId);
            if (!tempA.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("ArticleDoesntExists");
                return RedirectToAction("Index", "Shop");
            }
            Artikel A = tempA.First();

            // If the article is out of stock return to the view and notify the user
            if(A.TotalInStock <= 0)
            {
                TempData["ec"] = MyExtentions.GetEc("OutOfStock");
                return RedirectToAction("Order", "Shop", new { ArticleId = ArticleId });
            }


            // Update the article
            A.TotalInStock--;
            _context.Artikels.Update(A);

            // Create a new Order instance and fill all the fields
            Order O = new Order()
            {
                Article = A,
                Firstname = FirstName,
                Lastname = Lastname,
                Email = Email,
                Cellphone = Cellphone,
                Message = Message
            };

            _context.Orders.Add(O);
            // Save all the changes made to the database
            _context.SaveChanges();

            // Send a mail to the admin to notify him on this new order placement.
            string msg =    "Dear Mr/Mrs, \n\n" +
                            "A new order has been place on the website by " + FirstName + " " + Lastname + ". \n" +
                            "Their contact information is: \n Email: " + Email + "\n Cellphone: " + Cellphone + "\n" +
                            "They orderd the " + A.Titel + ". \nThe model is " + A.Model + ". \n" +
                            "To View more visit the website. \n\n" +
                            "With kind regards, \n\n Smoothboard Stylers Admin";
            _mailTaskFactory.CreateCustomMailTasks("New order - Smoothboard Stylers", msg, new MailAddress("colin.bloemendaal@gmail.com"), false);

            // Send a confirm mail to the user placing the order
            string confirmMsg = "Dear Mr/Mrs " + Lastname + ", \n\n" +
                                "We have recieved your order and we will be in touch as soon as possible. \n\n" +
                                "With kind regards, \n\n Smoothboard Stylers";
            _mailTaskFactory.CreateCustomMailTasks("Confirmation of Order placement - Smoothboard Stylers", confirmMsg, new MailAddress("colin.bloemendaal@gmail.com"), false);


            return RedirectToAction("Article", "Shop", new { ArticleId = ArticleId });
        }
    }
}
