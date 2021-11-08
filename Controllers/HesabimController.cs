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
        private dbEticaretEntities db = new dbEticaretEntities();


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
                DateTime fromDate = DateTime.Now;
                int uyeid = Convert.ToInt16(Session["uyeid"]);
                var siparisler = db.Tbl_SiparisDetay.Where(x => x.UyeId == uyeid).ToList();
                return View(siparisler.Where(x=>x.SiparisTarihi>=fromDate.AddDays(-30)));
            }
            else
            {
                RedirectToAction("Login", "Uye");
            }
            return View();
        }

      
        public ActionResult SiparislerimFilter(string filter)
        {
            
            if (Session["uyeid"] != null)
            {
                ViewBag.selectedfilter = "";
                ViewBag.selectedfilter = filter;
                if (filter == "last30day")
                {
                    DateTime fromDate = DateTime.Now;
                    int uyeid = Convert.ToInt16(Session["uyeid"]);
                    var siparisler = db.Tbl_SiparisDetay.Where(x => x.UyeId == uyeid).ToList();
                    return View(siparisler.Where(x => x.SiparisTarihi >= fromDate.AddDays(-30)));
                }
                 if (filter == "last3month")
                {
                    DateTime fromDate = DateTime.Now;
                    int uyeid = Convert.ToInt16(Session["uyeid"]);
                    var siparisler = db.Tbl_SiparisDetay.Where(x => x.UyeId == uyeid).ToList();
                    return View(siparisler.Where(x => x.SiparisTarihi >= fromDate.AddMonths(-3)));
                }
                 if (filter == "year2021")
                {
                    DateTime fromDate = Convert.ToDateTime("2021-12-31");
                    DateTime EndDate = Convert.ToDateTime("2021-01-01");
                    int uyeid = Convert.ToInt16(Session["uyeid"]);
                    var siparisler = db.Tbl_SiparisDetay.Where(x => x.UyeId == uyeid).ToList();
                    return View(siparisler.Where(x => x.SiparisTarihi <= fromDate && x.SiparisTarihi >= EndDate));
                }
                 if (filter == "year2020")
                {
                    DateTime fromDate = Convert.ToDateTime("2020-12-31");
                    DateTime EndDate = Convert.ToDateTime("2020-01-01");
                    int uyeid = Convert.ToInt16(Session["uyeid"]);
                    var siparisler = db.Tbl_SiparisDetay.Where(x => x.UyeId == uyeid).ToList();
                    return View(siparisler.Where(x => x.SiparisTarihi <= fromDate && x.SiparisTarihi>=EndDate));
                }
                

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
