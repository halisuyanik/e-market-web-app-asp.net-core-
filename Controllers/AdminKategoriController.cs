using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using on_e_commerce.Models;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace on_e_commerce.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class AdminKategoriController : Controller
    {
        private readonly dbEticaretContext _context;

        public AdminKategoriController(dbEticaretContext context)
        {
            _context = context;
        }

        // GET: AdminKategori
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblKategoris.ToListAsync());
        }

        // GET: AdminKategori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblKategori = await _context.TblKategoris
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (tblKategori == null)
            {
                return NotFound();
            }

            return View(tblKategori);
        }

        // GET: AdminKategori/Create
        public IActionResult Create()
        {
            return View();
        }

      
        // POST: AdminKategori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategoriId,KategoriAdi")] TblKategori tblKategori, IFormFile gorsel)
        {
            if (ModelState.IsValid)
            {
                var uniqkategoriadi = _context.TblKategoris.Where(k => k.KategoriAdi == tblKategori.KategoriAdi).SingleOrDefault() ;
                if (uniqkategoriadi==null)
                {
                    if (gorsel!=null)
                    {
                        string imageExtension = Path.GetExtension(gorsel.FileName);
                        string imageName = Guid.NewGuid()+imageExtension;
                        tblKategori.KategoriResim = "wwwroot/Uploads/KategoriGorsel/" + imageName;
                        string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/KategoriGorsel/{imageName}");
                        using var stream = new FileStream(path,FileMode.Create);
                        await gorsel.CopyToAsync(stream);
                        _context.Add(tblKategori);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
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
            return View(tblKategori);
        }

        // GET: AdminKategori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblKategori = await _context.TblKategoris.FindAsync(id);
            if (tblKategori == null)
            {
                return NotFound();
            }
            return View(tblKategori);
        }

        // POST: AdminKategori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategoriId,KategoriAdi,IsActive,IsDelete,KategoriResim")] TblKategori tblKategori, IFormFile gorsel)
        {
            if (id != tblKategori.KategoriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var kategori = _context.TblKategoris.Where(x=>x.KategoriId==id).SingleOrDefault();
                if (gorsel!=null)
                {

                    var currentpath = Path.Combine(Directory.GetCurrentDirectory(), kategori.KategoriResim);

                    if (System.IO.File.Exists(currentpath))
                    {
                        System.IO.File.Delete(currentpath);
                    }
                    string imageExtension = Path.GetExtension(gorsel.FileName);
                    string imageName = Guid.NewGuid() + imageExtension;
                    tblKategori.KategoriResim = "wwwroot/Uploads/KategoriGorsel/" + imageName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/KategoriGorsel/{imageName}");
                    using var stream = new FileStream(path, FileMode.Create);
                    await gorsel.CopyToAsync(stream);
                    _context.Add(tblKategori);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    TempData["kategorisonuc"] = "Görsel alanı boş bırakılamaz.";
                }


            }
            return View(tblKategori);
        }

        // GET: AdminKategori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblKategori = await _context.TblKategoris
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (tblKategori == null)
            {
                return NotFound();
            }

            return View(tblKategori);
        }

        // POST: AdminKategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblKategori = await _context.TblKategoris.FindAsync(id);
            var currentpath = Path.Combine(Directory.GetCurrentDirectory(), tblKategori.KategoriResim);
            if (System.IO.File.Exists(currentpath))
            {
                System.IO.File.Delete(currentpath);
            }
            _context.TblKategoris.Remove(tblKategori);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblKategoriExists(int id)
        {
            return _context.TblKategoris.Any(e => e.KategoriId == id);
        }
    }
}
