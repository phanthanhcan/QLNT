using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QLNTWS.Models;

namespace QLNTWS.Controllers
{
    public class ChungLoaiController : Controller
    {
        NoiThatDbContext db = new NoiThatDbContext();
        // GET: ChungLoai
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _DanhSachChungLoai_Index()
        {
            try
            {
                List<ChungLoai> DSChungLoai = db.ChungLoais.ToList();
                return PartialView("_DanhSachChungLoai_IndexPartial",DSChungLoai);
            }
            catch(Exception ex)
            {
                return View("BaoLoi", model: ex.ToString());
            }

        }

        public ActionResult _CatagoriesMenuPartial ()
        {
            try
            {
                List<ChungLoai> ds = db.ChungLoais.Include(p => p.Loais).ToList();
                return PartialView("_CatagoriesMenuPartial", model:ds);
            }
            catch (Exception ex)
            {
                return View("BaoLoi", model: ex.ToString());
            }
        }
    }
}