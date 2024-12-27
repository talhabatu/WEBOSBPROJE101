using System.ComponentModel.DataAnnotations;

namespace WebApplicationOSB.Models
{
    public class TBLORGANIZEODEMELER
    {
        [Key]
        public int ABONE { get; set; }
        public decimal ODEMETUTAR { get; set; }
        public string ACIKLAMA { get; set; }
        public DateTime? ODEMETARIHI { get; set; }
        public string GetFormattedOdemeTarihi()
        {
            return ODEMETARIHI.HasValue ? ODEMETARIHI.Value.ToString("dd/MM/yyyy") : null;
        }

    }
}
