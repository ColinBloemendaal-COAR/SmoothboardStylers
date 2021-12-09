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
    public class AdminController : Controller
    {
        private readonly Smoothboard_StylersContext _context;

        public AdminController(Smoothboard_StylersContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }

            List<IdentityRole> Roles = _context.Roles.Take(3).ToList();
            ViewBag.Roles = Roles;
            List<Smoothboard_StylersUser> Users = _context.Users.Take(3).ToList();
            ViewBag.Users = Users;
            List<IdentityUserRole<string>> UserRoles = _context.UserRoles.ToList();
            ViewBag.UserRoles = UserRoles;

            List<FAQ> FAQs = _context.FAQs.Take(3).ToList();
            ViewBag.FAQs = FAQs;

            List<Artikel> Articles = _context.Artikels.Take(3).ToList();
            ViewBag.Articles = Articles;

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Roles()
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


            var Roles = _context.Roles.GetPaged(page, 15);
            if (Roles == null)
            {
                TempData["ec"] = MyExtentions.GetEc("RolesEmpty");
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.CurrentPage = page;
            return View(Roles);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RoleDelete(string RoleId)
        {
            if(string.IsNullOrEmpty(RoleId))
            {
                TempData["ec"] = MyExtentions.GetEc("RoleDoesntExists");
                return RedirectToAction("Roles", "Admin");
            }
            var tempRole = _context.Roles.Where(x => x.Id == RoleId);
            if(!tempRole.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("RoleDoesntExists");
                return RedirectToAction("Roles", "Admin");
            }
            if(tempRole.First().Name == "Admin")
            {
                TempData["ec"] = MyExtentions.GetEc("RoleIsDefault");
                return RedirectToAction("Roles", "Admin");
            }
            IdentityRole Role = tempRole.First();

            _context.Attach(Role);
            _context.Remove(Role);
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("DeletedRole");
            return RedirectToAction("Roles", "Admin");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RoleAdd(string RoleName)
        {
            if(string.IsNullOrEmpty(RoleName))
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Roles", "Admin");
            }

            var checkTempRole = _context.Roles.Where(x => x.Name.ToLower() == RoleName.ToLower());
            if(checkTempRole.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("RoleAlreadyExists");
                return RedirectToAction("Roles", "Admin");
            }
            
            IdentityRole newRole = new IdentityRole()
            {
                Name = RoleName,
                NormalizedName = RoleName.ToUpper()
            };

            _context.Roles.Add(newRole);
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("RoleAdded");
            return RedirectToAction("Roles", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RoleEdit()
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }
            string RoleId = "";
            if(string.IsNullOrEmpty(HttpContext.Request.Query["RoleId"]))
            {
                TempData["ec"] = MyExtentions.GetEc("RoleDoesntExists");
                return RedirectToAction("Roles", "Admin");
            }
            RoleId = HttpContext.Request.Query["RoleId"];

            var tempRole = _context.Roles.Where(x => x.Id == RoleId);
            if (!tempRole.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("RoleDoesntExists");
                return RedirectToAction("Roles", "Admin");
            }

            IdentityRole Role = tempRole.First();

            ViewBag.Role = Role;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RoleChange(string RoleId, string RoleName, string ConcurrencyStamp)
        {
            if(string.IsNullOrEmpty(RoleName) || string.IsNullOrEmpty(RoleId))
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("RoleEdit", "Admin");
            }

            var checkTempRole = _context.Roles.Where(x => x.Id == RoleId && x.Name == "Admin");
            if(checkTempRole.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("RoleIsDefault");
                return RedirectToAction("Roles", "Admin");
            }

            IdentityRole UpdateRole = new IdentityRole()
            {
                Id = RoleId,
                Name = RoleName,
                NormalizedName = RoleName.ToUpper(),
                ConcurrencyStamp = ConcurrencyStamp
            };

            _context.Roles.Update(UpdateRole);
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("RoleEdited");
            return RedirectToAction("Roles", "Admin");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult RoleUsers(string RoleId)
        {
            if (TempData["ec"] != null)
            {
                var ec = (object[])TempData["ec"];
                ViewBag.Status = ec[1];
                ViewBag.StatusMessage = ec[0];
            }

            if (!String.IsNullOrEmpty(HttpContext.Request.Query["RoleId"]))
            {
                RoleId = HttpContext.Request.Query["RoleId"];
            }

            // Get the role
            var roleTemp = _context.Roles.Where(x => x.Id == RoleId);
            if (!roleTemp.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("RoleDoesntExists");
                return RedirectToAction("Roles", "Admin");
            }
            IdentityRole role = roleTemp.First();

            // Get all the users which have a specific role
            var userTemp = _context.Users.Where(x => _context.UserRoles.Where(y => y.RoleId == role.Id && y.UserId == x.Id).Count() >= 1);
            List<Smoothboard_StylersUser> UsersWithRole = new List<Smoothboard_StylersUser>();
            foreach (Smoothboard_StylersUser u in userTemp)
            {
                UsersWithRole.Add(u);
            }

            // Get all the users which have a specific role
            var userTemp2 = _context.Users.Where(x => _context.UserRoles.Count() >= 1);
            List<Smoothboard_StylersUser> UsersWithoutRole = userTemp2.ToList();
            foreach (Smoothboard_StylersUser u in UsersWithRole)
            {
                UsersWithoutRole.Remove(u);
            }

            ViewBag.Role = role;
            ViewBag.UsersWith = UsersWithRole;
            ViewBag.UsersWithout = UsersWithoutRole;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RoleUserAdd(string RoleId, string UserId)
        {
            // Check if all form field have been filled out
            if (string.IsNullOrWhiteSpace(RoleId) || string.IsNullOrWhiteSpace(UserId))
            {
                TempData["ec"] = MyExtentions.GetEc("Unknown");
                return RedirectToAction("Roles", "Admin");
            }

            // Check if the user already has the role
            if (_context.UserRoles.Where(x => x.RoleId == RoleId && x.UserId == UserId).Any())
            {
                TempData["ec"] = MyExtentions.GetEc("RoleAlreadyAsigned");
                return RedirectToAction("RoleUsers", "Admin", new { RoleId = RoleId});
            }

            // Create a new UserRole connection and set it in the database
            IdentityUserRole<string> UserRole = new IdentityUserRole<string>()
            {
                RoleId = RoleId,
                UserId = UserId
            };
            _context.UserRoles.Add(UserRole);
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("RoleAsigned");
            return RedirectToAction("RoleUsers", "Admin", new { RoleId = RoleId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RoleUserDelete(string RoleId, string UserId)
        {
            // Check if all form field have been filled out
            if (string.IsNullOrWhiteSpace(RoleId) || string.IsNullOrWhiteSpace(UserId))
            {
                TempData["ec"] = MyExtentions.GetEc("Unknown");
                return RedirectToAction("Roles", "Admin");
            }

            // Check if the user doesn't have the role
            if (!_context.UserRoles.Where(x => x.RoleId == RoleId && x.UserId == UserId).Any())
            {
                TempData["ec"] = MyExtentions.GetEc("RoleNotAsigned");
                return RedirectToAction("RoleUsers", "Admin", new { RoleId = RoleId });
            }


            // Check if the default Admin account is being edited
            // Get Admin Role
            var tempAdminRole = _context.Roles.Where(x => x.Name == "Admin");
            if (!tempAdminRole.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("Unknown");
                return RedirectToAction("RoleUsers", "Admin", new { RoleId = RoleId });
            }
                
            IdentityRole AdminRole = tempAdminRole.First();

            // Get the user
            var tempAdminUser = _context.Users.Where(x => x.Id == UserId);
            if (!tempAdminUser.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("Unknown");
                return RedirectToAction("RoleUsers", "Admin", new { RoleId = RoleId });
            }
            Smoothboard_StylersUser AdminUser = tempAdminUser.First();

            // Check if the user which is being edited is an Admin role and if that user is the default admin account which may not be edited
            if (_context.UserRoles.Where(x => x.RoleId == AdminRole.Id && x.RoleId == RoleId && x.UserId == _context.Users.Where(y => y.Email == "admin@smoothboardstyler.nl").First().Id).Count() >= 1)
            {
                TempData["ec"] = MyExtentions.GetEc("UserIsDefault");
                return RedirectToAction("RoleUsers", "Admin", new { RoleId = RoleId });
            }
            // End of the admin check

            IdentityUserRole<string> UserRole = new IdentityUserRole<string>()
            {
                RoleId = RoleId,
                UserId = UserId
            };
            _context.UserRoles.Attach(UserRole);
            _context.UserRoles.Remove(UserRole);
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("RoleUserDeleted");
            return RedirectToAction("RoleUsers", "Admin", new { RoleId = RoleId });
        }
    
        [Authorize(Roles = "Admin")]
        public IActionResult Users()
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

            var tempUsers = _context.Users.GetPaged(page, 20);

            List<IdentityUserRole<string>> UserRoles = _context.UserRoles.ToList();
            List<IdentityRole> Roles = _context.Roles.ToList();

            ViewBag.Roles = Roles;
            ViewBag.UserRoles = UserRoles;
            return View(tempUsers);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UserDelete(string UserId)
        {
            if (string.IsNullOrWhiteSpace(UserId))
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("Users", "Admin");
            }
            // Get the User
            var tempUser = _context.Users.Where(x => x.Id == UserId);
            if(!tempUser.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("UserDoesntExists");
                return RedirectToAction("Users", "Admin");
            }
            Smoothboard_StylersUser User = tempUser.First();

            // Check if the user who is trying to be deleted is a admin
            if(_context.UserRoles.Where(x => x.UserId == User.Id && x.RoleId == _context.Roles.Where(y => y.Name == "Admin").First().Id).Count() >= 1)
            {
                TempData["ec"] = MyExtentions.GetEc("UserIsAdmin");
                return RedirectToAction("Users", "Admin");
            }
            // Check if user is a default account
            if(User.Email == "admin@smoothboardstyler.nl")
            {
                TempData["ec"] = MyExtentions.GetEc("UserIsDefault");
                return RedirectToAction("Users", "Admin");
            }

            _context.Users.Attach(User);
            _context.Users.Remove(User);
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("UserDeleted");
            return RedirectToAction("Users", "Admin");
        }

        [Authorize(Roles = "Admin")]
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
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.CurrentPage = page;
            return View(FAQs);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult FAQAdd(string Question, string Answer)
        {
            if (string.IsNullOrEmpty(Question))
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("FAQ", "Admin");
            }
            if (string.IsNullOrEmpty(Answer))
            {
                TempData["ec"] = MyExtentions.GetEc("FormEmpty");
                return RedirectToAction("FAQ", "Admin");
            }

            var checkTempQuestion = _context.FAQs.Where(x => x.Question.ToLower() == Question.ToLower());
            if (checkTempQuestion.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("QuestionAlreadyExists");
                return RedirectToAction("FAQ", "Admin");
            }

            FAQ FAQ = new FAQ()
            {
                Question = Question,
                Answer = Answer
            };

            _context.FAQs.Add(FAQ);
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("FAQAdded");
            return RedirectToAction("FAQ", "Admin");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult FAQDelete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                TempData["ec"] = MyExtentions.GetEc("FAQDoesntExists");
                return RedirectToAction("FAQ", "Admin");
            }
            var tempFAQ = _context.FAQs.Where(x => x.Id == Convert.ToInt32(Id));
            if (!tempFAQ.Any())
            {
                TempData["ec"] = MyExtentions.GetEc("FAQDoesntExists");
                return RedirectToAction("FAQ", "Admin");
            }
            FAQ FAQ = tempFAQ.First();

            _context.Attach(FAQ);
            _context.Remove(FAQ);
            _context.SaveChanges();

            TempData["ec"] = MyExtentions.GetEc("FAQDeleted");
            return RedirectToAction("FAQ", "Admin");
        }
    }
}
