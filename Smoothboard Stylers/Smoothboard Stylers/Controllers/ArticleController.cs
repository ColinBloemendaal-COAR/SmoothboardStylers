using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smoothboard_Stylers.Data;
using Smoothboard_Stylers.Infrastructure;
using Smoothboard_Stylers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Controllers
{

    [Route("Admin/Articles/")]
    public class ArticleController : Controller
    {
        private readonly Smoothboard_StylersContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _HostingEnvirement;

        public ArticleController(Smoothboard_StylersContext context, IUnitOfWork unitOfWork, IWebHostEnvironment HostingEnvirement)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _HostingEnvirement = HostingEnvirement;
        }


        // Articles
        [Route("")]
        [Route("Index")]
        [Authorize(Roles = "Admin")]
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

            var Articles = _context.Artikels.GetPaged(page, 20);
            if (Articles == null)
            {
                TempData["ec"] = MyExtentions.GetEc("ArticlesEmpty");
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.CurrentPage = page;
            return View("~/Views/Admin/Articles/Index.cshtml", Articles);
        }

        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }
            return View("~/Views/Admin/Articles/Add.cshtml");
        }

        [Route("Create")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(string Title, string Model, IFormFile Image, string Color, string Size, double Price = 0, int TotalInStock = 0, double SalePrice = 0)
        {
            // Check if all field of the form have been filled out and have a valid value
            if (string.IsNullOrWhiteSpace(Title) || 
                string.IsNullOrWhiteSpace(Model) || 
                !Enum.IsDefined(typeof(ArticleModel), Model) || 
                string.IsNullOrWhiteSpace(Color) || 
                string.IsNullOrWhiteSpace(Size) || 
                Price == 0 || 
                Price <= 0 ||
                TotalInStock == 0 || 
                TotalInStock <= 0 || 
                Image == null)
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Add", "Article");
            }

            // Validate the instock and insale values
            bool InSale = false;
            if (SalePrice > 0)
                InSale = true;

            bool InStock = false;
            if (TotalInStock > 0)
                InStock = true;

            // Proccess the image and upload it.
            string fileName = Path.GetFileNameWithoutExtension(Image.FileName).Trim('"');
            string fileExt = Path.GetExtension(Image.FileName);
            string datetime = DateTime.Now.ToString().Replace(@"/", "").Replace(" ", "").Replace(":", "");
            string fullname = fileName + datetime + fileExt;
            _unitOfWork.UploadImage(Image, fullname);
            
            // Create a new article instance and fill all fields
            Artikel A = new Artikel()
            {
                Titel = Title,
                Model = (ArticleModel)Enum.Parse(typeof(ArticleModel), Model),
                Image = fullname,
                Color = Color,
                Size = Size,
                Price = Price,
                InSale = InSale,
                SalePrice = SalePrice,
                InStock = InStock,
                TotalInStock = TotalInStock
            };

            // Save article to database
            _context.Artikels.Add(A);
            _context.SaveChanges();

            // Return to the view with correct corresponding feedback Message
            TempData["ec"] = MyExtentions.GetEc("ArticleAdded");
            return RedirectToAction("Articles", "Admin");
        }

        [Route("Edit")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit()
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
                return RedirectToAction("Articles", "Admin");
            }
            bool IsInt = Int32.TryParse(HttpContext.Request.Query["ArticleId"], out ArticleId);
            if (!IsInt)
            {
                TempData["ec"] = MyExtentions.GetEc("NotAValidArticle");
                return RedirectToAction("Articles", "Admin");
            }

            var tempA = _context.Artikels.Where(x => x.Id == ArticleId);
            if (!tempA.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("ArticleDoesntExists");
                return RedirectToAction("Articles", "Admin");
            }

            Artikel A = tempA.First();

            ViewBag.Article = A;
            return View("~/Views/Admin/Articles/Edit.cshtml");
        }

        [Route("Update")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int ArticleId, string OldFilename, string Title, string Model, IFormFile Image, string Color, string Size, double Price = 0, double SalePrice = 0, int TotalInStock = 0)
        {
            if(ArticleId == 0)
            {
                TempData["ec"] = MyExtentions.GetEc("Unknown");
                return RedirectToAction("Index", "Article");
            }
            // Check if all field of the form have been filled out and have a valid value
            if (
               string.IsNullOrWhiteSpace(OldFilename) ||
               string.IsNullOrWhiteSpace(Title) ||
               string.IsNullOrWhiteSpace(Model) || 
               string.IsNullOrWhiteSpace(Color) ||
               string.IsNullOrWhiteSpace(Size) ||
               Price <= 0 ||
               SalePrice < 0 ||
               TotalInStock <= 0 ||
               Image == null)
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Edit", "Article", new { ArticleId = ArticleId });
            }

            // Validate the InStock and InSale values
            bool InSale = false;
            if (SalePrice > 0)
                InSale = true;

            bool InStock = false;
            if (TotalInStock > 0)
                InStock = true;

            // Proccess the image and upload it. Delete the old image
            string fileName = Path.GetFileNameWithoutExtension(Image.FileName).Trim('"');
            string fileExt = Path.GetExtension(Image.FileName);
            string datetime = DateTime.Now.ToString().Replace(@"/", "").Replace(" ", "").Replace(":", "");
            string fullname = fileName + datetime + fileExt;
            _unitOfWork.DeleteImage(OldFilename);
            _unitOfWork.UploadImage(Image, fullname);

            // Create a new instance of a article and fill all fields
            Artikel A = new Artikel()
            {
                Id = ArticleId,
                Titel = Title,
                Model = (ArticleModel)Enum.Parse(typeof(ArticleModel), Model),
                Image = fullname,
                Color = Color,
                Size = Size,
                Price = Price,
                InSale = InSale,
                SalePrice = SalePrice,
                InStock = InStock,
                TotalInStock = TotalInStock
            };

            // Update the article in the database
            _context.Artikels.Update(A);
            _context.SaveChanges();

            // Return to the view with correct corresponding feedback Message
            TempData["ec"] = MyExtentions.GetEc("ArticleUpdated");
            return RedirectToAction("Index", "Article");
        }

        [Route("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete()
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
                return RedirectToAction("Artikelen", "Admin");
            }

            bool IsInt = Int32.TryParse(HttpContext.Request.Query["ArticleId"], out ArticleId);
            if (!IsInt)
            {
                TempData["ec"] = MyExtentions.GetEc("NotAValidArticle");
                return RedirectToAction("Artikelen", "Admin");
            }

            ViewBag.ArticleId = ArticleId;
            return View("~/Views/Admin/Articles/Delete.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ConfirmDeletetion(int ArticleId)
        {
            if (ArticleId == 0)
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Artikelen", "Admin");
            }

            _context.RemoveRange(_context.Artikels.Where(x => x.Id == ArticleId));
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("ArticleDeleted");
            return RedirectToAction("Articles", "Admin");
        }
    }
}
