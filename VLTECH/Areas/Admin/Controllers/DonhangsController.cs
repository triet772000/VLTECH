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
    public class DonhangsController : BaseController
    {
        private Qlbanhang db = new Qlbanhang();

        // GET: Admin/Donhangs
        public ActionResult Index(int? page, string search)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.Donhangs.OrderBy(x => x.Madon);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(db.Donhangs.Where(x => x.Madon.ToString().Contains(search) || x.Nguoidung.Dienthoai.ToString().Contains(search) || search == null).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Donhangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Hoten", donhang.MaNguoidung);
            ViewBag.Matt = new SelectList(db.Tinhtrangs, "Matt", "Tinhtrangdonhang", donhang.Matt);
            return View(donhang);
        }

        // POST: Admin/Donhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Madon,Ngaydat,MaNguoidung,Matt")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
                var oldItem = db.Donhangs.Find(donhang.Madon);
                oldItem.Matt = donhang.Matt;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoidung = new SelectList(db.Nguoidungs, "MaNguoiDung", "Hoten", donhang.MaNguoidung);
            ViewBag.Matt = new SelectList(db.Tinhtrangs, "Matt", "Tinhtrangdonhang", donhang.Matt);
            return View(donhang);
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
