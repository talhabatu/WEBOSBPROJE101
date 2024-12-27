using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationOSB.Models;

namespace WebApplicationOSB.Services
{
    public class GetAboneBilgi
    {
        private readonly AppDbContext _context;

        public GetAboneBilgi(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAboneBilgiAsync(string kurumVKN)
        {
            var aboneBilgiler = await _context.TblKurum
                .Where(k => k.VERGIDAIRESIHESAPNO == kurumVKN) 
                .ToListAsync();

            if (aboneBilgiler == null || !aboneBilgiler.Any())
            {
                return new { message = "Kuruma ait bilgi bulunamadı.", kurumVKN };
            }

            var result = aboneBilgiler.Select(k => new
            {
                k.KODU, 
                k.ADI, 
                k.VERGIDAIRESIHESAPNO
            }).ToList();

            return result;
        }
    }
}
