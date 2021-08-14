using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using on_e_commerce.Models;
using on_e_commerce.data;

namespace on_e_commerce.Controllers
{
    public class AdminController : Controller
    {
        public dbEticaretEntities db = new dbEticaretEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }

    }
