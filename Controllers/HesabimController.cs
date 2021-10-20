using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using on_e_commerce.data;
using on_e_commerce.Models;

namespace on_e_commerce.Controllers
{
    public class HesabimController : Controller
    {
        private dbEticaretEntities1 db = new dbEticaretEntities1();
        // GET: Hesabim
        public ActionResult Index()
        {
            if (Session["uyeid"] != null)
            {
                return View();
            }
            else
            {
                RedirectToAction("Login", "Uye");
            }
            return View();
        }
        public ActionResult Siparislerim()
        {
            if (Session["uyeid"]!=null)
            {
                int uyeid = Convert.ToInt16(Session["uyeid"]);
                var siparisler = db.Tbl_SiparisDetay.Where(x => x.UyeId == uyeid).ToList();
                return View(siparisler.OrderByDescending(x=>x.SiparisTarihi));
            }
            else
            {
                RedirectToAction("Login", "Uye");
            }
            return View();
        }
        
        public List<Tbl_SiparisUrunleri> SiparisUrunleri(string siparisbarkod)
        {
            var urunler = db.Tbl_SiparisUrunleri.Where(x=>x.SiparisBarkod==siparisbarkod).ToList();
            return urunler;
        }


    }
}