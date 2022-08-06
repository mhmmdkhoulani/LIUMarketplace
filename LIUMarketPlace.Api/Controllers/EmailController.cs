using LIUMarketplace.Shared.DTOs;
using LIUMarketPlace.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIUMarketPlace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public EmailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] ConnectionRequest req)
        {
            var filePath = $"{Directory.GetCurrentDirectory()}\\Templates\\EmailTemplateRequest.html";
            var str = new StreamReader(filePath);

            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText.Replace("[emailtoname]", req.ReseverName);
            mailText = mailText.Replace("[sendername]", req.SenderName);
            mailText = mailText.Replace("[email]", req.SenderName);
            mailText = mailText.Replace("[product name]", req.ProductName);
            

            await _mailService.SendEmailAsync(req.MailTo, req.Subject, mailText);
            return Ok();
        }
    }
}
