using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using VLTECH.Models;


namespace VLTECH.Areas.Admin.Controllers
{
    public class BaocaosController : BaseController
    {
        private Qlbanhang db = new Qlbanhang();
        public ActionResult Index(int? thang)
        {
            var data = db.sp_baocao_thang(thang).ToList();
            return View(data);
        }
    }
}