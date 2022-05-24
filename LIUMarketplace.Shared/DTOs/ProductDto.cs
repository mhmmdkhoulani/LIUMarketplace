using LIUMarketplace.Models.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Shared.DTOs
{
    public class ProductDto
    {
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile ImageCover { get; set; }
        public string ImageCoverUrl { get;set; }
        public List<IFormFile> MediaPaths { get; set; }
        
        [Required]
        public double Price { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; } 
        public Category Category { get; set; }

    }

}
