using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLNTWS.Models
{
    [MetadataType(typeof(Loai.LoaiMetadata))]
    public partial class Loai
    {

        class LoaiMetadata
        {
            [Display(Name = "ID")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public int LoaiID { get; set; }

            [Display(Name = "Tên loại")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public string Ten { get; set; }

            [Display(Name = "Chủng loại ID")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public Nullable<int> ChungLoaiID { get; set; }

            [Display(Name = "Bí Danh")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public string BiDanh { get; set; }

            [Display(Name = "Giá từ")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public Nullable<decimal> PriceFrom { get; set; }
        }
    }
}