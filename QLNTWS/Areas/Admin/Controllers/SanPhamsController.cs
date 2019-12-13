using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLNTWS.Models;
using X.PagedList;
using X.PagedList.Mvc;
using X.PagedList.Mvc.Common;

namespace QLNTWS.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private NoiThatDbContext db = new NoiThatDbContext();

        #region danh sách
        // GET: Admin/SanPhams
        public async Task<ActionResult> Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.Loai).Include(s => s.NhaCungCap);
            return View(await sanPhams.ToListAsync());
        }

        public async Task< ActionResult> List(int? page = 1)
        {
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 6;
            int n = (pageIndex - 1) * pageSize;
            List<SanPham> items = null;
            int totalCount = await db.SanPhams.CountAsync();

            if (totalCount == 0)
            {
                string Thongbao = "Không có dữ liệu sản phẩm";
                return PartialView(viewName: "_BaoLoiPartial", model: Thongbao);
            }
            items = await db.SanPhams.Include(p => p.Loai).Include(p => p.NhaCungCap).OrderByDescending(p => p.SanPhamID).Skip(n).Take(pageSize).ToListAsync();

            ViewBag.OnePageOfData = new StaticPagedList<SanPham>(items, pageIndex, pageSize, totalCount);

            //ViewBag.TieuDe = " Danh Danh Hang hoa mới";
            //truyen du lieu sang view
            //ViewBag.LoaiAct = "active";
            //TempData["LID"] = id;
            //TempData["CLID"] = idCL ?? LoaiItem.ChungLoaiID;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", model : items);
            }
            return View( model : items);
        }
        #endregion

        #region xử lý upload hình
        //GET : AdminHangHoa/Upload/3            
        public async Task<ActionResult> Upload(int? id)
        {
            if (id == null || id < 1) return RedirectToAction("List");
            try
            {
                SanPham item = await db.SanPhams.FindAsync(id);
                if (item == null) throw new Exception("ID:" + id + "không tồn tại");
                return View(item);
            }
            catch (Exception ex)
            {
                object cauBaoLoi = "không truy cập được dữ liệu <br/>" + ex.Message;
                return View("BaoLoi", cauBaoLoi);
            }
        }
        //POST :AdminHangHoa/Upload/3

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(int id, HttpPostedFileBase[] taptins)
        {
            try
            {
                SanPham item = await db.SanPhams.FindAsync(id);
                if (taptins[0] != null)
                {
                    string duongDan = Server.MapPath("~/img/");
                    string dsTen = null;
                    foreach (var f in taptins)
                    {
                        string fileName = $"{ DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") }-{ f.FileName}";
                        f.SaveAs(duongDan + fileName);
                        dsTen += $"{fileName},";
                    }
                    //string dsTen = string.Join(",", taptins.Select(p => p.FileName).ToList());
                    item.ListHinh = dsTen.TrimEnd(',');
                    await db.SaveChangesAsync();
                    // Upload thành công
                    return RedirectToAction("List");
                }
                // Trường hợp chưa chọn file hoặc file không có nội dung thì quay trở lại view upload
                ViewBag.ThongBao = "Bạn chưa chọn file hoặc file bạn chọn không có nội dung.";
                return View(item);
            }
            catch (Exception ex)
            {
                object cauBaoLoi = "Upload không thành công <br/>" + ex.Message;
                return View("BaoLoi", cauBaoLoi);
            }

        }

        #endregion

        #region Chi tiết sản phẩm
        // GET: Admin/SanPhams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        #endregion
       
        #region Thêm mới
        // GET: Admin/SanPhams/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                ViewBag.LoaiID = await TaoDanhSachLoai();
                ViewBag.NhaCungCapID = await TaoDanhSachNhaCungCap();
                return View();
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Không truy cập được dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( SanPham sanPham)
        {
            try
            {
                int d = await db.SanPhams.CountAsync(p => p.MaSo == sanPham.MaSo);
                if (d > 0) ModelState.AddModelError("MaSo", $"Mã số={sanPham.MaSo} đã tồn tại.");
                if (ModelState.IsValid)
                {
                    db.SanPhams.Add(sanPham);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.LoaiID = await TaoDanhSachLoai();
                ViewBag.NhaCungCapID = await TaoDanhSachNhaCungCap();
                return View(sanPham);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Không ghi được dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        #endregion

        #region cập nhật
        // GET: Admin/SanPhams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiID = await TaoDanhSachLoai(sanPham.LoaiID);
            ViewBag.NhaCungCapID = await TaoDanhSachNhaCungCap(sanPham.NhaCungCapID);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SanPham sanPham)
        {
            try
            {
                int d = await db.SanPhams.CountAsync(p => p.SanPhamID != sanPham.SanPhamID && p.MaSo == sanPham.MaSo);
                if (d > 0) ModelState.AddModelError("MaSo", "Mã số bị trùng.");
                if (ModelState.IsValid)
                {// Trường hợp dữ liệu nhập hợp lệ (ko vi phạm các kiễm tra cài trong model)
                    var hangHoaHC = await db.SanPhams.FindAsync(sanPham.SanPhamID);
                    TryUpdateModel(hangHoaHC, new string[] {
                        "SanPhamID","MaSo","Ten","MoTa","GiaBan"," NhaCungCapID","XuatXu","LoaiID","ChatLieu","MauSac","HetHang","BiDanh","star"
                    });
                    await db.SaveChangesAsync();
                    return RedirectToAction("List");
                }
                // Trường hợp dữ liệu nhập không hợp lệ 
                ViewBag.LoaiID = await TaoDanhSachLoai(sanPham.LoaiID);
                ViewBag.NhaCungCapID = await TaoDanhSachNhaCungCap(sanPham.NhaCungCapID);
                return View(sanPham);
            }
            catch (Exception ex)
            {
                object cauBaoLoi = $"Lỗi ghi dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", cauBaoLoi);//pt6
            }
        }

        #endregion
       
        #region Xóa
        // GET: Admin/SanPhams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                SanPham sanPham = await db.SanPhams.FindAsync(id);
                db.SanPhams.Remove(sanPham);
                await db.SaveChangesAsync();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Xóa không thành công.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            db.SanPhams.Remove(sanPham);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region private methods
        private async Task<SelectList> TaoDanhSachLoai(int? IDChon = 0)
        {
            var items = await db.Loais
                                     .Select(p => new {
                                         p.LoaiID,
                                         ThongTin = p.LoaiID + " - " + p.Ten
                                     })
                                     .ToListAsync();
            items.Insert(0, new { LoaiID = -1, ThongTin = "------ Chọn loại------ " });
            var result = new SelectList(items, "LoaiID", "ThongTin", selectedValue: IDChon);
            return result;
        }

        private async Task<SelectList> TaoDanhSachNhaCungCap(int? IDChon = 0)
        {
            var items = await db.NhaCungCaps
                                     .Select(p => new {
                                         p.NhaCungCapID,
                                         ThongTin = p.NhaCungCapID + " - " + p.Ten
                                     })
                                     .ToListAsync();
            items.Insert(0, new { NhaCungCapID = -1, ThongTin = "------ Chọn loại------ " });
            var result = new SelectList(items, "NhaCungCapID", "ThongTin", selectedValue: IDChon);
            return result;
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
