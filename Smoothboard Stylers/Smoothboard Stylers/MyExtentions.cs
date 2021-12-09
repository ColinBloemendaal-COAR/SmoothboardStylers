using Rekentrainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smoothboard_Stylers
{
    public static class MyExtentions
    {
        public static string[] GetEc(string ec)
        {
            ec = ec.ToLower();
            if (string.IsNullOrEmpty(ec))
                return new string[] { "An error has occured. Please try again. If this error keeps happening, please contact the site administrator.", "Warning" };
            if (ec == "Unknown".ToLower())
                return new string[] { "An error has occured. Please try again. If this error keeps happening, please contact the site administrator.", "Warning" };
            if(ec == "TestW".ToLower())
                return new string[] { "Testing Warning", "Warning" };
            if (ec == "TestS".ToLower())
                return new string[] { "Testing succes", "Succes" };
            if (ec == "TestI".ToLower())
                return new string[] { "Testing info", "Info" };
            if(ec == "RolesEmpty".ToLower())
                return new string[] { "There are 0 roles currently in existance.", "Warning" };
            if(ec == "RoleDoesntExists".ToLower())
                return new string[] { "The role you tried to view, edit or delete doesn't exists", "Warning" };
            if(ec == "RoleIsDefault".ToLower())
                return new string[] { "The role you tried to edit or delete is a default role. Default roles may not be edited or deleted", "Warning" };
            if(ec == "DeletedRole".ToLower()) 
                return new string[] { "You succesfully deleted the role.", "Succes" };
            if(ec == "RoleAdded".ToLower())
                return new string[] { "You succesfully created a new role.", "Succes" };
            if(ec == "FormEmpty".ToLower())
                return new string[] { "Not all fields were filled out correctly in the form. Please check the fields.", "Warning" };
            if(ec == "RoleAlreadyExists".ToLower())
                return new string[] { "There already exists a role with this name.", "Warning" };
            if(ec == "RoleEdited".ToLower())
                return new string[] { "You succesfully edited that role", "Succes" };
            if(ec == "RoleAsigned".ToLower())
                return new string[] { "This role was succesfully assigned to the user", "Succes" };
            if(ec == "RoleAlreadyAsigned".ToLower())
                return new string[] { "This role was already assigned to this user.", "Info" };
            if(ec == "RoleNotAsigned".ToLower())
                return new string[] { "The user you tried to remove the role from already didn't have the role assigned.", "Info" };
            if(ec == "RoleUserDeleted".ToLower())
                return new string[] { "You succesfully removed the user from this role.", "Succes" };
            if(ec == "UserDeleted".ToLower())
                return new string[] { "You succesfully deleted the user account.", "Succes" };
            if (ec == "UserDoesntExists".ToLower())
                return new string[] { "The user you tried to view, edit or delete doesn't exists", "Warning" };
            if (ec == "UserIsDefault".ToLower())
                return new string[] { "The user you tried to edit or delete is a default user. Default users may not be edited or deleted", "Warning" };
            if(ec == "UserIsAdmin".ToLower())
                return new string[] { "The user you tried to edit or delete is an Admin. Admins may not be edited or deleted", "Warning" };
            if (ec == "ArticleDoesntExists".ToLower())
                return new string[] { "We can't seem to find the article that you're looking for.", "Warning" };
            if (ec == "NotAValidArticle".ToLower())
                return new string[] { "The article you tried to view doesn't have a valid Id.", "Warning" };
            if (ec == "ArticleUpdated".ToLower())
                return new string[] { "You succesfully updated the article.", "Succes" };
            if (ec == "ArticleDeleted".ToLower())
                return new string[] { "You succesfully deleted that article.", "Succes" };
            if (ec == "ArticlesEmpty".ToLower())
                return new string[] { "There are currently 0 articles.", "Info" };
            if (ec == "ArticleAdded".ToLower())
                return new string[] { "You succesfully added a new article.", "Succes" };
            if (ec == "FAQAdded".ToLower())
                return new string[] { "You succesfully added a new question.", "Succes" };
            if (ec == "FAQDeleted".ToLower())
                return new string[] { "You sucessfully deleted that question.", "Success" };
            if (ec == "FAQEmpty".ToLower())
                return new string[] { "There are currently 0 questions.", "Info" };
            if (ec == "FAQUpdated".ToLower())
                return new string[] { "You successfully updated the question.", "Success" };
            if (ec == "FAQAlreadyExist".ToLower())
                return new string[] { "This question already exists.", "Info" };
            if (ec == "FAQDoesntExist".ToLower())
                return new string[] { "This question doesn't extist.", "Warning" };
            if(ec == "NewsLetterAdded".ToLower())
                return new string[] { "You succesfully added a newsletter.", "Succes" };
            if (ec == "ContactFormSend".ToLower())
                return new string[] { "The contact form was succesfully send", "Succes" };
            if (ec == "OutOfStock".ToLower())
                return new string[] { "This article is out of stock. Our apologies!", "Warning" };

            return new string[] { "An error has occured. Please try again. If this error keeps happening, please contact the site administrator.", "Warning" };
        }

        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
