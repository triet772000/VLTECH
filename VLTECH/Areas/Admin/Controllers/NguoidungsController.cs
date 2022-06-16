using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLTECH.Models;
using PagedList;
namespace VLTECH.Areas.Admin.Controllers
{
    public class NguoidungsController : BaseController
    {
        private Qlbanhang db = new Qlbanhang();

        // Xem quản lý tất cả người dùng
        // GET: Admin/Nguoidungs
        public ActionResult Index(int? page, string search)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.Nguoidungs.OrderBy(x => x.MaNguoiDung);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(db.Nguoidungs.Where(x => x.Email.ToString().Contains(search) || x.Dienthoai.ToString().Contains(search) || search == null).ToList().ToPagedList(pageNumber, pageSize));
        }

        //Xem chi tiết người dùng theo Mã người dùng
        // GET: Admin/Nguoidungs/Details/5
        public ActionResult Details(int? id)
        {
            // Nếu không có người dùng có mã được truyền vào thì trả về trang báo lỗi
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Khai báo một người dùng theo mã
            Nguoidung nguoidung = db.Nguoidungs.Find(id);
            if (nguoidung == null)
            {
                return HttpNotFound();
            }
            // trả về trang chi tiết người dùng
            return View(nguoidung);
        }

        //// GET: Admin/Nguoidungs/Create
        //public ActionResult Create()
        //{
        //    ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen");
        //    return View();
        //}

        //// POST: Admin/Nguoidungs/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaNguoiDung,Hoten,Email,Dienthoai,Matkhau,IDQuyen")] Nguoidung nguoidung)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Nguoidungs.Add(nguoidung);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", nguoidung.IDQuyen);
        //    return View(nguoidung);
        //}


            // Chỉnh sửa người dùng
        // GET: Admin/Nguoidungs/Edit/5
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
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", nguoidung.IDQuyen);
            return View(nguoidung);
        }

        // POST: Admin/Nguoidungs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNguoiDung,Hoten,Email,Dienthoai,Matkhau,IDQuyen,Diachi")] Nguoidung nguoidung)
        {
            if (ModelState.IsValid)
            {
                // Sửa sản phẩm theo mã sản phẩm
                var oldUser = db.Nguoidungs.Find(nguoidung.MaNguoiDung);
                oldUser.Hoten = nguoidung.Hoten;
                oldUser.Email = nguoidung.Email;
                oldUser.Dienthoai = nguoidung.Dienthoai;
                oldUser.Diachi = nguoidung.Diachi;
                oldUser.IDQuyen = nguoidung.IDQuyen;
                //db.Entry(nguoidung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", nguoidung.IDQuyen);
            return View(nguoidung);
        }

        // Xoá người dùng 
        // GET: Admin/Nguoidungs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Nguoidungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nguoidung nguoidung = db.Nguoidungs.Find(id);
            db.Nguoidungs.Remove(nguoidung);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
