using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Models.Models
{
    public class FavoriteItem
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string FavoriteId { get; set; }
        public virtual Favorite Favorite { get; set; }
    }
}
