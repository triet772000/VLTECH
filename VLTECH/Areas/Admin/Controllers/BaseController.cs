using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLTECH.Models;

namespace VLTECH.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Qlbanhang db = new Qlbanhang();
            Nguoidung ndSession = (Nguoidung)Session["use"];
            if (Session["use"] == null || Session["use"].ToString() == "" || ndSession.IDQuyen != 2)
            {
                filterContext.Result = new RedirectToRouteResult(new
                System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Dangnhap" }));
            }
            base.OnActionExecuting(filterContext);

            //Qlbanhang db = new Qlbanhang();
            //Nguoidung ndSession = (Nguoidung)Session["use"];
            //var count = db.Nguoidungs.Count(m => m.MaNguoiDung == ndSession.MaNguoiDung & m.IDQuyen == 2);
            //if (count == 0)
            //{
            //    filterContext.Result = new RedirectToRouteResult(new
            //        System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Dangnhap" }));
            //}
            //base.OnActionExecuting(filterContext);
        }
    }
}