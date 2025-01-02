using System.ComponentModel.DataAnnotations;

namespace WebApplicationOSB.Models
{
    public class TblFatura
    {
        [Key]
        public int KURUMID { get; set; }
        public string VERGIDAIRESIHESAPNO { get; set; }
        public string KODU { get; set; }
        public string CARIHESAPKODU { get; set; }
        public int? Tarih { get; set; }
        public int? ODENENTUTAR { get; set; }



    }
}
