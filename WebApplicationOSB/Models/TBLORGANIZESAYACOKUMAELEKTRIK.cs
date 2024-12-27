using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace YourNamespace
{
    public class TBLORGANIZESAYACOKUMAELEKTRIK
    {
        [Key]
        public int KURUMID { get; set; }
        int OdemeTakipFaturaTipi = 2;
        public decimal? AKTARIMFATURATUTARI { get; set; }
        public decimal? ODENENTUTAR { get; set; }
        public DateTime? OKUMATARIHI {  get; set; }
        public string? DONEMI {  get; set; }
        public string? FATURANO { get; set; }
        public DateTime? FATURATARIHI {  get; set; }

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