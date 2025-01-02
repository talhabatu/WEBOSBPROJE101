using Microsoft.AspNetCore.Mvc;
using WebApplicationOSB.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class GetAboneEkstreController : ControllerBase
{
    private readonly FaturaService _faturaService;

    public GetAboneEkstreController(FaturaService faturaService)
    {
        _faturaService = faturaService;
    } 

    [HttpGet]
    [Route("GetAboneEkstre")]
    public async Task<IActionResult> GetAboneEkstre(
        [FromQuery] string KurumKodu, 
        [FromQuery] string VergiDairesiHesapNo, 
        [FromQuery] string FaturaTarihi)
    {
        Console.WriteLine($"Gelen Parametreler - KurumKodu: {KurumKodu}, VKN: {VergiDairesiHesapNo}, FaturaTarihi: {FaturaTarihi}");

        DateTime faturaBaslangicTarihi;
        try
        {
            faturaBaslangicTarihi = DateTime.ParseExact(FaturaTarihi, "MM-yyyy", null);
            Console.WriteLine($"Parse Edilen Tarih: {faturaBaslangicTarihi:yyyy-MM-dd}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Tarih Parse Hatası: {ex.Message}");
            return BadRequest("FaturaTarihi geçerli bir tarih formatında olmalıdır (MM-yyyy).");
        }

        // Fetch invoices based on the selected FaturaTipi
        var faturalar = _faturaService.GetFaturalar(faturaBaslangicTarihi, KurumKodu, VergiDairesiHesapNo);

        var response = new GetAboneEkstreResponse
        {
            KurumKodu = KurumKodu,
            Data = faturalar,
            Message = faturalar.Any() ? "Veriler başarıyla getirildi." : "Veri bulunamadı."
        };

        Console.WriteLine($"Dönen Fatura Sayısı: {faturalar.Count}");
        return Ok(response);
    }
}
