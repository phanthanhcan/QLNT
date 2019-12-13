using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNTWS.Models
{
    public class GioHangItem
    {
        // Properties
        public SanPham SanPham { get; set; }
        public int SoLuong { get; set; }

        // Constructors
        public GioHangItem() { }

        public GioHangItem(SanPham sanPham, int soLuong)
        {
            this.SanPham = sanPham;
            this.SoLuong = soLuong;
        }
    }
    public class GioHangModel
    {
        // Field
        private List<GioHangItem> _DanhSach = new List<GioHangItem>();
        // Read Only Property
        public List<GioHangItem> DanhSach => _DanhSach;

        // Methods
        public void Them(GioHangItem item)
        {
            var gioHangItem = _DanhSach.Find(p => p.SanPham.SanPhamID == item.SanPham.SanPhamID);
            if (gioHangItem == null)
                _DanhSach.Add(item);
            else
                gioHangItem.SoLuong += item.SoLuong;
        }
        public void HieuChinh(int id, int SoLuong)
        {
            var itemHieuChinh = _DanhSach.Find(p => p.SanPham.SanPhamID == id);
            itemHieuChinh.SoLuong = SoLuong;
        }
        public void Xoa(int id)
        {
            var itemXoa = _DanhSach.Find(p => p.SanPham.SanPhamID == id);
            _DanhSach.Remove(itemXoa);
        }
        public void XoaTatCa()
        {
            _DanhSach.Clear();
        }
        // Read Only Properties
        public int TongSanPham
        {
            get { return _DanhSach.Count; }
        }
        public int TongSoLuong
        {
            get { return _DanhSach.Sum(p => p.SoLuong); }
        }
        public int TongTriGia
        {
            get
            {
                int kq = 0;
                kq = _DanhSach.Sum(p => p.SoLuong * p.SanPham.GiaBan);
                return kq;
            }
        }
    }
}