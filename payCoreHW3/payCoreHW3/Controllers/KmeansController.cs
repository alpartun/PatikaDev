using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using payCoreHW3.Context;
using payCoreHW3.Models;

namespace payCoreHW3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KmeansController : ControllerBase
{
    private readonly IMapperSession _session;

    public KmeansController(IMapperSession session)
    {
        _session = session;
    }

    // POST
    [HttpPost("Cluster")]

    public IActionResult Cluster(long vehicleId, int numberOfClusters)
    {
        var containers = _session.Containers.Where(x => x.VehicleId == vehicleId).ToArray();

        var veri = containers.Select((x) =>
        
            (X:x.Latitude, Y : x.Longitude)).ToArray();

        var dizi = veri.Select(n => new double[] { (double)n.X, (double)n.Y }).ToArray();


    var random = new Random(5555);
    // Her satırı rastgele bir kümeye ata
    var sonucKumesi = Enumerable
                            .Range(0, dizi.Length)
                            .Select(index => (AtananKume: random.Next(0, numberOfClusters),
                                          Degerler: dizi[index]))
                            .ToList();

    var boyutSayisi = dizi[0].Length;
    var limit = 10000;
    var guncellendiMi = true;
    while (--limit > 0)
    {
        try
        {



            // kümelerin merkez noktalarını hesapla
            var merkezNoktalar = Enumerable.Range(0, numberOfClusters)
                .AsParallel()
                .Select(kumeNumarasi =>
                    (
                        kume: kumeNumarasi,
                        merkezNokta: Enumerable.Range(0, boyutSayisi)
                            .Select(eksen => sonucKumesi.Where(s => s.AtananKume == kumeNumarasi)
                                .Average(s => s.Degerler[eksen]))
                            .ToArray())
                ).ToArray();
            // Sonuç kümesini merkeze en yakın ile güncelle
            guncellendiMi = false;
            //for (int i = 0; i < sonucKumesi.Count; i++)
            Parallel.For(0, sonucKumesi.Count, i =>
            {
                var satir = sonucKumesi[i];
                var eskiAtananKume = satir.AtananKume;

                var yeniAtananKume = merkezNoktalar.Select(n => (KumeNumarasi: n.kume,
                        Uzaklik: UzaklikHesapla(satir.Degerler, n.merkezNokta))).MinBy(x => x.Uzaklik)
                    .KumeNumarasi;

                if (yeniAtananKume != eskiAtananKume)
                {
                    sonucKumesi[i] = (AtananKume: yeniAtananKume, Degerler: satir.Degerler);
                    guncellendiMi = true;
                }
            });

            if (!guncellendiMi)
            {
                break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok(sonucKumesi.Select(k => k.AtananKume).ToArray());
        }
    } // while

    return Ok(sonucKumesi.Select(k => k.AtananKume).ToArray());
    }

    private double UzaklikHesapla(double[] birinciNokta, double[] ikinciNokta)
    {
        var kareliUzaklik = birinciNokta
            .Zip(ikinciNokta,
                (n1, n2) => Math.Pow(n1 - n2, 2)).Sum();
        return Math.Sqrt(kareliUzaklik);
    }
}

// Pretty-print of ClusteringMetrics object.



