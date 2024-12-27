public class FaturaResult
{
    public decimal AlacakTutar { get; set; }
    public decimal BorçTutar { get; set; }
    public string Açıklama { get; set; }
    public string DONEMI { get; set; }
    public string FATURANO { get; set; }
    public string FATURATARIHI { get; set; }
    public string IslemTipi { get; set; }

    
    public DateTime RawFaturaTarihi { get; set; }  // Sıralama için eklendi
}