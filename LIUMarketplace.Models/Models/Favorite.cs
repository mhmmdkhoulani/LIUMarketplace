using System.ComponentModel.DataAnnotations;

namespace LIUMarketplace.Models.Models
{
    public class Favorite
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public virtual List<FavoriteItem> FavoriteItems { get; set; }
    }
}
