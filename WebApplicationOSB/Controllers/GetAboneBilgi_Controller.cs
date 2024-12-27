using Microsoft.AspNetCore.Mvc;
using WebApplicationOSB.Services;
using System.Threading.Tasks;

namespace WebApplicationOSB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAboneBilgi_Controller : ControllerBase
    {
        private readonly GetAboneBilgi _getAboneBilgi;

        public GetAboneBilgi_Controller(GetAboneBilgi getAboneBilgi)
        {
            _getAboneBilgi = getAboneBilgi;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAboneBilgi(string kurumVKN)
        {
            var result = await _getAboneBilgi.GetAboneBilgiAsync(kurumVKN);
            return Ok(result); 
        }
    }
}
