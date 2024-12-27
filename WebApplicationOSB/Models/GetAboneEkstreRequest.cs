namespace WebApplicationOSB.Models
{
    public class GetAboneEkstreRequest
    {
        public string KurumKodu { get; set; }
        public string VergiDairesiHesapNo { get; set; }
        public string FaturaTarihi { get; set; }  // "yyyyMM" formatında
    }
}
