using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using on_e_commerce.Models;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using on_e_commerce.Helpers;
using Microsoft.AspNetCore.Http;


namespace on_e_commerce.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {

        readonly UserManager<AppUser> _userManager;
        private readonly dbEticaretContext _context;

        public PaymentController(dbEticaretContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
        }

        
        public ActionResult SepetAction(List<Item> urunler)
        {
            if (urunler != null)
            {
                List<Item> sepet = new List<Item>();
                for (int i = 0; i < urunler.Count(); i++)
                {
                    var urun = _context.TblUruns.Find(urunler[i].UrunId);
                    sepet.Add(new Item()
                    {
                        Urun = urun,
                        UrunId = urunler[i].UrunId,
                        Name = urunler[i].Name,
                        Price = urunler[i].Price,
                        Count = urunler[i].Count,
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "sepet", sepet);
                //Session["sepet"] = sepet;
                //List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet")
                return RedirectToAction("Checkout");
            }
            else
            {
                return RedirectToAction("SepetiOnayla");
            }
        }

        public ActionResult SepetiOnayla()
        { 
            return View();
        }

        public ActionResult Checkout()
        { 
            ViewBag.Sehirler = _context.Sehirlers.OrderBy(x => x.SehirAdi).ToList();
            List<Item> sepet = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet");
            ViewBag.sepet = sepet;
            return View();      
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(TblSiparisDetay SiparisDetay, string il,string ilce)
        { 
            if(ModelState.IsValid)
            {
                int toplamfiyat = 0;
                List<Item> sepet = (List<Item>)SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet");
                if (sepet != null)
                    {
                        string siparisbarkod=Guid.NewGuid().ToString();
                        for(int i=0; i<sepet.Count;i++)
                        {
                            _context.TblUruns.Find(sepet[i].Urun.UrunId).Miktar=_context.TblUruns.Find(sepet[i].Urun.UrunId).Miktar - sepet[i].Count;
                            TblSiparisUrunleri siparisurunleri=new TblSiparisUrunleri();
                            siparisurunleri.Adet=sepet[i].Count;
                            siparisurunleri.UrunId=sepet[i].Urun.UrunId;
                            siparisurunleri.SiparisBarkod=siparisbarkod;
                            siparisurunleri.UyeId= _userManager.GetUserId(User);
                            toplamfiyat += Convert.ToInt16(sepet[i].Price) * sepet[i].Count;
                            _context.TblSiparisUrunleris.Add(siparisurunleri);
                        }
                        SiparisDetay.Sehir = il;
                        SiparisDetay.Ilce = ilce;
                        SiparisDetay.Ulke = "Türkiye";
                        SiparisDetay.ToplamFiyat = toplamfiyat;
                        SiparisDetay.SiparisDurumu = 1;
                        SiparisDetay.SiparisTarihi = DateTime.Now;
                        SiparisDetay.UyeId= _userManager.GetUserId(User);
                        SiparisDetay.SiparisBarkod = siparisbarkod;
                        _context.TblSiparisDetays.Add(SiparisDetay);
                        await _context.SaveChangesAsync();
                        SiparisTamamlandır();
                        //return RedirectToAction("MyOrders", "Account" /*, new { OdemeFiyatToplam = toplamfiyat }*/);
                        return View("OdemeBasarili"); 
                    }               
                else
                    {
                    return View("OdemeBasarısız");
                    }
            }
            return View();      
        }
        public void SiparisTamamlandır()
        {
            List<Item> sepet = (List<Item>)SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet");
            for(int i=0; i<sepet.Count;i++)
            {
                _context.TblUruns.Find(sepet[i].Urun.UrunId).Miktar=_context.TblUruns.Find(sepet[i].Urun.UrunId).Miktar - sepet[i].Count;
            }
            _context.SaveChangesAsync();
        }
        public ActionResult OdemeBasarisiz()
        {
            return View();
        }
        public ActionResult OdemeBasarili()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Ilceler(int SehirId)
        {
            int sehirid = Convert.ToInt32(SehirId);
            //var ilceler = _context.Ilcelers.Where(x => x.SehirId == sehirid).Select(i => new { Id = i.IlceId, Ad = i.IlceAdi }).ToList();
            var ilceler =  _context.Ilcelers.Where(x => x.SehirId == sehirid).Select(i => new { id = i.ilceId, ad = i.IlceAdi }).ToList();
            return Json(ilceler);

        }
        
    }
}