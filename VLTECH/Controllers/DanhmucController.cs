using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLTECH.Models;

namespace VLTECH.Controllers
{
    public class DanhmucController : Controller
    {
        Qlbanhang db = new Qlbanhang();
        // GET: Danhmuc
        public ActionResult DanhmucPartial()
        {
            var danhmuc = db.Hangsanxuats.ToList();
            return PartialView(danhmuc);
        }
    }
}