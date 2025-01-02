using System.ComponentModel.DataAnnotations;

namespace WebApplicationOSB.Models
{
    public class TBLORGANIZESAYACOKUMADOGALGAZ
    {
        [Key]
        public int KURUMID { get; set; }
        public decimal? AKTARIMFATURATUTARI { get; set; }
        public decimal? ODENENTUTAR { get; set; }
        public string? DONEMI { get; set; }
        public DateTime? FATURATARIHI { get; set; }
        public string? FATURANO { get; set; }




        public string GetFormattedFaturaTarihi()
        {
            return FATURATARIHI.HasValue ? FATURATARIHI.Value.ToString("dd/MM/yyyy") : null;
        }
    }
}
