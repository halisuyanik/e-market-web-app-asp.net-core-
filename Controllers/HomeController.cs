using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using on_e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using on_e_commerce.Helpers;


namespace on_e_commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly dbEticaretContext _context;

        public HomeController(dbEticaretContext context)
        {
            _context = context;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            var urunler = _context.TblUruns;
            return View(urunler);
        }

        public ActionResult _PartialKategori()
        {
            var kategories=_context.TblKategoris;
            return View(kategories);
        }
        public ActionResult SepetiOnayla()
        { 
            return View();
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
                return RedirectToAction("SepetiOnayla");
            }
            else
            {
                return RedirectToAction("SepetiOnayla");
            }

        }

        [HttpPost]
        public JsonResult ModalSepetUrunEkle(int urunid, string url)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet") == null)
            {
                List<Item> sepet = new List<Item>();
                var urun = _context.TblUruns.Find(urunid);
                sepet.Add(new Item()
                {
                    Urun = urun,
                    Count = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session,"sepet", sepet);
            }
            else
            {
                List<Item> sepet = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet");
                var urunsayisi = sepet.Count();
                var urun = _context.TblUruns.Find(urunid);
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
                SessionHelper.SetObjectAsJson(HttpContext.Session,"sepet",sepet);
            }

            return Json(SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"sepet")) ;
        }
        public ActionResult UrunuCıkar(int urunid)
        {
            List<Item> sepet = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"sepet");
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet") != null)
            {

                var urun = _context.TblUruns.Where(x => x.UrunId == urunid).SingleOrDefault();
                foreach (var item in sepet)
                {
                    if (item.Urun.UrunId == urunid)
                    {
                        sepet.Remove(item);


                        break;
                    }
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session,"sepet",sepet);
               
                if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet") == null)
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session,"sepet", null);
                }
            }

            return Redirect("SepetiOnayla");
        }
        public ActionResult UrunEksilt(int urunid)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,"sepet") != null)
            {
                List<Item> sepet = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet");
                var urun = _context.TblUruns.Where(x => x.UrunId == urunid).SingleOrDefault();
                foreach (var item in sepet)
                {
                    if (item.Urun.UrunId == urunid)
                    {
                        int prevQty = item.Count;
                        if (prevQty == 1)
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
                SessionHelper.SetObjectAsJson(HttpContext.Session,"sepet", sepet);
                
            }
            return Redirect("SepetiOnayla");
        }
        public ActionResult UrunArttır(int urunid, string url)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet") != null)
            {
                List<Item> sepet = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "sepet");
                var urun = _context.TblUruns.Where(x => x.UrunId == urunid).SingleOrDefault();
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
                SessionHelper.SetObjectAsJson(HttpContext.Session,"sepet",sepet);
               
            }
            return Redirect(url);
        }
        public ActionResult SepetiTemizle()
        {
            SessionHelper.SetObjectAsJson(HttpContext.Session,"sepet",null);
            return View();
        }

       


    }
}
