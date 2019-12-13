using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNTWS.Models
{
    public partial class SanPham
    {
        public string DuongDanHinh
        {
            get
            {
                return "";
            }
        }
        public List<string> Hinhs
        {
            get
            {
                var hinhs = new List<string>();
                if (!string.IsNullOrWhiteSpace(ListHinh))
                {
                    hinhs.AddRange(ListHinh.Split(','));
                }
                else
                {
                    hinhs.Add("tests");
                }
                return hinhs;
            }

        }
    }
}