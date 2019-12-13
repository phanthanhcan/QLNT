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
    public class NhaCungCapsController : Controller
    {
        private NoiThatDbContext db = new NoiThatDbContext();

        #region  list
        // GET: Admin/NhaCungCaps
        public async Task<ActionResult> Index(int? page = 1)
        {
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 6;
            int n = (pageIndex - 1) * pageSize;
            List<NhaCungCap> items = null;
            int totalCount = await db.NhaCungCaps.CountAsync();

            if (totalCount == 0)
            {
                string Thongbao = "Không có dữ liệu Nhà cung cấp";
                return PartialView(viewName: "_BaoLoiPartial", model: Thongbao);
            }
            items = await db.NhaCungCaps.OrderByDescending(p => p.NhaCungCapID).Skip(n).Take(pageSize).ToListAsync();

            ViewBag.OnePageOfData = new StaticPagedList<NhaCungCap>(items, pageIndex, pageSize, totalCount);

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

        #region Chi tiết
        // GET: Admin/NhaCungCaps/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = await db.NhaCungCaps.FindAsync(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        #endregion

        #region Thêm mới

        // GET: Admin/NhaCungCaps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaCungCaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create ( NhaCungCap nhaCungCap)
        {
            try
            {
                int d = await db.NhaCungCaps.CountAsync(p => p.Ten == nhaCungCap.Ten);
                if (d > 0) ModelState.AddModelError("Ten", $"Tên Chủng loại = {nhaCungCap.Ten} đã tồn tại.");
                if (ModelState.IsValid)
                {
                    db.NhaCungCaps.Add(nhaCungCap);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(nhaCungCap);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Không ghi được dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }
        #endregion

        #region Cập nhật
        // GET: Admin/NhaCungCaps/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhaCungCap = await db.NhaCungCaps.FindAsync(id);
            if (nhaCungCap == null)
            {
                return HttpNotFound();
            }
            return View(nhaCungCap);
        }

        // POST: Admin/NhaCungCaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( NhaCungCap nhaCungCap)
        {
            try
            {
                int d = await db.NhaCungCaps.CountAsync(p => p.NhaCungCapID != nhaCungCap.NhaCungCapID && p.Ten == nhaCungCap.Ten);
                if (d > 0) ModelState.AddModelError("Ten", "Tên nhà cung cấp không được trùng.");
                if (ModelState.IsValid)
                {// Trường hợp dữ liệu nhập hợp lệ (ko vi phạm các kiễm tra cài trong model)
                    var NhaCungCapHC = await db.NhaCungCaps.FindAsync(nhaCungCap.NhaCungCapID);
                    TryUpdateModel(NhaCungCapHC, new string[] {
                        "NhaCungCapID", "Ten", "GioiThieu"
                    });
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                // Trường hợp dữ liệu nhập không hợp lệ 
                return View(nhaCungCap);
            }
            catch (Exception ex)
            {
                object cauBaoLoi = $"Lỗi ghi dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", cauBaoLoi);
            }
        }

        #endregion

        #region xóa
        // GET: Admin/NhaCungCaps/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                NhaCungCap nhaCungCap = await db.NhaCungCaps.FindAsync(id);
                db.NhaCungCaps.Remove(nhaCungCap);
                await db.SaveChangesAsync();
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Xóa không thành công.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        // POST: Admin/NhaCungCaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NhaCungCap nhaCungCap = await db.NhaCungCaps.FindAsync(id);
            db.NhaCungCaps.Remove(nhaCungCap);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
