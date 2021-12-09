using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public bool IsHtml { get; set; }
    }
}
