using Microsoft.EntityFrameworkCore;

public class FaturaService
{
    private readonly AppDbContext _context;

    public FaturaService(AppDbContext context)
    {
        _context = context;
    }

    public List<FaturaResult> GetFaturalar(DateTime faturaBaslangicTarihi, string kurumKodu, string vergiDairesiHesapNo)
    {
        // Retrieve the Kurum ID based on the kurumKodu and vergiDairesiHesapNo
        var kurumId = _context.TblKurum
            .Where(k => k.KODU == kurumKodu && k.VERGIDAIRESIHESAPNO == vergiDairesiHesapNo)
            .Select(k => k.ID)
            .FirstOrDefault();

        if (kurumId == 0)
        {
            Console.WriteLine("Kurum bulunamadı.");
            return new List<FaturaResult>();
        }

        // Log for debugging
        Console.WriteLine($"Kurum ID: {kurumId} - Arama Tarihleri: {faturaBaslangicTarihi:yyyy-MM-dd} ve sonrası");

        // Retrieve Dogalgaz Verisi (Gas data)
        var dogalgazVerisi = _context.TBLORGANIZESAYACOKUMADOGALGAZ
            .Where(d => d.FATURATARIHI.HasValue &&
                        d.FATURATARIHI.Value >= faturaBaslangicTarihi &&  // Ensures it includes the input date and future dates
                        d.KURUMID == kurumId) // Filter by the retrieved kurumId
            .Select(d => new FaturaResult
            {
                IslemTipi = "Fatura",
                Açıklama = $"{d.DONEMI} Dönemi Doğalgaz Faturası",
                BorçTutar = 0,
                AlacakTutar = d.AKTARIMFATURATUTARI ?? 0m,
                FATURATARIHI = d.GetFormattedFaturaTarihi(),
            }).ToList();

        Console.WriteLine($"Bulunan Dogalgaz Faturası: {dogalgazVerisi.Count}");

        // Retrieve Elektrik Verisi (Electric data)
        var elektrikVerisi = _context.TBLORGANIZESAYACOKUMAELEKTRIK
            .Where(e => e.FATURATARIHI.HasValue &&
                        e.FATURATARIHI.Value >= faturaBaslangicTarihi &&  // Same logic for Elektrik
                        e.KURUMID == kurumId) // Filter by the retrieved kurumId
            .Select(e => new FaturaResult
            {

                IslemTipi = "Fatura",
                Açıklama = $"{e.DONEMI} Dönemi Elektrik Faturası",
                BorçTutar = 0,
                AlacakTutar = e.AKTARIMFATURATUTARI ?? 0m,
                FATURATARIHI = e.GetFormattedFaturaTarihi(),

            }).ToList();

        Console.WriteLine($"Bulunan Elektrik Faturası: {elektrikVerisi.Count}");

        // Retrieve Su Verisi (Water data)
        var suVerisi = _context.TBLORGANIZESAYACOKUMASU
            .Where(s => s.FATURATARIHI.HasValue &&
                        s.FATURATARIHI.Value >= faturaBaslangicTarihi &&  // Same logic for Su
                        s.KURUMID == kurumId) // Filter by the retrieved kurumId
            .Select(s => new FaturaResult
            {
                IslemTipi = "Fatura",
                Açıklama = $"{s.DONEMI} Dönemi Su Faturası",
                BorçTutar =0,
                AlacakTutar = s.AKTARIMFATURATUTARI ?? 0m,
                FATURATARIHI = s.GetFormattedFaturaTarihi(),
       
            }).ToList();
        var OdemeVerisi = _context.TBLORGANIZEODEMELER
      .Where(o => o.ODEMETARIHI.HasValue &&
                  o.ODEMETARIHI.Value >= faturaBaslangicTarihi &&  // Same logic for Su
                  o.ABONE == kurumId) // Filter by the retrieved kurumId
      .Select(o => new FaturaResult
      {
          IslemTipi = "Odeme",
          Açıklama = o.ACIKLAMA,
          BorçTutar = o.ODEMETUTAR,
          AlacakTutar = 0,
          FATURATARIHI = o.GetFormattedOdemeTarihi(),

      }).ToList();

        Console.WriteLine($"Bulunan Su Faturası: {suVerisi.Count}");

        // Combine all results and order by RawFaturaTarihi
        var sonuc = dogalgazVerisi
            .Concat(elektrikVerisi)
            .Concat(suVerisi)
            .Concat(OdemeVerisi)
            .OrderBy(f => f.RawFaturaTarihi)
            .ToList();

        Console.WriteLine($"Toplam Bulunan Fatura: {sonuc.Count}");
        return sonuc;
    }

}
