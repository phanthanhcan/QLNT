using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLNTWS.Models
{
    [MetadataType(typeof(HoaDon.HoaDonMetadata))]
    public partial class HoaDon
    {
        class HoaDonMetadata
        {
            [Display(Name = "ID")]
            public int ID { get; set; }

            [Display(Name = "Ngày đạt hàng")]
            [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
            public System.DateTime NgayDatHang { get; set; }

            [Display(Name = "Họ tên")]
            public string HoTenKhach { get; set; }

            [Display(Name = "Dịa chỉ")]
            public string DiaChi { get; set; }

            [Display(Name = "Diện thoại")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public string DienThoai { get; set; }

            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Tổng tiền")]
            [Required(ErrorMessage = "Không đươc để trống")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            public int TongTien { get; set; }
        }
    }
}

//using System.ComponentModel.DataAnnotations;
//[MetadataType(typeof(SanPham.SanPhamMetadata))]
//public partial class SanPham
//{
//    class SanPhamMetadata
//    {
//    }
//}