using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using QLNTWS.Models;
using QLNTWS.Helper;
using DTO;

namespace QLNTWS.Controllers
{
    public class HangHoaController : Controller
    {
        NoiThatDbContext db = new NoiThatDbContext();
        // GET: HangHoa
        public ActionResult Index()
        {
            return View();
        }
        public  async  Task< ActionResult> DSHangHoaTheoChungLoai(int? ID)
        {
            if (ID == null)
            {
                string str = "ID Chủng loại hợp lệ";
                return View("BaoLoi", model: str);
            }
            ChungLoai item = await db.ChungLoais.FindAsync(ID);
            if (item == null)
            {
                string str = "Không tồn tại mã chủng loại sản phẩm";
                return View("BaoLoi", model: str);
            }
            List<SanPham> DS = null;
            try
            {
                DS = await db.SanPhams.Where(o => o.Loai.ChungLoaiID == ID).ToListAsync();
                if (DS == null)
                {
                    string str = "Không tìm thấy Sản phẩm";
                    return View("BaoLoi", model: str);
                }

                return View("DSHangHoaTheoChungLoai", DS);
            }
            catch (Exception Ex)
            {
                string str = "có lỗi xảy ra" + Ex.ToString();
                return View("BaoLoi", model: str);
            }
        }

        public async Task< ActionResult> DSHangHoaTheoLoai(int? ID)
        {
            if (ID == null)
            {
                string str = "ID ko tồn tại";
                return View("BaoLoi", model: str);
            }
            Loai item = await db.Loais.FindAsync(ID);
            if (item == null)
            {
                string str = "Không tồn tại mã loại sản phẩm";
                return View("BaoLoi", model: str);
            }
            List<SanPham> DS = null;
            try
            {
                DS = await db.SanPhams.Where(o => o.Loai.LoaiID== ID).ToListAsync();
                if (DS == null)
                {
                    string str = "Không tìm thấy Sản phẩm";
                    return View("BaoLoi", model: str);
                }

                return View("DSHangHoaTheoChungLoai", DS);
            }
            catch (Exception Ex)
            {
                string str = "có lỗi xảy ra" + Ex.ToString();
                return View("BaoLoi", model: str);
            }
        }

        public async Task<ActionResult> DSSanPhamGiamGia(int? ID)
        {
            try
            {
                //var chungloais = await ApiHelper<List<ChungLoaiOutput>>.RunPostAsync("chung-loai/doc-danh-sach-thong-ke");
                var chungloais = await ApiHelper<List<SanPhamAPIOutput>>.RunPostAsync("san-pham/doc-danh-sach-san-pham-giam-gia");
                return View(chungloais);
            }
            catch (Exception ex)
            {
                return View("Error", model: ex.Message);
            }
        }

        public async Task<ViewResult> HangHoaChiTiet(int? ID)
        {
                if (ID == null)
                {
                    string str = "ID ko hợp lệ";
                    return View("BaoLoi", model: str);
                }
                try
                {
                    SanPham item = await db.SanPhams.Include(p => p.NhaCungCap).Where( o => o.SanPhamID == ID).FirstOrDefaultAsync();
                    if (item == null) throw new Exception($"Mặt hàng ID={ID} không tồn tại.");

                return View(model: item);
                }
                catch (Exception Ex)
                {
                    string str = "có lỗi xảy ra" + Ex.ToString();
                    return View("BaoLoi", model: str);
                }
        }
    }
}