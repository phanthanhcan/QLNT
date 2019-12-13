using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace QLNTWS.Models
{
    [MetadataType(typeof(ChungLoai.ChungLoaiMetadata))]
    public partial class ChungLoai
    {
        class ChungLoaiMetadata
        {
            [Display(Name = "ID")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public int ChungLoaiID { get; set; }

            [Display(Name = "Tên chủng loại")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public string Ten { get; set; }

            [Display(Name = "Bí danh")]
            [Required(ErrorMessage = "Không đươc để trống")]
            public string BiDanh { get; set; }

            public string Hinh { get; set; }
        }
    }
}

