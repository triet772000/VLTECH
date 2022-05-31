using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLTECH.Models;

namespace VLTECH.Controllers
{
    public class DonHangController : Controller
    {
        private Qlbanhang db = new Qlbanhang();

        // GET: Donhangs
        // Hiển thị danh sách đơn hàng
        public ActionResult Index(int? page)
        {
            //Kiểm tra đang đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.
            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            //// theo Masp mới có thể phân trang.
            Nguoidung kh = (Nguoidung)Session["use"];
            int maND = kh.MaNguoiDung;
            var sp = db.Donhangs.OrderBy(x => x.Madon).Include(d => d.Nguoidung).Where(d => d.MaNguoidung == maND);
            //var sp = db.Donhangs.OrderBy(x => x.Madon);
            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 15;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(sp.ToPagedList(pageNumber, pageSize));

            //Nguoidung kh = (Nguoidung)Session["use"];
            //int maND = kh.MaNguoiDung;
            //var donhangs = db.Donhangs.Include(d => d.Nguoidung).Where(d => d.MaNguoidung == maND);
            //return View(donhangs.ToList());
        }

        // GET: Donhangs/Details/5
        //Hiển thị chi tiết đơn hàng
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            var chitiet = db.Chitietdonhangs.Include(d => d.Sanpham).Where(d => d.Madon == id).ToList();
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
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