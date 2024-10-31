using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaSach.Models;

namespace WebNhaSach.Controllers
{
    public class DonHangController : Controller
    {
        // GET: DonHang
        public ActionResult Index()
        {
            if (Session["taikhoanad"] == null)
            {
                return RedirectToAction("DangNhapAd", "loginAdmin");
            }
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC XUATDONDATHANG");
            return View();
        }
        public ActionResult Detail(String id)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC TIMHOADONTHEOID " + id + ";");
            ViewBag.listCT = db.get("EXEC CHITIETHOADONID " + id + ";");
            return View();
        }
        }
}