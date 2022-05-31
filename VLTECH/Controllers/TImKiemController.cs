using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLTECH.Models;

namespace VLTECH.Controllers
{
    public class TImKiemController : Controller
    {
        Qlbanhang db = new Qlbanhang();
        // GET: TImKiem
        public ActionResult Index(int? page, string search)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.Sanphams.OrderBy(x => x.Masp);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 8;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(db.Sanphams.Where(x => x.Tensp.Contains(search) || x.Hangsanxuat.Tenhang.Contains(search) || search == null).ToList().ToPagedList(pageNumber, pageSize));
        }
    }
}