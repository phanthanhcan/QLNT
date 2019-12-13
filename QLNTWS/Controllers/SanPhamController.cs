using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using QLNTWS.Helper;
using DTO;
using System.Threading;
using System.Threading.Tasks;
using QLNTWS.Models;
using X.PagedList;

namespace QLNTWS.Controllers
{
    public class SanPhamController : Controller
    {
        NoiThatDbContext db = new NoiThatDbContext();
        // GET: HangHoa
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List(int? page = 1, int? idLoai = -1, int? idChungLoai = -1)
            {
                int pageIndex = (page < 1 ? 1 : page.Value);
                var pageSize = 6;
                int n = (pageIndex - 1) * pageSize;
                List<SanPham> items = null;
                Loai loaiItem = null;
                int totalCount = 0;
                if ( idLoai > -1)
                {
                    totalCount = await db.SanPhams.Where(p => p.LoaiID == idLoai).CountAsync();
                    items = await db.SanPhams.Include(p => p.Loai).Include(p => p.NhaCungCap).Where(p => p.LoaiID == idLoai).OrderByDescending(p => p.SanPhamID).Skip(n).Take(pageSize).ToListAsync();
                    loaiItem = await db.Loais.FindAsync(idLoai);
                    TempData["idLoai"] = idLoai;
                    TempData["idChungLoai"] = idChungLoai == -1 ? loaiItem.ChungLoaiID : idChungLoai;
                }
                if (idChungLoai > -1)
                {
                    totalCount =  await db.SanPhams.Include(p => p.Loai).Include(p => p.Loai.ChungLoai).Where(p => p.Loai.ChungLoaiID == idChungLoai).CountAsync();
                    items = await db.SanPhams.Include(p => p.Loai).Include(p => p.Loai.ChungLoai).Include(p => p.NhaCungCap).Where(p => p.Loai.ChungLoaiID == idChungLoai).OrderByDescending(p => p.SanPhamID).Skip(n).Take(pageSize).ToListAsync();
                    TempData["idChungLoai"] =  idChungLoai;
                }
                if (idChungLoai == -1 && idLoai == -1)
                {
                    totalCount = await db.SanPhams.CountAsync();
                    items = await db.SanPhams.Include(p => p.Loai).Include(p => p.NhaCungCap).OrderByDescending(p => p.SanPhamID).Skip(n).Take(pageSize).ToListAsync();

                }

                if (totalCount == 0)
                {
                    string Thongbao = "Không có dữ liệu sản phẩm";
                    return PartialView(viewName: "_BaoLoiPartial", model: Thongbao);
                }
                ViewBag.OnePageOfData = new StaticPagedList<SanPham>(items, pageIndex, pageSize, totalCount);

                //ViewBag.TieuDe = " Danh Danh Hang hoa mới";
                //truyen du lieu sang view
                //ViewBag.LoaiAct = "active";


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListPartial", model: items);
                }
                return View(model: items);
            }

        public async Task<ActionResult> DSSanPhamTheoChungLoai(int? ID)
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

        public async Task<ActionResult> DSSanPhamTheoLoai(int? ID)
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
                DS = await db.SanPhams.Where(o => o.Loai.LoaiID == ID).ToListAsync();
                if (DS == null)
                {
                    string str = "Không tìm thấy Sản phẩm";
                    return View("BaoLoi", model: str);
                }

                return View("DSSanPhamTheoChungLoai", DS);
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

        public async Task<ViewResult> SanPhamChiTiet(int? ID)
        {
            if (ID == null)
            {
                string str = "ID ko hợp lệ";
                return View("BaoLoi", model: str);
            }
            try
            {
                SanPham item = await db.SanPhams.Include(p => p.NhaCungCap).Where(o => o.SanPhamID == ID).FirstOrDefaultAsync();
                if (item == null) throw new Exception($"Mặt hàng ID={ID} không tồn tại.");

                return View(model: item);
            }
            catch (Exception Ex)
            {
                string str = "có lỗi xảy ra" + Ex.ToString();
                return View("BaoLoi", model: str);
            }
        }
        // GET: SanPham
        public async Task<ActionResult> DanhSachSanPhamGiamGia(int? page = 1)
        {
            try
            { 
                int pageIndex = (page < 1 ? 1 : page.Value);
                var pageSize = 8;
                int n = (pageIndex - 1) * pageSize;
                listSanPhamAPIOutput items = null;
                items = await ApiHelper<listSanPhamAPIOutput>.RunPostAsync($"san-pham/doc-danh-sach-san-pham-giam-gia?giamgia=1&page={pageIndex}&size={pageSize}");
                //items = await ApiHelper<List<SanPhamAPIOutput>>.RunPostAsync($"san-pham/doc-danh-sach-san-pham-giam-gia?giamgia=1&page={page}");
                if (items.TotalCount == 0)
                {
                    string Thongbao = "Không có dữ liệu sản phẩm";
                    return PartialView(viewName: "_BaoLoiPartial", model: Thongbao);
                }
                ViewBag.OnePageOfData = new StaticPagedList<SanPhamAPIOutput>(items.listSanPhamOutput, pageIndex, pageSize, items.TotalCount);


                //ViewBag.TieuDe = " Danh Danh Hang hoa mới";
                //truyen du lieu sang view
                //ViewBag.LoaiAct = "active";
                //TempData["LID"] = id;
                //TempData["CLID"] = idCL ?? LoaiItem.ChungLoaiID;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_DanhSachSanPhamGiamGia_ListPartial");
                }
                return View();
                //    //var chungloais = await ApiHelper<List<ChungLoaiOutput>>.RunPostAsync("chung-loai/doc-danh-sach-thong-ke");
                //    var sanPhams = await ApiHelper<List<SanPhamAPIOutput>>.RunPostAsync("san-pham/doc-danh-sach-san-pham-giam-gia?giamgia=1");
                //return View(sanPhams);
            }
            catch (Exception ex)
            {
                return View("Error", model: ex.Message);
            }
        }

        public async Task<ActionResult> DanhSachSanPhamMoiNhat()
        {
            try
            {

                List<SanPhamAPIOutput> items = null;
                items = await ApiHelper<List<SanPhamAPIOutput>>.RunPostAsync("san-pham/doc-danh-sach-san-pham-moi-nhat");
                if (items == null)
                {
                    string Thongbao = "Không có dữ liệu sản phẩm";
                    return PartialView(viewName: "_BaoLoiPartial", model: Thongbao);
                }

                //ViewBag.TieuDe = " Danh Danh Hang hoa mới";
                //truyen du lieu sang view
                //ViewBag.LoaiAct = "active";
                //TempData["LID"] = id;
                //TempData["CLID"] = idCL ?? LoaiItem.ChungLoaiID;

                return View(items);
                //    //var chungloais = await ApiHelper<List<ChungLoaiOutput>>.RunPostAsync("chung-loai/doc-danh-sach-thong-ke");
                //    var sanPhams = await ApiHelper<List<SanPhamAPIOutput>>.RunPostAsync("san-pham/doc-danh-sach-san-pham-giam-gia?giamgia=1");
                //return View(sanPhams);
            }
            catch (Exception ex)
            {
                return View("Error", model: ex.Message);
            }
        }
    }
}