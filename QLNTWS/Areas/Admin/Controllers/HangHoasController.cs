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

namespace QLNTWS.Areas.Admin.Controllers
{
    public class HangHoastestController : Controller
    {
        //private QLNoiThatDbContext db = new QLNoiThatDbContext();

        //// GET: Admin/HangHoas
        //#region Danh sách chi tiết
        //public async Task<ActionResult> Index()
        //{
        //    var hangHoas = db.HangHoas.Include(h => h.ChungLoai);
        //    return View(await hangHoas.ToListAsync());
        //}

        //public ActionResult List()
        //{
        //    IQueryable<HangHoa> Query = db.HangHoas.Include(p => p.ChungLoai).AsQueryable();
        //    if(Request.IsAjaxRequest())
        //    {
        //        return PartialView("_ListPartial", model: Query);
        //    }
        //    return View(Query);
        //}
        //#endregion

        //#region Xem chi tiết
        //// GET: Admin/HangHoas/Details/5
        //    public async Task<ActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HangHoa hangHoa = await db.HangHoas.FindAsync(id);
        //    if (hangHoa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hangHoa);
        //}

        //#endregion

        //#region Thêm mới
        //// GET: Admin/HangHoas/Create
        //public ActionResult Create()
        //{
        //    ViewBag.IDChungLoai = new SelectList(db.ChungLoais, "IDChungLoai", "TenChungLoai");
        //    return View();
        //}

        //// POST: Admin/HangHoas/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "IDHangHoa,TenHangHoa,GiaBan,MoTa,ConHang,star,Image_Header,Image_Detail_Large,Image_Detail_Small,IDChungLoai")] HangHoa hangHoa)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        hangHoa.IDHangHoa = Guid.NewGuid();
        //        db.HangHoas.Add(hangHoa);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IDChungLoai = new SelectList(db.ChungLoais, "IDChungLoai", "TenChungLoai", hangHoa.IDChungLoai);
        //    return View(hangHoa);
        //}

        //#endregion

        //#region cập nhật
        //// GET: Admin/HangHoas/Edit/5
        //public async Task<ActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HangHoa hangHoa = await db.HangHoas.FindAsync(id);
        //    if (hangHoa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IDChungLoai = new SelectList(db.ChungLoais, "IDChungLoai", "TenChungLoai", hangHoa.IDChungLoai);
        //    return View(hangHoa);
        //}

        //// POST: Admin/HangHoas/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "IDHangHoa,TenHangHoa,GiaBan,MoTa,ConHang,star,Image_Header,Image_Detail_Large,Image_Detail_Small,IDChungLoai")] HangHoa hangHoa)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(hangHoa).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IDChungLoai = new SelectList(db.ChungLoais, "IDChungLoai", "TenChungLoai", hangHoa.IDChungLoai);
        //    return View(hangHoa);
        //}

        //#endregion

        //#region Xóa
        //// GET: Admin/HangHoas/Delete/5
        //public async Task<ActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HangHoa hangHoa = await db.HangHoas.FindAsync(id);
        //    if (hangHoa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hangHoa);
        //}

        //// POST: Admin/HangHoas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(Guid id)
        //{
        //    HangHoa hangHoa = await db.HangHoas.FindAsync(id);
        //    db.HangHoas.Remove(hangHoa);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //#endregion

        //#region xử lý upload hình Chi tiết
        ////GET : AdminHangHoa/Upload/3            
        //public async Task<ActionResult> Upload(string id)
        //{
        //    if (id == null) return RedirectToAction("Index");
        //    try
        //    {
        //        HangHoa item = await db.HangHoas.FindAsync(Guid.Parse(id));
        //        if (item == null) throw new Exception("ID:" + id + "không tồn tại");
        //        return View(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        object cauBaoLoi = "không truy cập được dữ liệu <br/>" + ex.Message;
        //        return View("BaoLoi", cauBaoLoi);
        //    }
        //}
        ////POST :AdminHangHoa/Upload/3

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Upload(string id, HttpPostedFileBase[] taptins)
        //{
        //    try
        //    {

        //        HangHoa item = await db.HangHoas.FindAsync(Guid.Parse(id));
        //        if (taptins[0] != null)
        //        {
        //            string duongDan = Server.MapPath("~/img/");
        //            string dsTen = null;
        //            foreach (var f in taptins)
        //            {
        //                f.SaveAs(duongDan + f.FileName);
        //                dsTen += $"{f.FileName},";
        //            }
        //            //string dsTen = string.Join(",", taptins.Select(p => p.FileName).ToList());
        //            item.Image_Detail_Large = dsTen.TrimEnd(',');
        //            await db.SaveChangesAsync();
        //            // Upload thành công
        //            return RedirectToAction("Index");
        //        }
        //        // Trường hợp chưa chọn file hoặc file không có nội dung thì quay trở lại view upload
        //        ViewBag.ThongBao = "Bạn chưa chọn file hoặc file bạn chọn không có nội dung.";
        //        return View(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        object cauBaoLoi = "Upload không thành công <br/>" + ex.Message;
        //        return View("BaoLoi", cauBaoLoi);
        //    }

        //}

        //#endregion

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
