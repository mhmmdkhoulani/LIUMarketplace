using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUMarketplace.Shared.DTOs
{
    public class ConnectionRequest
    {
        public string SenderEmail { get; set; }
        public string SenderName { get; set;}
        public string MailTo { get; set; }
        public string ReseverName { get; set; }
        public string Body { get; set; }    
        public string Subject { get; set; }
        public string ProductName { get; set; }
    }
}
