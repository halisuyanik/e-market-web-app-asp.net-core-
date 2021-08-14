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
    public class AdminKategoriController : Controller
    {
        private readonly dbEticaretEntities db = new dbEticaretEntities();

        // GET: AdminKategori
        public ActionResult Index()
        {
            return View(db.Tbl_Kategori.ToList());
        }

        // GET: AdminKategori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Kategori tbl_Kategori = db.Tbl_Kategori.Find(id);
            if (tbl_Kategori == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kategori);
        }

        // GET: AdminKategori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminKategori/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategoriId,KategoriAdi")] Tbl_Kategori tbl_Kategori, HttpPostedFileBase gorsel)
        {
            if (ModelState.IsValid)
            {
                var uniqkategoriadi = db.Tbl_Kategori.Where(k => k.KategoriAdi == tbl_Kategori.KategoriAdi).SingleOrDefault();
                if (uniqkategoriadi == null)
                {
                    if (gorsel != null)
                    {
                        WebImage img = new WebImage(gorsel.InputStream);
                        FileInfo fotoinfo = new FileInfo(gorsel.FileName);
                        string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        img.Save("~/Uploads/KategoriGorsel/" + newfoto);
                        tbl_Kategori.KategoriResim = "/Uploads/KategoriGorsel/" + newfoto;
                        db.Tbl_Kategori.Add(tbl_Kategori);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        TempData["kategorisonuc"] = "Görsel alanı boş bırakılamaz.";
                    }
                }
                else
                {

                    TempData["kategorisonuc"] = "Böyle bir kategori zaten oluşturulmuş.";
                }

            }
            return View(tbl_Kategori);
        }

        // GET: AdminKategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Kategori tbl_Kategori = db.Tbl_Kategori.Find(id);
            if (tbl_Kategori == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kategori);
        }

        // POST: AdminKategori/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategoriId,KategoriAdi,IsActive,IsDelete,KategoriResim")] Tbl_Kategori tbl_Kategori, int id, HttpPostedFileBase gorsel)
        {
            if (ModelState.IsValid)
            {

                var kategori = db.Tbl_Kategori.Where(m => m.KategoriId == id).SingleOrDefault();
                if (gorsel != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(kategori.KategoriResim)))
                    {
                        System.IO.File.Delete(Server.MapPath(kategori.KategoriResim));
                    }
                    WebImage img = new WebImage(gorsel.InputStream);
                    FileInfo fotoinfo = new FileInfo(gorsel.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Save("~/Uploads/KategoriGorsel/" + newfoto);
                    kategori.KategoriResim = "/Uploads/KategoriGorsel/" + newfoto;
                }
                kategori.KategoriAdi = tbl_Kategori.KategoriAdi;
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            return View(tbl_Kategori);
        }

        // GET: AdminKategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Kategori tbl_Kategori = db.Tbl_Kategori.Find(id);
            if (tbl_Kategori == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kategori);
        }

        // POST: AdminKategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var kategori = db.Tbl_Kategori.Where(m => m.KategoriId == id).SingleOrDefault();
                if (System.IO.File.Exists(Server.MapPath(kategori.KategoriResim)))
                {
                    System.IO.File.Delete(Server.MapPath(kategori.KategoriResim));
                }

                db.Tbl_Kategori.Remove(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            { return View(); }

          
           
            
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
