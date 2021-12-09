using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Smoothboard_Stylers.Infrastructure;
using System;
using System.IO;

namespace Smoothboard_Stylers.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private IWebHostEnvironment _HostingEnvirement;

        public UnitOfWork(IWebHostEnvironment HostingEnvirement)
        {
            _HostingEnvirement = HostingEnvirement;
        }

        public async void UploadImage(IFormFile file, string fullname)
        {
            if (file != null)
            {

                long totalBytes = file.Length;
                string filename = fullname;
                filename = EnsureFileName(filename);

                byte[] buffer = new byte[file.Length];
                using (FileStream output = System.IO.File.Create(GetPathAndFileName(filename)))
                {
                    try
                    {
                        using (Stream input = file.OpenReadStream())
                        {
                            int readBytes;
                            while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                await output.WriteAsync(buffer, 0, readBytes);
                                totalBytes += readBytes;
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        
                    }
                    
                }
                Array.Clear(buffer, 0, buffer.Length);
            }
        }


        public void DeleteImage(string filename)
        {
            if(string.IsNullOrWhiteSpace(filename))
            {
                string imageFilePath = GetPathAndFileName(filename);
                File.Delete(imageFilePath);
            }
        }

        private string GetPathAndFileName(string filename)
        {
            string path = _HostingEnvirement.WebRootPath + "\\img\\ProductImage\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path + filename;
        }

        private string EnsureFileName(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            return filename;
        }
    }
}
