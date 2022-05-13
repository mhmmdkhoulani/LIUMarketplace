using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Models.Models
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();
            CreatedTime = DateTime.UtcNow;
            UpdatedTime = DateTime.UtcNow;
        }
        [Key]
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }
        
        [ForeignKey("ApplicationUser")]
        [Required]
        public string CreatedByUserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(50)]
        public string Description { get; set; }
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        public double Price { get; set; }

        public virtual List<Review> Reviews { get; set; }

        [NotMapped]
        public IFormFile ImageCover { get; set; }
        public string ImageCoverUrl { get; set; }
        public List<Media> MediaPaths { get; set; }

        public virtual Category Category { get; set; }
       
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

    }
}
