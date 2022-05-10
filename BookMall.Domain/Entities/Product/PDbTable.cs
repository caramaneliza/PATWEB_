using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookMall.Domain.Enums;

namespace BookMall.Domain.Entities.Product
{
    public class PDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "OwnerId")]
        public int OwnerId { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        public string Description { get; set; }

        public string Genre { get; set; }
        public string ImageUrl { get; set; }
        public string PdfUrl { get; set; }
        public string JpgFile { get; set; }
        public string PdfFile { get; set; }
        public float Price { get; set; }
        public int Pages { get; set; }
        public string ISBN { get; set; }
    }
}
