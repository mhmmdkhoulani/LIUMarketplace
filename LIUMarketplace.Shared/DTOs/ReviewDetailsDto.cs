using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Shared.DTOs
{
    public class ReviewDetailsDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string ProductId { get;set; }
        public DateTime CreatedTime { get; set; }
    }
}
