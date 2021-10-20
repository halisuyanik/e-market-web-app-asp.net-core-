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
        public readonly dbEticaretEntities1 db = new dbEticaretEntities1();
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
     
                if (sepet != null)
                {
                    string siparisbarkod = Guid.NewGuid().ToString();

                    for (int i = 0; i < sepet.Count; i++)
                    {
                        db.Tbl_Urun.Find( sepet[i].Urun.UrunId).Miktar = db.Tbl_Urun.Find(sepet[i].Urun.UrunId).Miktar - sepet[i].Count;
                        Tbl_SiparisUrunleri siparisurunleri = new Tbl_SiparisUrunleri();
                        siparisurunleri.Adet = sepet[i].Count;
                        siparisurunleri.UrunId = sepet[i].Urun.UrunId;
                        siparisurunleri.SiparisBarkod = siparisbarkod;

                        if (Session["uyeid"] != null)
                        {
                            siparisurunleri.UyeId = Convert.ToInt16(Session["uyeid"]);
                        }
                        toplamfiyat += Convert.ToInt16(sepet[i].Price) * sepet[i].Count;
                        db.Tbl_SiparisUrunleri.Add(siparisurunleri);
                    }
                   
                    SiparisDetay.Sehir = il;
                    SiparisDetay.Ilce = ilce;
                    SiparisDetay.Ulke = "Türkiye";
                    SiparisDetay.ToplamFiyat = toplamfiyat;
                    SiparisDetay.SiparisDurumu = 1;
                    SiparisDetay.SiparisTarihi = DateTime.Now;
                    if (Session["uyeid"] != null)
                    {
                        SiparisDetay.UyeId = Convert.ToInt16(Session["uyeid"]);
                    }
                    SiparisDetay.SiparisBarkod = siparisbarkod;
                    db.Tbl_SiparisDetay.Add(SiparisDetay);


                    db.SaveChanges();
                    Session["sepet"] = null;
                    return RedirectToAction("Siparislerim", "Hesabim" /*, new { OdemeFiyatToplam = toplamfiyat }*/);
                }
                else
                {
                    return View("OdemeBasarısız");
                }
            }
            return View();
        }
        public ActionResult OdemeBasarılı()
        {

            return View();
        }
        public ActionResult OdemeBasarısız()
        {
            return View();
        }
    }
}
