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
    public class HoaDonChiTietsController : Controller
    {
        private NoiThatDbContext db = new NoiThatDbContext();

        // GET: Admin/HoaDonChiTiets
        public async Task<ActionResult> Index()
        {
            var hoaDonChiTiets = db.HoaDonChiTiets.Include(h => h.HoaDon).Include(h => h.SanPham);
            return View(await hoaDonChiTiets.ToListAsync());
        }
        public async Task<ActionResult> ChiTietDonHang(int id)
        {
            var hoaDonChiTiets = db.HoaDonChiTiets.Include(h => h.SanPham).Where(o => o.HoaDonID == id);
            return View("index", await hoaDonChiTiets.ToListAsync());
        }

        // GET: Admin/HoaDonChiTiets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonChiTiet hoaDonChiTiet = await db.HoaDonChiTiets.FindAsync(id);
            if (hoaDonChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonChiTiet);
        }

        // GET: Admin/HoaDonChiTiets/Create
        public ActionResult Create()
        {
            ViewBag.HoaDonID = new SelectList(db.HoaDons, "ID", "HoTenKhach");
            ViewBag.SanPhamID = new SelectList(db.SanPhams, "SanPhamID", "MaSo");
            return View();
        }

        // POST: Admin/HoaDonChiTiets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HoaDonID,SanPhamID,SoLuong,DonGia,ThanhTien")] HoaDonChiTiet hoaDonChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonChiTiets.Add(hoaDonChiTiet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.HoaDonID = new SelectList(db.HoaDons, "ID", "HoTenKhach", hoaDonChiTiet.HoaDonID);
            ViewBag.SanPhamID = new SelectList(db.SanPhams, "SanPhamID", "MaSo", hoaDonChiTiet.SanPhamID);
            return View(hoaDonChiTiet);
        }

        // GET: Admin/HoaDonChiTiets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonChiTiet hoaDonChiTiet = await db.HoaDonChiTiets.FindAsync(id);
            if (hoaDonChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.HoaDonID = new SelectList(db.HoaDons, "ID", "HoTenKhach", hoaDonChiTiet.HoaDonID);
            ViewBag.SanPhamID = new SelectList(db.SanPhams, "SanPhamID", "MaSo", hoaDonChiTiet.SanPhamID);
            return View(hoaDonChiTiet);
        }

        // POST: Admin/HoaDonChiTiets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "HoaDonID,SanPhamID,SoLuong,DonGia,ThanhTien")] HoaDonChiTiet hoaDonChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDonChiTiet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.HoaDonID = new SelectList(db.HoaDons, "ID", "HoTenKhach", hoaDonChiTiet.HoaDonID);
            ViewBag.SanPhamID = new SelectList(db.SanPhams, "SanPhamID", "MaSo", hoaDonChiTiet.SanPhamID);
            return View(hoaDonChiTiet);
        }

        // GET: Admin/HoaDonChiTiets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonChiTiet hoaDonChiTiet = await db.HoaDonChiTiets.FindAsync(id);
            if (hoaDonChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonChiTiet);
        }

        // POST: Admin/HoaDonChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HoaDonChiTiet hoaDonChiTiet = await db.HoaDonChiTiets.FindAsync(id);
            db.HoaDonChiTiets.Remove(hoaDonChiTiet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
