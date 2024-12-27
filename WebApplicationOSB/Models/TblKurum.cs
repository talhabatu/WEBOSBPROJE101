using Microsoft.EntityFrameworkCore;

namespace WebApplicationOSB.Models
{
    public class TblKurum
    {
        public int ID { get; set; }
        public string ADI { get; set; }
        public string? VERGIDAIRESIHESAPNO { get; set; }
        public string? KODU { get; set; }
        public string? CARIHESAPKODU { get; set; }
    }
}
