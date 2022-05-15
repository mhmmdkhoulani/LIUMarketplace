using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Shared.DTOs
{
    public class AuthResponse
    {
        public string Messages { get; set; }
        public string Token { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<String> Roles { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
