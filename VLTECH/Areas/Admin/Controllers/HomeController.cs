using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLTECH.Models;
using PagedList;
using System.IO;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace VLTECH.Areas.Admin.Controllers
{
    public class HomeController : BaseController
        
    {
        Qlbanhang db = new Qlbanhang();
       
        // GET: Admin/Home
        public ActionResult Index(int ?page, string search)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.Sanphams.OrderBy(x => x.Masp);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(db.Sanphams.Where(x => x.Tensp.Contains(search) ||  search == null ).ToList().ToPagedList(pageNumber, pageSize));
        }

        // Xem chi tiết người dùng GET: Admin/Home/Details/5 
        public ActionResult Details(int id)
        {
            var dt = db.Sanphams.Find(id);
            return View(dt);
        }

        // Tạo sản phẩm mới phương thức GET: Admin/Home/Create
        [HttpGet]
        public ActionResult Create()
        {
            //Để tạo dropdownList bên view
            var hangselected = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang");
            ViewBag.Mahang = hangselected;
            var hdhselected = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh");
            ViewBag.Mahdh = hdhselected;
            return View(new Sanpham());
        }

        // Tạo sản phẩm mới phương thức POST: Admin/Home/Create
        [HttpPost]
        public ActionResult Create(Sanpham sanpham)
        {
            try
            {
                if (sanpham.ImageFile != null)
                {
                    var hangselected = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang");
                    ViewBag.Mahang = hangselected;
                    var hdhselected = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh");
                    ViewBag.Mahdh = hdhselected;
                    string fileName = Path.GetFileNameWithoutExtension(sanpham.ImageFile.FileName);
                    string extension = Path.GetExtension(sanpham.ImageFile.FileName);
                    fileName = fileName + extension;
                    sanpham.Anhbia = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    sanpham.ImageFile.SaveAs(fileName);
                }
                db.Sanphams.Add(sanpham);
                db.SaveChanges();
                return Redirect("Index");
            }
            catch
            {
                return View(new Sanpham());
            }
        }


        // Sửa sản phẩm GET lấy ra ID sản phẩm: Admin/Home/Edit/5
        public ActionResult Edit(int id)
        {
            // Hiển thị dropdownlist
            var dt = db.Sanphams.Find(id);
            var hangselected = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang",dt.Mahang);
            ViewBag.Mahang = hangselected;
            var hdhselected = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh",dt.Mahdh);
            ViewBag.Mahdh = hdhselected;
           // 
            return View(dt);
            
        }

        // POST: Admin/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Sanpham sanpham)
        {
            try
            {
                if (sanpham.ImageFile != null)
                {
                    var hangselected = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang");
                    ViewBag.Mahang = hangselected;
                    var hdhselected = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh");
                    ViewBag.Mahdh = hdhselected;
                    string fileName = Path.GetFileNameWithoutExtension(sanpham.ImageFile.FileName);
                    string extension = Path.GetExtension(sanpham.ImageFile.FileName);
                    fileName = fileName + extension;
                    sanpham.Anhbia = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    sanpham.ImageFile.SaveAs(fileName);
                }
                db.Entry(sanpham).State = EntityState.Modified;
                // Sửa sản phẩm theo mã sản phẩm
                //var oldItem = db.Sanphams.Find(sanpham.Masp);
                //oldItem.Tensp = sanpham.Tensp;
                //oldItem.Giatien = sanpham.Giatien;
                //oldItem.Soluong = sanpham.Soluong;
                //oldItem.Mota = sanpham.Mota;
                //oldItem.Anhbia = sanpham.Anhbia;
                //oldItem.Bonhotrong = sanpham.Bonhotrong;
                //oldItem.Ram = sanpham.Ram;
                //oldItem.Thesim = sanpham.Thesim;
                //oldItem.Mahang = sanpham.Mahang;
                //oldItem.Mahdh = sanpham.Mahdh;
                // lưu lại
                db.SaveChanges();
                // xong chuyển qua index
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Sanpham());
            }
        }

        
        // Xoá sản phẩm phương thức GET: Admin/Home/Delete/5
        public ActionResult Delete(int id)
        {
            var dt = db.Sanphams.Find(id);
            return View(dt);
        }

        // Xoá sản phẩm phương thức POST: Admin/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //Lấy được thông tin sản phẩm theo ID(mã sản phẩm)
                var dt = db.Sanphams.Find(id);
                // Xoá
                db.Sanphams.Remove(dt);
                // Lưu lại
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(new Sanpham());
            }
        }
    }
}
