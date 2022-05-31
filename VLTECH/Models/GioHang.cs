using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLTECH.Models
{
    public class GioHang
    {
        List<GioHang> items = new List<GioHang>();
        //private int iMaSP;

        //public int IMaSP
        //{
        //    get { return iMaSP; }
        //    set { iMaSP = value; }
        //}
        Qlbanhang db = new Qlbanhang();
        public int iMasp { get; set; }
        public string sTensp { get; set; }
        public string sAnhbia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        //public int TongTien
        //{
        //    get { return (Convert.ToInt32(items.Sum(iSoLuong * dDonGia)); }
        //}
        //Hàm tạo cho giỏ hàng
        public GioHang(int Masp)
        {
            iMasp = Masp;
            Sanpham sp = db.Sanphams.Single(n => n.Masp == iMasp);
            sTensp =sp.Tensp;
            sAnhbia = sp.Anhbia;
            dDonGia = double.Parse(sp.Giatien.ToString());
            iSoLuong = 1;
        }
    }
}