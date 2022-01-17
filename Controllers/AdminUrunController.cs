using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using on_e_commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace on_e_commerce.Controllers
{
    [Authorize(Roles = "Admin, Manager, Editor")]
    public class AdminUrunController : Controller
    {
        private readonly dbEticaretContext _context;

        public AdminUrunController(dbEticaretContext context)
        {
            _context = context;
        }

        // GET: AdminUrun
        public async Task<IActionResult> Index()
        {
            var dbEticaretContext = _context.TblUruns.Include(t => t.Kategori);
            return View(await dbEticaretContext.ToListAsync());
        }

        // GET: AdminUrun/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUrun = await _context.TblUruns
                .Include(t => t.Kategori)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (tblUrun == null)
            {
                return NotFound();
            }

            return View(tblUrun);
        }

        // GET: AdminUrun/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.TblKategoris, "KategoriId", "KategoriAdi");
            return View();
        }

        // POST: AdminUrun/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrunId,UrunAdi,KategoriId,IsActive,IsDelete,OlusturulmaTarihi,DegistirilmeTarihi,Fiyat,Acıklama,UrunResim,OneCıkan,Miktar,Agırlık")] TblUrun tblUrun, string agirliktürü, IFormFile gorsel)
        {
            if (ModelState.IsValid)
            {
                if (gorsel!=null)
                {
                    string imageExtension = Path.GetExtension(gorsel.FileName);
                    string imageName = Guid.NewGuid() + imageExtension;
                    tblUrun.UrunResim = "wwwroot/Uploads/UrunGorsel/" + imageName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/UrunGorsel/{imageName}");
                    using var stream = new FileStream(path, FileMode.Create);
                    await gorsel.CopyToAsync(stream);

                    tblUrun.OlusturulmaTarihi = DateTime.Now;
                    tblUrun.Agırlık = tblUrun.Agırlık+" "+ agirliktürü;
                    _context.Add(tblUrun);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.HataMesaji = "Görsel alanı boş bırakılamaz.";
                }
            }
            ViewData["KategoriId"] = new SelectList(_context.TblKategoris, "KategoriId", "KategoriAdi", tblUrun.KategoriId);
            return View(tblUrun);
        }

        // GET: AdminUrun/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUrun = await _context.TblUruns.FindAsync(id);
            if (tblUrun == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.TblKategoris, "KategoriId", "KategoriAdi", tblUrun.KategoriId);
            return View(tblUrun);
        }

        // POST: AdminUrun/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UrunId,UrunAdi,KategoriId,IsActive,IsDelete,OlusturulmaTarihi,DegistirilmeTarihi,Fiyat,Acıklama,UrunResim,OneCıkan,Miktar,Agırlık")] TblUrun tblUrun,IFormFile gorsel)
        {
            if (id != tblUrun.UrunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var urun = _context.TblUruns.Where(x => x.KategoriId == id).SingleOrDefault();
                    if (gorsel!=null)
                    {
                        var currenturun = Path.Combine(Directory.GetCurrentDirectory(),urun.UrunResim );
                        if (System.IO.File.Exists(currenturun))
                        {
                            System.IO.File.Delete(currenturun);
                        }
                        string imageExtension = Path.GetExtension(gorsel.FileName);
                        string imageName = Guid.NewGuid() + imageExtension;
                        tblUrun.UrunResim = "wwwroot/Uploads/UrunGorsel/" + imageName;
                        string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Uploads/UrunGorsel/{imageName}");
                        using var stream = new FileStream(path, FileMode.Create);
                        await gorsel.CopyToAsync(stream);

                        tblUrun.DegistirilmeTarihi = DateTime.Now;
                        _context.Update(tblUrun);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ViewBag.HataMesaji = "Görsel alanı boş bırakılamaz.";
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblUrunExists(tblUrun.UrunId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.TblKategoris, "KategoriId", "KategoriAdi", tblUrun.KategoriId);
            return View(tblUrun);
        }

        // GET: AdminUrun/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblUrun = await _context.TblUruns
                .Include(t => t.Kategori)
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (tblUrun == null)
            {
                return NotFound();
            }

            return View(tblUrun);
        }

        // POST: AdminUrun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblUrun = await _context.TblUruns.FindAsync(id);
            var currenturun = Path.Combine(Directory.GetCurrentDirectory(), tblUrun.UrunResim);
            if (System.IO.File.Exists(currenturun))
            {
                System.IO.File.Delete(currenturun);
            }

            _context.TblUruns.Remove(tblUrun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblUrunExists(int id)
        {
            return _context.TblUruns.Any(e => e.UrunId == id);
        }
    }
}
