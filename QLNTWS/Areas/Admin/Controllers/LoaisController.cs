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

namespace QLNTWS.Areas.Admin.Controllers
{
    public class LoaisController : Controller
    {
        private NoiThatDbContext db = new NoiThatDbContext();

        #region List
        // GET: Admin/Loais
        public async Task<ActionResult> Index(int? page = 1)
        {
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 6;
            int n = (pageIndex - 1) * pageSize;
            List<Loai> items = null;
            int totalCount = await db.Loais.CountAsync();

            if (totalCount == 0)
            {
                string Thongbao = "Không có dữ liệu loại sản phẩm";
                return PartialView(viewName: "_BaoLoiPartial", model: Thongbao);
            }
            items = await db.Loais.Include(p => p.ChungLoai).OrderByDescending(p => p.LoaiID).Skip(n).Take(pageSize).ToListAsync();

            ViewBag.OnePageOfData = new StaticPagedList<Loai>(items, pageIndex, pageSize, totalCount);

            //ViewBag.TieuDe = " Danh Danh Hang hoa mới";
            //truyen du lieu sang view
            //ViewBag.LoaiAct = "active";
            //TempData["LID"] = id;
            //TempData["CLID"] = idCL ?? LoaiItem.ChungLoaiID;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartial", model: items);
            }
            return View(model: items);
        }
        #endregion

        #region xử lý upload hình
        //GET : AdminHangHoa/Upload/3            
        public async Task<ActionResult> Upload(int? id)
        {
            if (id == null || id < 1) return RedirectToAction("index");
            try
            {
                Loai item = await db.Loais.FindAsync(id);
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
                Loai item = await db.Loais.FindAsync(id);
                if (taptins[0] != null)
                {
                    string duongDan = Server.MapPath("~/img/");
                    string fileName = $"{ DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") }-{ taptins[0].FileName}";
                    taptins[0].SaveAs(duongDan + fileName);
                    item.Hinh = fileName;
                    await db.SaveChangesAsync();
                    // Upload thành công
                    return RedirectToAction("index");
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

        #region Chi tiết
        // GET: Admin/Loais/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai loai = await db.Loais.FindAsync(id);
            if (loai == null)
            {
                return HttpNotFound();
            }
            return View(loai);
        }
        #endregion

        #region Thêm mới
        // GET: Admin/Loais/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                ViewBag.ChungLoaiID = await TaoDanhSachChungLoai();
                return View();
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Không truy cập được dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        // POST: Admin/Loais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Loai loai)
        {
            try
            {
                int d = await db.Loais.CountAsync(p => p.Ten == loai.Ten);
                if (d > 0) ModelState.AddModelError("Ten", $"Tên loại={loai.Ten} đã tồn tại.");
                if (ModelState.IsValid)
                {
                    db.Loais.Add(loai);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.ChungLoaiID = await TaoDanhSachChungLoai();
                return View(loai);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Không ghi được dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        #endregion

        #region cập nhật
        // GET: Admin/Loais/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai loai = await db.Loais.FindAsync(id);
            if (loai == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChungLoaiID = await TaoDanhSachChungLoai(loai.ChungLoaiID);
            return View(loai);
        }

        // POST: Admin/Loais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Loai loai)
        {
            try
            {
                int d = await db.Loais.CountAsync(p => p.LoaiID != loai.LoaiID && p.Ten == loai.Ten);
                if (d > 0) ModelState.AddModelError("MaSo", "Tên không được trùng.");
                if (ModelState.IsValid)
                {// Trường hợp dữ liệu nhập hợp lệ (ko vi phạm các kiễm tra cài trong model)
                    var loaiHC = await db.Loais.FindAsync(loai.LoaiID);
                    TryUpdateModel(loaiHC, new string[] {
                        "LoaiID", "Ten", "PriceFrom", "ChungLoaiID", "BiDanh"
                    });
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                // Trường hợp dữ liệu nhập không hợp lệ 
                ViewBag.ChungLoaiID = await TaoDanhSachChungLoai(loai.ChungLoaiID);
                return View(loai);
            }
            catch (Exception ex)
            {
                object cauBaoLoi = $"Lỗi ghi dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", cauBaoLoi);
            }
        }

        #endregion

        #region xóa
        // GET: Admin/Loais/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                Loai loai = await db.Loais.FindAsync(id);
                db.Loais.Remove(loai);
                await db.SaveChangesAsync();
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Xóa không thành công.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        // POST: Admin/Loais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Loai loai = await db.Loais.FindAsync(id);
            db.Loais.Remove(loai);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region private methods
        private async Task<SelectList> TaoDanhSachChungLoai(int? IDChon = 0)
        {
            var items = await db.ChungLoais
                                     .Select(p => new {
                                         p.ChungLoaiID,
                                         ThongTin = p.ChungLoaiID + " - " + p.Ten
                                     })
                                     .ToListAsync();
            items.Insert(0, new { ChungLoaiID = -1, ThongTin = "------ Chọn loại------ " });
            var result = new SelectList(items, "ChungLoaiID", "ThongTin", selectedValue: IDChon);
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
