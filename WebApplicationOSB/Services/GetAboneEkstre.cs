using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplicationOSB.Services;

public class FaturaService
{
    private readonly AppDbContext _context;

    public FaturaService(AppDbContext context)
    {
        _context = context ?? throw new InvalidOperationException("DbContext is not initialized.");
    }

    public List<FaturaResult> GetFaturalar(DateTime faturaBaslangicTarihi, string kurumKodu, string vergiDairesiHesapNo)
    {
        var kurumId = _context.TblKurum
            .Where(k => k.KODU == kurumKodu && k.VERGIDAIRESIHESAPNO == vergiDairesiHesapNo)
            .Select(k => k.ID)
            .FirstOrDefault();

        if (kurumId == 0)
        {
            Console.WriteLine("Kurum bulunamadı.");
            return new List<FaturaResult>();
        }

        Console.WriteLine($"Kurum ID: {kurumId} - Arama Tarihleri: {faturaBaslangicTarihi:yyyy-MM-dd} ve sonrası");
        
        
        ////////////////////////////////////////////////////////    DEVREDEN BORÇ ALACAK BAKİYE     //////////////////////////////////////////////////////////







        
        
        
        
        
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        var dogalgazVerisi = _context.TBLORGANIZESAYACOKUMADOGALGAZ
            .Where(d => d.FATURATARIHI.HasValue &&
                        d.FATURATARIHI.Value >= faturaBaslangicTarihi &&
                        d.KURUMID == kurumId)
            .Select(d => new FaturaResult
            {
                IslemTipi = "Fatura",
                Açıklama = $"{d.DONEMI} Dönemi Doğalgaz Faturası",
                BorçTutar = 0,
                AlacakTutar = d.AKTARIMFATURATUTARI ?? 0m,
                FaturaTarihi = d.FATURATARIHI.HasValue ? DateOnly.ParseExact(d.FATURATARIHI.Value.ToString("dd-MM-yyyy"), "dd-MM-yyyy") : default,
                FATURANO= d.FATURANO
            }).ToList();

        var elektrikVerisi = _context.TBLORGANIZESAYACOKUMAELEKTRIK
            .Where(e => e.FATURATARIHI.HasValue &&
                        e.FATURATARIHI.Value >= faturaBaslangicTarihi &&
                        e.KURUMID == kurumId)
            .Select(e => new FaturaResult
            {
                IslemTipi = "Fatura",
                Açıklama = $"{e.DONEMI} Dönemi Elektrik Faturası",
                BorçTutar = 0,
                AlacakTutar = e.AKTARIMFATURATUTARI ?? 0m,
                FaturaTarihi = e.FATURATARIHI.HasValue ? DateOnly.ParseExact(e.FATURATARIHI.Value.ToString("dd-MM-yyyy"), "dd-MM-yyyy") : default,
                FATURANO = e.FATURANO
            }).ToList();

        var suVerisi = _context.TBLORGANIZESAYACOKUMASU
            .Where(s => s.FATURATARIHI.HasValue &&
                        s.FATURATARIHI.Value >= faturaBaslangicTarihi &&
                        s.KURUMID == kurumId 
                        )
            .Select(s => new FaturaResult
            {
                IslemTipi = "Fatura",
                Açıklama = $"{s.DONEMI} Dönemi Su Faturası",
                BorçTutar = 0,
                AlacakTutar = s.AKTARIMFATURATUTARI ?? 0m,
                FaturaTarihi = s.FATURATARIHI.HasValue ? DateOnly.ParseExact(s.FATURATARIHI.Value.ToString("dd-MM-yyyy"), "dd-MM-yyyy") : default,
                FATURANO = s.FATURANO
            }).ToList();

        var OdemeVerisi = _context.TBLORGANIZEODEMELER
            .Where(o => o.ODEMETARIHI.HasValue &&
                        o.ODEMETARIHI.Value >= faturaBaslangicTarihi &&
                        o.ABONE == kurumId)
            .Select(o => new FaturaResult
            {
                IslemTipi = "Odeme",
                Açıklama = o.ACIKLAMA,
                BorçTutar = o.ODEMETUTAR,
                AlacakTutar = 0,
                FaturaTarihi = o.ODEMETARIHI.HasValue ? DateOnly.ParseExact(o.ODEMETARIHI.Value.ToString("dd-MM-yyyy"), "dd-MM-yyyy") : default
            }).ToList();
        
        var sonuc = dogalgazVerisi
            .Concat(elektrikVerisi)
            .Concat(suVerisi)
            .Concat(OdemeVerisi)
            .OrderBy(f => f.FaturaTarihi)
            .ToList();

        decimal DevredenkümülatifBakiye = 0;
        foreach (var item in sonuc)
        {
            item.DevredenBakiye = DevredenkümülatifBakiye + item.DevredenBorçTutar - item.DevredenAlacakTutar;
            DevredenkümülatifBakiye = item.DevredenBakiye;
        }


        decimal kümülatifBakiye = DevredenkümülatifBakiye;
        foreach (var item in sonuc)
        {
            item.Bakiye = kümülatifBakiye + item.BorçTutar - item.AlacakTutar;
            kümülatifBakiye = item.Bakiye;
        }

        return sonuc;
    }

}
