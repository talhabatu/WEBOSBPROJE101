using System.Text.Json.Serialization;

public class FaturaResult
{
    public decimal AlacakTutar { get; set; }
    public decimal BorçTutar { get; set; }
    public string Açıklama { get; set; }
    public string DONEMI { get; set; }
    public string FATURANO { get; set; }

    [JsonPropertyName("faturaTarihi")]  
    public DateOnly FaturaTarihi { get; set; }
    public decimal DevredenAlacakTutar { get; set; }

    public string IslemTipi { get; set; }

    [JsonPropertyName("FaturaTarihiFormatted")]
    public string FaturaTarihiFormatted => FaturaTarihi.ToString("dd-MM-yyyy");

    public decimal Bakiye { get; internal set; }
    public int DevredenBorçTutar { get; internal set; }
    public decimal DevredenBakiye { get; internal set; }
}
