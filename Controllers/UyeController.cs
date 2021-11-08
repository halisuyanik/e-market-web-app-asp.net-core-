using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using on_e_commerce.data;
using Scrypt;

namespace on_e_commerce.Controllers
{
    public class UyeController : Controller
    {
        private dbEticaretEntities db = new dbEticaretEntities();

        // GET: Uye
        public ActionResult Index()
        {
            return View(db.Tbl_Uye.ToList());
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string pass)
        {
            Session.Abandon();
            ScryptEncoder encoder = new ScryptEncoder();
            var log = db.Tbl_Uye.Where(u=>u.Email==email).SingleOrDefault();
            
            if (log!=null)
            {
                bool isValidUser = encoder.Compare(pass, log.Sifre);
                if (isValidUser)
                {
                    TempData["girissonuc"] = "";
                    Session["uyeid"] = log.UyeId;
                    Session["uyead"] = log.Adi;
                    Session["uyesoyadi"] = log.Soyadi;
                    Session["uyeemail"] = log.Email;
                    Session["uyeyetkiid"] = log.yetkiid;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["girissonuc"] = "Kullanıcı adı veya şifre yanlış.";
                    return RedirectToAction("Login", "Uye");
                }
               
            }
            
            else
            {
                TempData["girissonuc"]= "Kullanıcı adı veya şifre yanlış.";
                return RedirectToAction("Login", "Uye");
            }
        }
        public ActionResult Loginoto(string email, string pass)
        {

            ScryptEncoder encoder = new ScryptEncoder();
            var log = db.Tbl_Uye.Where(u => u.Email == email).SingleOrDefault();

            if (log != null)
            {
                bool isValidUser = encoder.Compare(pass, log.Sifre);
                if (isValidUser)
                {
                    TempData["girissonuc"] = "";
                    Session["uyeid"] = log.UyeId;
                    Session["uyead"] = log.Adi;
                    Session["uyesoyadi"] = log.Soyadi;
                    Session["uyeemail"] = log.Email;
                    Session["uyeyetkiid"] = log.yetkiid;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["girissonuc"] = "Kullanıcı adı veya şifre yanlış.";
                    return RedirectToAction("Login", "Uye");
                }

            }

            else
            {
                TempData["girissonuc"] = "Kullanıcı adı veya şifre yanlış.";
                return RedirectToAction("Login", "Uye");
            }
        }


        public ActionResult Logout()
        {
            Session["yetkiid"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }




        // GET: Uye/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Uye tbl_Uye = db.Tbl_Uye.Find(id);
            if (tbl_Uye == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Uye);
        }

        // GET: Uye/Create
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Uye/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "Adi,Soyadi,Email,Sifre")] Tbl_Uye tbl_Uye , string confirmpassword)
        {
            Session.Abandon();
            TempData["kayitsonuc"] = null;
            if (ModelState.IsValid)
            {
                
                var emailkontrol = db.Tbl_Uye.Where(e => e.Email == tbl_Uye.Email).SingleOrDefault();
                if (emailkontrol==null)
                {
                    if (tbl_Uye.Sifre.Length >= 8)
                    {
                        if (tbl_Uye.Sifre == confirmpassword)
                        {
                            ScryptEncoder encoder = new ScryptEncoder();
                            tbl_Uye.OlusturulmaTarihi = DateTime.Now;
                            tbl_Uye.yetkiid = 2;
                            tbl_Uye.Sifre = encoder.Encode(tbl_Uye.Sifre);
                            db.Tbl_Uye.Add(tbl_Uye);
                            db.SaveChanges();
                            return RedirectToAction("Loginoto", new { email = tbl_Uye.Email, pass = confirmpassword });
                        }
                        else
                        {
                            TempData["kayitsonuc"] = "Şifreler birbirleriyle uyuşmuyor.";
                        }
                    }
                    else
                    {
                        TempData["kayitsonuc"] = "Şifre Min 8 karakter içermelidir.";
                    }
                }
                else
                {
                    TempData["kayitsonuc"] = "Daha önce bu mail ile bir kayıt işlemi yapılmış.";
                }
               
               
            }

            return View(tbl_Uye);
        }

        // GET: Uye/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Uye tbl_Uye = db.Tbl_Uye.Find(id);
            if (tbl_Uye == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Uye);
        }

        // POST: Uye/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UyeId,Adi,Soyadi,Email,Sifre,IsActive,IsDelete,OlusturulmaTarihi,DegistirilmeTarihi")] Tbl_Uye tbl_Uye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Uye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Uye);
        }

        // GET: Uye/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Uye tbl_Uye = db.Tbl_Uye.Find(id);
            if (tbl_Uye == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Uye);
        }

        // POST: Uye/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Uye tbl_Uye = db.Tbl_Uye.Find(id);
            db.Tbl_Uye.Remove(tbl_Uye);
            db.SaveChanges();
            return RedirectToAction("Index");
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
