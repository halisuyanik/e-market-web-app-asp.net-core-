using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using on_e_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using on_e_commerce.Helpers;

namespace on_e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductApiController : ControllerBase
    {
        private readonly dbEticaretContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public OrderProductApiController(dbEticaretContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/OrderProductApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblSiparisUrunleri>>> GetTblSiparisUrunleris()
        {
            return await _context.TblSiparisUrunleris.ToListAsync();
        }

        // GET: api/OrderProductApi/5
        [HttpGet("{SiparisBarkod}")]
        public async Task<ActionResult<IEnumerable<TblSiparisUrunleri>>> GetTblSiparisUrunleri(string SiparisBarkod)
        {
          //  var tblSiparisUrunleri = await _context.TblSiparisUrunleris.FindAsync(id);

            //if (tblSiparisUrunleri == null)
           // {
            //    return NotFound();
            //}

            //return tblSiparisUrunleri;


            string barkod=SiparisBarkod.ToString();
            
            var products= await _context.TblSiparisUrunleris.Where(x=>x.SiparisBarkod==SiparisBarkod).ToListAsync();
            return products;
        }

        // PUT: api/OrderProductApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblSiparisUrunleri(int id, TblSiparisUrunleri tblSiparisUrunleri)
        {
            if (id != tblSiparisUrunleri.SiparisId)
            {
                return BadRequest();
            }

            _context.Entry(tblSiparisUrunleri).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblSiparisUrunleriExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderProductApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblSiparisUrunleri>> PostTblSiparisUrunleri(TblSiparisUrunleri tblSiparisUrunleri)
        {
            _context.TblSiparisUrunleris.Add(tblSiparisUrunleri);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblSiparisUrunleri", new { id = tblSiparisUrunleri.SiparisId }, tblSiparisUrunleri);
        }

        // DELETE: api/OrderProductApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblSiparisUrunleri(int id)
        {
            var tblSiparisUrunleri = await _context.TblSiparisUrunleris.FindAsync(id);
            if (tblSiparisUrunleri == null)
            {
                return NotFound();
            }

            _context.TblSiparisUrunleris.Remove(tblSiparisUrunleri);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblSiparisUrunleriExists(int id)
        {
            return _context.TblSiparisUrunleris.Any(e => e.SiparisId == id);
        }
    }
}
