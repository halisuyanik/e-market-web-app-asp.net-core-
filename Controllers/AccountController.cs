using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using on_e_commerce.Models;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using on_e_commerce.Helpers;

namespace on_e_commerce.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        private readonly dbEticaretContext _context;

        public AccountController(dbEticaretContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult MyOrders()
        {
            DateTime fromDate = DateTime.Now;
            string idd=_userManager.GetUserId(User).ToString();

            var orders= _context.TblSiparisDetays.Where(x=>x.UyeId==idd).ToList();
            return View( orders.Where(x=>x.SiparisTarihi>=fromDate.AddDays(-30)));
          
        }
        public IActionResult MyOrderstry()
        { 
            DateTime fromDate = DateTime.Now;
            string idd=_userManager.GetUserId(User).ToString();

            var orders= _context.TblSiparisDetays.Where(x=>x.UyeId==idd).ToList();
            return View( orders.Where(x=>x.SiparisTarihi>=fromDate.AddDays(-30)));
        }
        public IActionResult MyOrders1()
        {
            
            DateTime fromDate = DateTime.Now;
            string idd=_userManager.GetUserId(User).ToString();

            var orders= _context.TblSiparisDetays.Where(x=>x.UyeId==idd).ToList();
            return View( orders.Where(x=>x.SiparisTarihi>=fromDate.AddDays(-30)));
        }

        public ActionResult SiparislerimFilter(string filter)
        { string id=_userManager.GetUserId(User).ToString();
                if (filter == "last30day")
                {
                    DateTime fromDate = DateTime.Now;
                    var siparisler = _context.TblSiparisDetays.Where(x=>x.UyeId==id).ToList();
                    return View(siparisler.Where(x => x.SiparisTarihi >= fromDate.AddDays(-30)));
                }
                 if (filter == "last3month")
                {
                    DateTime fromDate = DateTime.Now;
                    var siparisler = _context.TblSiparisDetays.Where(x=>x.UyeId==id).ToList();
                    return View(siparisler.Where(x => x.SiparisTarihi >= fromDate.AddMonths(-3)));
                }
                 if (filter == "year2021")
                {
                    DateTime fromDate = Convert.ToDateTime("2021-12-31");
                    DateTime EndDate = Convert.ToDateTime("2021-01-01");
                    var siparisler = _context.TblSiparisDetays.Where(x=>x.UyeId==id).ToList();
                    return View(siparisler.Where(x => x.SiparisTarihi <= fromDate && x.SiparisTarihi >= EndDate));
                }
                 if (filter == "year2020")
                {
                    DateTime fromDate = Convert.ToDateTime("2020-12-31");
                    DateTime EndDate = Convert.ToDateTime("2020-01-01");
                    var siparisler = _context.TblSiparisDetays.Where(x=>x.UyeId==id).ToList();
                    return View(siparisler.Where(x => x.SiparisTarihi <= fromDate && x.SiparisTarihi>=EndDate));
                }
            return View();
        }



        [BindProperty]

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        
        
    }
}
