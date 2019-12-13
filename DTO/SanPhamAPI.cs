using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class SanPhamAPIOutput
    {
        public int SanPhamID { get; set; }
        public string MaSo { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int GiaBan { get; set; }
        public int NhaCungCapID { get; set; }
        public string XuatXu { get; set; }
        public int LoaiID { get; set; }
        public string ChatLieu { get; set; }
        public string MauSac { get; set; }
        public string Hinh { get; set; }
        public bool HetHang { get; set; }
        public string BiDanh { get; set; }
        public int star { get; set; }
        public int GiamGia { get; set; }

        #region sub class Loại

        public Loai loaiSanPham { get; set; }

        public class Loai
        {
            public int LoaiID { get; set; }
            public string Ten { get; set; }
        }

        #endregion

        #region sub class nhà cung cấp

        public NhaCungCap nhaCungCap { get; set; }

        public class NhaCungCap
        {
            public int NhaCungCapID { get; set; }
            public string Ten { get; set; }
        }
        #endregion
    }

    public class listSanPhamAPIOutput
    {
        public int TotalCount { get; set; }
        public List<SanPhamAPIOutput> listSanPhamOutput { get; set; }
    }

    class SanPhamAPIInput
    {
    }
}
