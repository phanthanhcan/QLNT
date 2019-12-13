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

namespace QLNTWS.Controllers
{
    public class LoaiController : Controller
    {
        NoiThatDbContext db = new NoiThatDbContext();
        // GET: Loai
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly] // chỉ cho phép view gọi action
        public ActionResult _DanhSachLoai_IndexPartial()
        {
            try
            {
                List<Loai> DSLoai =  db.Loais.ToList();
                return PartialView("_DanhSachLoai_IndexPartial",  model:  DSLoai);
            }
            catch (Exception ex)
            {
                return View("BaoLoi", model: ex.ToString());
            }

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