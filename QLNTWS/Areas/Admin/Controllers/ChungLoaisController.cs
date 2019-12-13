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
    public class ChungLoaisController : Controller
    {
        private NoiThatDbContext db = new NoiThatDbContext();

        #region list
        // GET: Admin/ChungLoais
        public async Task<ActionResult> Index( int? page = 1 )
        {
            int pageIndex = (page < 1 ? 1 : page.Value);
            var pageSize = 6;
            int n = (pageIndex - 1) * pageSize;
            List<ChungLoai> items = null;
            int totalCount = await db.ChungLoais.CountAsync();

            if (totalCount == 0)
            {
                string Thongbao = "Không có dữ liệu Chủng loại sản phẩm";
                return PartialView(viewName: "_BaoLoiPartial", model: Thongbao);
            }
            items = await db.ChungLoais.OrderByDescending(p => p.ChungLoaiID).Skip(n).Take(pageSize).ToListAsync();

            ViewBag.OnePageOfData = new StaticPagedList<ChungLoai>(items, pageIndex, pageSize, totalCount);

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
        // GET: Admin/ChungLoais/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChungLoai chungLoai = await db.ChungLoais.FindAsync(id);
            if (chungLoai == null)
            {
                return HttpNotFound();
            }
            return View(chungLoai);
        }

        #endregion

        #region thêm mới

        // GET: Admin/ChungLoais/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Không truy cập được dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        // POST: Admin/ChungLoais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( ChungLoai chungLoai)
        {
            try
            {
                int d = await db.ChungLoais.CountAsync(p => p.Ten == chungLoai.Ten);
                if (d > 0) ModelState.AddModelError("Ten", $"Tên Chủng loại = {chungLoai.Ten} đã tồn tại.");
                if (ModelState.IsValid)
                {
                    db.ChungLoais.Add(chungLoai);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(chungLoai);
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Không ghi được dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        #endregion

        #region Cập nhật

        // GET: Admin/ChungLoais/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChungLoai chungLoai = await db.ChungLoais.FindAsync(id);
            if (chungLoai == null)
            {
                return HttpNotFound();
            }
            return View(chungLoai);
        }

        // POST: Admin/ChungLoais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChungLoai chungLoai)
        {
            try
            {
                int d = await db.ChungLoais.CountAsync(p => p.ChungLoaiID != chungLoai.ChungLoaiID && p.Ten == chungLoai.Ten);
                if (d > 0) ModelState.AddModelError("Ten", "Tên Chủng  loại không được trùng.");
                if (ModelState.IsValid)
                {// Trường hợp dữ liệu nhập hợp lệ (ko vi phạm các kiễm tra cài trong model)
                    var chungLoaiHC = await db.ChungLoais.FindAsync(chungLoai.ChungLoaiID);
                    TryUpdateModel(chungLoaiHC, new string[] {
                        "ChungLoaiID", "Ten", "BiDanh"
                    });
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                // Trường hợp dữ liệu nhập không hợp lệ 
                return View(chungLoai);
            }
            catch (Exception ex)
            {
                object cauBaoLoi = $"Lỗi ghi dữ liệu.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", cauBaoLoi);
            }
        }

        #endregion

        #region xóa
        // GET: Admin/ChungLoais/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                ChungLoai chungLoai = await db.ChungLoais.FindAsync(id);
                db.ChungLoais.Remove(chungLoai);
                await db.SaveChangesAsync();
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                string cauBaoLoi = $"Xóa không thành công.<br/>Lý do:{ex.Message}";
                return View("BaoLoi", model: cauBaoLoi);
            }
        }

        // POST: Admin/ChungLoais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChungLoai chungLoai = await db.ChungLoais.FindAsync(id);
            db.ChungLoais.Remove(chungLoai);
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
