using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Models.Models
{
    public class Review
    {
        public Review()
        {
            Id = Guid.NewGuid().ToString();
            CreatedTime = DateTime.UtcNow;
            ModifiedTime = DateTime.UtcNow;
        }
        public string Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string CreatedByUserId { get; set; }

        public virtual Product Product { get; set; }

        [ForeignKey("Product")]
        public string ProductId { get; set; }

        public string Content { get; set; }

    }
}
