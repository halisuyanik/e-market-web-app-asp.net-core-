using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using on_e_commerce.data;
using on_e_commerce.Models;

namespace on_e_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly dbEticaretEntities db = new dbEticaretEntities();
        private readonly Item db1 = new Item();
        public ActionResult Index()
        {
            var urunler = db.Tbl_Urun;
            return View(urunler);
        }
        public ActionResult KategoriPartial()
        {
            var kategoriler = db.Tbl_Kategori;
            return View(kategoriler);
        }

        public ActionResult SepetAction(List<Item> urunler)
        {
            if (urunler != null)
            {
                List<Item> sepet = new List<Item>();
                for (int i = 0; i < urunler.Count(); i++)
                {
                    var urun = db.Tbl_Urun.Find(urunler[i].UrunId);
                    sepet.Add(new Item()
                    {
                        Urun = urun,
                        UrunId = urunler[i].UrunId,
                        Name = urunler[i].Name,
                        Price = urunler[i].Price,
                        Count = urunler[i].Count,
                    });
                }
                Session["sepet"] = sepet;
                return RedirectToAction("SepetiOnayla");
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

        [HttpPost]
        public JsonResult ModalSepetUrunEkle (int urunid, string url)
        {
            if (Session["sepet"] == null)
            {
                List<Item> sepet = new List<Item>();
                var urun = db.Tbl_Urun.Find(urunid);
                sepet.Add(new Item()
                {
                    Urun = urun,
                    Count = 1
                });
                Session["sepet"] = sepet;
            }
            else
            {
                List<Item> sepet = (List<Item>)Session["cart"];
                var urunsayisi = sepet.Count();
                var urun = db.Tbl_Urun.Find(urunid);
                for (int i = 0; i < urunsayisi; i++)
                {
                    if (sepet[i].Urun.UrunId == urunid)
                    {
                        int prevQty = sepet[i].Count;
                        sepet.Remove(sepet[i]);
                        sepet.Add(new Item()
                        {
                            Urun = urun,
                            Count = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = sepet.Where(x => x.Urun.UrunId == urunid).SingleOrDefault();
                        if (prd == null)
                        {
                            sepet.Add(new Item()
                            {
                                Urun = urun,
                                Count = 1
                            });
                        }
                    }
                }
                Session["sepet"] = sepet;
            }

            return Json(Session["sepet"]);
        }
        public ActionResult UrunuCıkar(int urunid)
        {
            List<Item> sepet = (List<Item>)Session["sepet"];
            if (Session["sepet"] != null)
            {

                var urun = db.Tbl_Urun.Where(x => x.UrunId == urunid).SingleOrDefault();
                foreach (var item in sepet)
                {
                    if (item.Urun.UrunId == urunid)
                    {
                        sepet.Remove(item);


                        break;
                    }
                }
                Session["sepet"] = sepet;
                if (Session["sepet"]==null)
                {
                    Session["sepet"] = null;
                }
            }

            return Redirect("SepetiOnayla");
        }
        public ActionResult UrunEksilt(int urunid)
        {
            if (Session["sepet"] != null)
            {
                List<Item> sepet = (List<Item>)Session["sepet"];
                var urun = db.Tbl_Urun.Where(x => x.UrunId == urunid).SingleOrDefault();
                foreach (var item in sepet)
                {
                    if (item.Urun.UrunId == urunid)
                    {
                        int prevQty = item.Count;
                        if (prevQty==1)
                        {
                            sepet.Remove(item);
                        }
                        //if (prevQty > 0)
                        //{
                        //    sepet.Remove(item);
                        //    sepet.Add(new Item()
                        //    {
                        //        Urun = urun,
                        //        Count = prevQty - 1
                        //    });
                        //}
                        else 
                        {
                            sepet.Remove(item);
                            sepet.Add(new Item()
                            {
                                Urun = urun,
                                Count = prevQty - 1
                            });
                        }

                        break;
                    }
                }
                Session["sepet"] = sepet;
            }
            return Redirect("SepetiOnayla");
        }
        public ActionResult UrunArttır(int urunid, string url)
        {
            if (Session["sepet"] != null)
            {
                List<Item> sepet = (List<Item>)Session["sepet"];
                var urun = db.Tbl_Urun.Where(x => x.UrunId == urunid).SingleOrDefault();
                foreach (var item in sepet)
                {
                    if (item.Urun.UrunId == urunid)
                    {
                        int prevQty = item.Count;
                        sepet.Remove(item);
                        sepet.Add(new Item()
                        {
                            Urun = urun,
                            Count = prevQty + 1
                        });
                        break;
                    }
                }

                Session["sepet"] = sepet;
            }
            return Redirect(url);
        }
        public ActionResult SepetiTemizle()
        {
            Session["sepet"] = null;
            return View();
        }
        public ActionResult SiparisiTamamla()
        {
            ViewBag.Sehirler = db.Sehirler.OrderBy(x => x.SehirAdi).ToList();
            return View();

        }
        [HttpPost]
        public ActionResult SiparisiTamamla(int totalfiyat)
        {

            return View();

        }       
        [HttpPost]
        public JsonResult Ilceler(int SehirId)
        {
            int sehirid = Convert.ToInt32(SehirId);
            var ilceler = db.Ilceler.Where(x => x.SehirId == sehirid).Select(i => new { Id = i.ilceId, Ad = i.IlceAdi }).ToList();
            
            return Json(ilceler);

        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}