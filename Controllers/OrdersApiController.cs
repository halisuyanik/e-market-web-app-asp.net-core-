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
    public class OrdersApiController : ControllerBase
    {
        private readonly dbEticaretContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        
        
        public OrdersApiController(dbEticaretContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/OrdersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblSiparisDetay>>> GetTblSiparisDetays()
        {
            if(_userManager.GetUserId(User)!=null){
                  DateTime fromDate = DateTime.Now;
            Guid id=Guid.Parse(_userManager.GetUserId(User));
            string idd=_userManager.GetUserId(User).ToString();
            return await _context.TblSiparisDetays.Where(x=>x.UyeId==idd).ToListAsync();
            }
            else{
            return NotFound();
            }
           
        }

        // GET: api/OrdersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblSiparisDetay>> GetTblSiparisDetay(int id)
        {
            var tblSiparisDetay = await _context.TblSiparisDetays.FindAsync(id);

            if (tblSiparisDetay == null)
            {
                return NotFound();
            }
            if(_userManager.GetUserId(User)!=null){
            return tblSiparisDetay;
            }
            else{return NotFound();}
        }

        // PUT: api/OrdersApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblSiparisDetay(int id, TblSiparisDetay tblSiparisDetay)
        {
            if (id != tblSiparisDetay.SiparisDetayId)
            {
                return BadRequest();
            }

            _context.Entry(tblSiparisDetay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblSiparisDetayExists(id))
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

        // POST: api/OrdersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblSiparisDetay>> PostTblSiparisDetay(TblSiparisDetay tblSiparisDetay)
        {
            _context.TblSiparisDetays.Add(tblSiparisDetay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblSiparisDetay", new { id = tblSiparisDetay.SiparisDetayId }, tblSiparisDetay);
        }

        // DELETE: api/OrdersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblSiparisDetay(int id)
        {
            var tblSiparisDetay = await _context.TblSiparisDetays.FindAsync(id);
            if (tblSiparisDetay == null)
            {
                return NotFound();
            }

            _context.TblSiparisDetays.Remove(tblSiparisDetay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblSiparisDetayExists(int id)
        {
            return _context.TblSiparisDetays.Any(e => e.SiparisDetayId == id);
        }
    }
}
