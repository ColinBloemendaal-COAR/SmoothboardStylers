using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Smoothboard_Stylers.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Smoothboard_StylersUser class
    public class Smoothboard_StylersUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
