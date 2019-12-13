using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLNTWS.Models
{
    [MetadataType(typeof(SanPham.SanPhamMetadata))]
    public partial class SanPham
    {
        class SanPhamMetadata
        {
            [Display(Name = "ID sản phẩm")]
            public int SanPhamID { get; set; }

            [Display(Name = "Mã số")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public string MaSo { get; set; }

            [Display(Name = "Tên sản phẩm")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public string Ten { get; set; }

            [Display(Name = "Mô tả")]
            public string MoTa { get; set; }

            [Display(Name = "Giá bán")]
            [DisplayFormat(DataFormatString = "{0:#,##0VND}")]
            [Required(ErrorMessage = "Không đươc để trống")]
            [Range(0, int.MaxValue, ErrorMessage = "{0} phải từ {1} đến {2}")]
            [RegularExpression(@"\d*", ErrorMessage = "{0} phải nhập sốs")]
            public int GiaBan { get; set; }

            [Display(Name = "Nhà cung cấp")]
            [Required(ErrorMessage ="Không đươc để trống")]
            public Nullable<int> NhaCungCapID { get; set; }

            [Display(Name = "Xuất xứ")]
            public string XuatXu { get; set; }

            [Display(Name = "ID loại")]
            public Nullable<int> LoaiID { get; set; }

            [Display(Name = "Chất liệu")]
            public string ChatLieu { get; set; }

            [Display(Name = "Màu sắc")]
            public string MauSac { get; set; }
            public string Hinh { get; set; }
            [Display(Name = "Hết hàng")]
            public bool HetHang { get; set; }

            [Display(Name = "Bí danh")]
            public string BiDanh { get; set; }

            [Display(Name = "Bình chọn")]
            [Range(0, 5, ErrorMessage = "{0} phải từ {1} đến {2}")]
            [RegularExpression(@"\d*", ErrorMessage = "{0} phải nhập sốs")]
            public Nullable<int> star { get; set; }
            public string ListHinh { get; set; }


            [Display(Name = "Giảm giá")]
            [Range(0, 100, ErrorMessage = "{0} phải từ 1 đến 100")]
            [RegularExpression(@"\d*", ErrorMessage = "{0} phải nhập số")]
            public Nullable<int> GiamGia { get; set; }
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