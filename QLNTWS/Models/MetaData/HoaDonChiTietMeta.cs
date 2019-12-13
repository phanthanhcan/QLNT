using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLNTWS.Models
{
    [MetadataType(typeof(HoaDonChiTiet.HoaDonChiTietMetadata))]
    public partial class HoaDonChiTiet
    {
        class HoaDonChiTietMetadata
        {
            [Display(Name = "ID Hóa đơn")]
            public int HoaDonID { get; set; }

            [Display(Name = "ID Sản phẩm ")]
            public int SanPhamID { get; set; }

            [Display(Name = "Số lượng")]
            public int SoLuong { get; set; }

            [Display(Name = "Đơn giá")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            public int DonGia { get; set; }

            [Display(Name = "Thành tiền")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            public int ThanhTien { get; set; }
        }
    }
}