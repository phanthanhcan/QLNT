using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using QLNTWS.Models;
using QLNTWS.Helper;
using DTO;
using System.Threading;
using System.Threading.Tasks;

namespace QLNTWS.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public async Task< ActionResult>  Index()
        {
            try
            {
                //var chungloais = await ApiHelper<List<ChungLoaiOutput>>.RunPostAsync("chung-loai/doc-danh-sach-thong-ke");
                var sanPhams = await ApiHelper<List<SanPhamAPIOutput>>.RunPostAsync("san-pham/doc-danh-sach-san-pham-giam-gia?giamgia=0");
                return View(sanPhams);
            }
            catch (Exception ex)
            {
                return View("Error", model: ex.Message);
            }
        }
    }
}