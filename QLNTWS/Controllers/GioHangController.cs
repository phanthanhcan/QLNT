using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using QLNTWS.Models;

namespace QLNTWS.Controllers
{
    public class GioHangController : Controller
    {
        NoiThatDbContext db = new NoiThatDbContext();

        // GET: GioHang
        public ActionResult Index()
        {
            // Tham chiếu đến giỏ hàng lưu trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            if (gioHang == null || gioHang.TongSanPham == 0)
            {// Điều hướng về trang chủ
                return RedirectToAction("Index", "Home");
            }
            return View(gioHang);
        }
        [HttpPost]
        public JsonResult CreateGet(int SanPhamID, int SoLuong = 1)
        {
            // Tham chiếu đến giỏ hàng lưu trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            if (gioHang == null)
            {
                gioHang = new GioHangModel();
                Session["GioHang"] = gioHang;
            }
            SanPham sanPham = db.SanPhams.Find(SanPhamID);
            var item = new GioHangItem(sanPham, SoLuong);
            gioHang.Them(item);

            return  Json(gioHang.TongSanPham.ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int SanPhamID, int SoLuong = 1)
        {
            // Tham chiếu đến giỏ hàng lưu trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            if (gioHang == null)
            {
                gioHang = new GioHangModel();
                Session["GioHang"] = gioHang;
            }
            SanPham sanPham = db.SanPhams.Find(SanPhamID);
            var item = new GioHangItem(sanPham, SoLuong);
            gioHang.Them(item);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int SanPhamID, int SoLuong)
        {
            //Tham chiếu đến giỏ hàng trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            gioHang.HieuChinh(SanPhamID, SoLuong);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int SanPhamID)
        {
            //Tham chiếu đến giỏ hàng trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            gioHang.Xoa(SanPhamID);
            return RedirectToAction("Index");
        }

        #region Xử lý phát sinh đơn đặt hàng

        [HttpGet]
        public ActionResult DatHang()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DatHang(HoaDon hoaDon)
        {
            var gioHang = Session["GioHang"] as GioHangModel;
            if (gioHang == null || gioHang.TongSanPham == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            // Xử lý phát sinh HoaDon và HoaDonChiTiet
            try
            {
                //1. Thêm HoaDon
                hoaDon.NgayDatHang = DateTime.Now;
                hoaDon.TongTien = gioHang.TongTriGia;
                db.HoaDons.Add(hoaDon);
                //2. Thêm HoaDonChiTiet              
                foreach (var item in gioHang.DanhSach)
                {
                    HoaDonChiTiet ct = new HoaDonChiTiet();
                    ct.HoaDonID = hoaDon.ID;
                    ct.SanPhamID = item.SanPham.SanPhamID;
                    ct.SoLuong = item.SoLuong;
                    ct.DonGia = item.SanPham.GiaBan;
                    ct.ThanhTien = item.SanPham.GiaBan * item.SoLuong;
                    db.HoaDonChiTiets.Add(ct);
                }
                await db.SaveChangesAsync();
                gioHang.XoaTatCa();

                return View("DatHangThanhCong", hoaDon);
            }
            catch (Exception ex)
            {
                TempData["LoiDatHang"] = "Đặt hàng không thành công.<br>" + ex.Message;
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}