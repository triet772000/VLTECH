using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using VLTECH.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace VLTECH.Areas.Admin.Controllers
{
    public class BaocaongaysController : BaseController
    {
        private Qlbanhang db = new Qlbanhang();

        public ActionResult Index(DateTime? Ngaybd, DateTime? Ngaykt)
        {
            var data = db.sp_baocao_ngay(Ngaybd, Ngaykt).ToList();
            return View(data);
        }

        //public void Exportexcel()
        //{
        //    var list = new List<VLTECH.Models.sp_baocao_ngay_Result>();
        //    for (int i = 0; i < 9; i++)
        //    {
        //        list.Add(new Models.sp_baocao_ngay_Result
        //        {
        //            Ngaydat = new DateTime(),
        //            Madon = 0,
        //            Tensp = string.Empty,
        //            Hoten = string.Empty,
        //            Dienthoai = string.Empty,
        //            Soluong = 0,
        //            Dongia = 0,
        //            Thanhtien = 0
        //        });

        //    };
        //    ExcelPackage ep = new ExcelPackage();
        //    ExcelWorksheet Sheet = ep.Workbook.Worksheets.Add("Report");
        //    Sheet.Cells["A1"].Value = "Ngày đặt hàng";
        //    Sheet.Cells["B1"].Value = "Mã đơn";
        //    Sheet.Cells["C1"].Value = "Tên sản phẩm";
        //    Sheet.Cells["D1"].Value = "Tên khách";
        //    Sheet.Cells["E1"].Value = "Số điện thoại";
        //    Sheet.Cells["F1"].Value = "Đơn giá";
        //    Sheet.Cells["G1"].Value = "Số lượng";
        //    Sheet.Cells["H1"].Value = "Thành tiền";

        //    int row = 2;// dòng bắt đầu ghi dữ liệu
        //    foreach (var item in list)
        //    {
        //        Sheet.Cells[string.Format("A{0}", row)].Value = item.Ngaydat;
        //        Sheet.Cells[string.Format("B{0}", row)].Value = item.Madon;
        //        Sheet.Cells[string.Format("C{0}", row)].Value = item.Tensp;
        //        Sheet.Cells[string.Format("D{0}", row)].Value = item.Hoten;
        //        Sheet.Cells[string.Format("E{0}", row)].Value = item.Dienthoai;
        //        Sheet.Cells[string.Format("E{0}", row)].Value = item.Dongia;
        //        Sheet.Cells[string.Format("E{0}", row)].Value = item.Soluong;
        //        Sheet.Cells[string.Format("E{0}", row)].Value = item.Thanhtien;
        //        row++;
        //    }
        //    Sheet.Cells["A:AZ"].AutoFitColumns();
        //    Response.Clear();
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment; filename=" + "Report.xlsx");
        //    Response.BinaryWrite(ep.GetAsByteArray());
        //    Response.End();

        //}


    }
}