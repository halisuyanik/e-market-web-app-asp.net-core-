using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using on_e_commerce.data;

namespace on_e_commerce.Controllers
{
    public class AdminUrunController : Controller
    {
        private dbEticaretEntities db = new dbEticaretEntities();

        // GET: AdminUrun
        public ActionResult Index()
        {
            var tbl_Urun = db.Tbl_Urun.Include(t => t.Tbl_Kategori);
            return View(tbl_Urun.ToList());
        }

        // GET: AdminUrun/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Urun tbl_Urun = db.Tbl_Urun.Find(id);
            if (tbl_Urun == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Urun);
        }

        // GET: AdminUrun/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Tbl_Kategori, "KategoriId", "KategoriAdi");
            return View();
        }

        // POST: AdminUrun/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunId,UrunAdi,KategoriId,IsActive,IsDelete,OlusturulmaTarihi,DegistirilmeTarihi,Fiyat,Acıklama,UrunResim,OneCıkan,Miktar,Agırlık")] Tbl_Urun tbl_Urun , string agirliktürü, HttpPostedFileBase gorsel)
        {
            if (ModelState.IsValid)
            {
                if (gorsel != null)
                {
                    WebImage img = new WebImage(gorsel.InputStream);
                    FileInfo fotoinfo = new FileInfo(gorsel.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Save("~/Uploads/UrunGorsel/" + newfoto);
                    tbl_Urun.UrunResim = "/Uploads/UrunGorsel/" + newfoto;
                    tbl_Urun.OlusturulmaTarihi = DateTime.Now;
                    tbl_Urun.Agırlık = tbl_Urun.Agırlık +" "+ agirliktürü;
                    db.Tbl_Urun.Add(tbl_Urun);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    //Response.Write(@"< script language = 'javascript' > alert('Görsel alanı boş bırakılamaz.');</ script > ");
                    ViewBag.HataMesaji = "Görsel alanı boş bırakılamaz.";
                }
               
            }

            ViewBag.KategoriId = new SelectList(db.Tbl_Kategori, "KategoriId", "KategoriAdi", tbl_Urun.KategoriId);
            return View(tbl_Urun);
        }

        // GET: AdminUrun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Urun tbl_Urun = db.Tbl_Urun.Find(id);
            if (tbl_Urun == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Tbl_Kategori, "KategoriId", "KategoriAdi", tbl_Urun.KategoriId);
            return View(tbl_Urun);
        }

        // POST: AdminUrun/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunId,UrunAdi,KategoriId,IsActive,IsDelete,OlusturulmaTarihi,DegistirilmeTarihi,Fiyat,Acıklama,UrunResim,OneCıkan,Miktar,Agırlık")] Tbl_Urun tbl_Urun ,int id, HttpPostedFileBase gorsel)
        {
            try
            {
                var urun = db.Tbl_Urun.Where(m=>m.UrunId==id).SingleOrDefault();
                if (gorsel != null)
                {
                        if (System.IO.File.Exists(Server.MapPath(urun.UrunResim)))
                        {
                            System.IO.File.Delete(Server.MapPath(urun.UrunResim));
                        }
                        WebImage img = new WebImage(gorsel.InputStream);
                        FileInfo fotoinfo = new FileInfo(gorsel.FileName);
                        string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        img.Save("~/Uploads/UrunGorsel/" + newfoto);
                        urun.UrunResim = "/Uploads/UrunGorsel/" + newfoto;
                }
                urun.UrunAdi = tbl_Urun.UrunAdi;
                urun.Acıklama = tbl_Urun.Acıklama;
                urun.KategoriId = tbl_Urun.KategoriId;
                urun.Fiyat = tbl_Urun.Fiyat;
                urun.Miktar = tbl_Urun.Miktar;
                urun.DegistirilmeTarihi = DateTime.Now;
                urun.Agırlık = tbl_Urun.Agırlık;
                ViewBag.KategoriId = new SelectList(db.Tbl_Kategori, "KategoriId", "KategoriAdi", tbl_Urun.KategoriId);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            catch
            {
                ViewBag.KategoriId = new SelectList(db.Tbl_Kategori, "KategoriId", "KategoriAdi", tbl_Urun.KategoriId);
                return View(tbl_Urun);
            }    
        }

        // GET: AdminUrun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Urun tbl_Urun = db.Tbl_Urun.Find(id);
            if (tbl_Urun == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Urun);
        }

        // POST: AdminUrun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var urun = db.Tbl_Urun.Where(m=>m.UrunId==id).SingleOrDefault();
                if (System.IO.File.Exists(Server.MapPath(urun.UrunResim)))
                {
                    System.IO.File.Delete(Server.MapPath(urun.UrunResim));
                }

                db.Tbl_Urun.Remove(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }

            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
