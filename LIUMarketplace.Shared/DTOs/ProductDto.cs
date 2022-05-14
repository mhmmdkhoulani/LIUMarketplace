using LIUMarketplace.Models.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Shared.DTOs
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile ImageCover { get; set; }
        public string ImageCoverUrl { get;set; }
        public List<IFormFile> MediaPaths { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; } 
        public Category Category { get; set; }

    }
}
