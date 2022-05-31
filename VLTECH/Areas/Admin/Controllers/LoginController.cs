using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLTECH.Models;

namespace VLTECH.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        Qlbanhang db = new Qlbanhang();
        // GET: Admin/Login
        public ActionResult Dangnhap()
        {
            return View(new LoginModel());
        }


        [HttpPost]
        public ActionResult Dangnhap(FormCollection userlog)
        {
            string userMail = userlog["userMail"].ToString();
            string password = userlog["password"].ToString();
            var islogin = db.Nguoidungs.SingleOrDefault(x => x.Email.Equals(userMail) && x.Matkhau.Equals(password) && x.IDQuyen == 2);

            if (islogin != null)
            {
                Session["use"] = islogin;
                return RedirectToAction("Index", "Home");
            }
            else
            {   
                ViewBag.Fail = "Đăng nhập thất bại";
                return View(new LoginModel());
            }

            //if (islogin != null)
            //{
            //    if (userMail == "Admin@gmail.com")
            //    {
            //        Session["use"] = islogin;
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        ViewBag.Fail = "Đăng nhập thất bại";
            //        return View(new LoginModel());
            //    }
            //}
            //else
            //{
            //    ViewBag.Fail = "Đăng nhập thất bại";
            //    return View(new LoginModel());
            //}
        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Dangnhap", "Login");
        }
    }
}