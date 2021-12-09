using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Models
{
    public class Artikel
    {
        [Key]
        public int Id { get; set; }
        public string Titel { get; set; }
        public ArticleModel Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public bool InSale { get; set; }
        public double SalePrice { get; set; }
        public string Image { get; set; }
        public bool InStock { get; set; }
        public int TotalInStock { get; set; }
    }
    public enum ArticleModel
    {
        Shortboard,
        Fish,
        Funboard,
        Longboard,
        Sup
    }
}
