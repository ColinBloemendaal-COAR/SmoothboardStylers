using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Infrastructure
{
    public interface IUnitOfWork
    {
        void UploadImage(IFormFile file, string fullname)
        {

        }
        void DeleteImage(string filename)
        {

        }
    }
}
