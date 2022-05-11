using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIUMarketplace.Models.Models
{
    public class Media
    {
        public Media()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public string Path { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Product")]
        public string ProductId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
    }
}
