using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using on_e_commerce.data;
using on_e_commerce.Models;

namespace on_e_commerce.Controllers
{
    public class PaymentController : Controller
    {
        public readonly dbEticaretEntities db = new dbEticaretEntities();
        // GET: Payment
        public ActionResult Payment()
        {
            ViewBag.Sehirler = db.Sehirler.OrderBy(x => x.SehirAdi).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Payment([Bind(Include = "SiparisDetayId,UyeId,Ad,Soyad,Adres,Sehir,Ilce,Ulke,PostaKodu,TelefonNo,Email,SiparisTarihi,OdemeTipi,ToplamFiyat,SiparisDurumu")] Tbl_SiparisDetay SiparisDetay, string il, string ilce)
        {

            if (ModelState.IsValid)
            {
                int toplamfiyat = 0;
                List<Item> sepet = (List<Item>)Session["sepet"];
                if (Session["sepet"] != null)
                {

                    Tbl_SiparisUrunleri siparisurunleri = new Tbl_SiparisUrunleri();
                    for (int i = 0; i < sepet.Count; i++)
                    {
                        siparisurunleri.Adet = sepet[i].Count;
                        siparisurunleri.UrunId = sepet[i].Urun.UrunId;
                        siparisurunleri.SiparisDetayId = SiparisDetay.SiparisDetayId;
                        if (Session["uyeid"] != null)
                        {
                            siparisurunleri.UyeId = Convert.ToInt16(Session["uyeid"]);
                        }
                        db.Tbl_SiparisUrunleri.Add(siparisurunleri);
                        toplamfiyat += Convert.ToInt16(sepet[i].Urun.Fiyat);
                    }
                    SiparisDetay.Sehir = il;
                    SiparisDetay.Ilce = ilce;
                    SiparisDetay.Ulke = "Türkiye";
                    SiparisDetay.ToplamFiyat = toplamfiyat;
                    SiparisDetay.SiparisDurumu = 1;
                    SiparisDetay.SiparisTarihi = DateTime.Now;
                    db.Tbl_SiparisDetay.Add(SiparisDetay);
                    db.SaveChanges();

                    return RedirectToAction("OdemeBasarılı", new { OdemeFiyatToplam = toplamfiyat });
                }
                else
                {
                    return View("OdemeBasarısız");
                }
            }
            return View();
        }
        public ActionResult OdemeBasarılı(int OdemeFiyatToplam)
        {
            ViewBag["toplamfiyat"] = "";
            ViewBag["toplamfiyat"] = OdemeFiyatToplam;
            return View();
        }
        public ActionResult OdemeBasarısız()
        {
            return View();
        }
    }
}