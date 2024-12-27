namespace WebApplicationOSB.Models
{
    public class GetAboneEkstreResponse
    {
        public string KurumKodu { get; set; }
        public List<FaturaResult> Data { get; set; }
        public string Message { get; set; }
    }
}
