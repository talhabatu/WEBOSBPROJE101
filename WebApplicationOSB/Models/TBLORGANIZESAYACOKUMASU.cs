using System.ComponentModel.DataAnnotations;

namespace WebApplicationOSB.Models
{
    public class TBLORGANIZESAYACOKUMASU
    {
        [Key]
        public int KURUMID { get; set; }
        public int OdemeTakipFaturaTipi = 1;
        public decimal? AKTARIMFATURATUTARI { get; set; }
        public decimal? ODENENTUTAR { get; set; }
        public DateTime? OKUMATARIHI { get; set; }
        public string? DONEMI { get; set; }
        public string? FATURANO { get; set; }
        public DateTime? FATURATARIHI { get; set; }

        public string GetFormattedOkumaTarihi()
        {
            return OKUMATARIHI.HasValue ? OKUMATARIHI.Value.ToString("dd/MM/yyyy") : null;
        }

        public string GetFormattedFaturaTarihi()
        {
            return FATURATARIHI.HasValue ? FATURATARIHI.Value.ToString("dd/MM/yyyy") : null;
        }
    }
}
