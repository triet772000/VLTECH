using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLTECH.Models;
namespace VLTECH.Controllers
{
    public class UserController : Controller
    {
        Qlbanhang db = new Qlbanhang();
        // ĐĂNG KÝ
        public ActionResult Dangky()
        {
            return View(new Nguoidung());
        }

        // ĐĂNG KÝ PHƯƠNG THỨC POST
        [HttpPost]
        public ActionResult Dangky(Nguoidung nguoidung)
        {
            try
            {
                // Thêm người dùng  mới
                db.Nguoidungs.Add(nguoidung);
                // Lưu lại vào cơ sở dữ liệu
                db.SaveChanges();
                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                    {
                        return RedirectToAction("Dangnhap");
                    }
                return View("Dangky");
                
            }
            catch
            {
                return View(new Nguoidung());
            }
        }
   
        public ActionResult Dangnhap()
        {
            return View(new LoginModel());

        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection userlog)
        {
            string userMail = userlog["userMail"].ToString();
            string password = userlog["password"].ToString();
            var islogin = db.Nguoidungs.SingleOrDefault(x => x.Email.Equals(userMail) && x.Matkhau.Equals(password));

            if (islogin != null)
                {
                    if (userMail == "Admin@gmail.com")
                        {
                           Session["use"] = islogin;
                           return RedirectToAction("Index", "Admin/Home");
                        }
                    else
                        {
                            Session["use"] = islogin;
                            return RedirectToAction("Index","Home");
                        }
                 }
            else
                {
                    ViewBag.Fail = "Đăng nhập thất bại";
                    return View(new LoginModel());
                }

        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Dangnhap","User");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nguoidung nguoidung = db.Nguoidungs.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            return View(nguoidung);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNguoiDung,Hoten,Email,Dienthoai,Matkhau,IDQuyen,Diachi")] Nguoidung nguoidung)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(nguoidung).State = EntityState.Modified;
                var oldUser = db.Nguoidungs.Find(nguoidung.MaNguoiDung);
                oldUser.Hoten = nguoidung.Hoten;
                oldUser.Matkhau = nguoidung.Matkhau;
                oldUser.Email = nguoidung.Email;
                oldUser.Dienthoai = nguoidung.Dienthoai;
                oldUser.Diachi = nguoidung.Diachi;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(nguoidung);
        }
    }
}