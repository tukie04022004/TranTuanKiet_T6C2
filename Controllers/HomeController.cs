using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaSach.Models;

namespace WebNhaSach.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("SELECT * FROM SACH");
            ViewBag.listCD = db.get("SELECT * FROM CHUDE");
            return View();
        }
        public ActionResult Detail(String id)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC TIMKIEMSACHTHEOID " + id + ";");
            ViewBag.listCD = db.get("SELECT * FROM CHUDE");
            return View();
        }
        public ActionResult TimKiemTenSach(string tensach)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC TIMKIEMSACHTHEOTEN '" + tensach + "'");
            ViewBag.listCD = db.get("SELECT * FROM CHUDE");
            ViewBag.searchTerm = tensach;
            return View();
        }
        public ActionResult Timsachtheochude(string id)
        {
            DataModels db = new DataModels();
            ViewBag.listCDD = db.get("EXEC TIMSACHTHEOCHUDE " + id);
            ViewBag.listCD = db.get("SELECT * FROM CHUDE");
            return View();
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(string hotenkh,
                          string diachikh,
                          string dienthoai,
                          string tendn,
                          string matkhau,
                          DateTime? ngaysinh,
                          bool gioitinh,
                          string email)
        {
            DataModels db = new DataModels();
            try
            {
                // Thêm khách hàng mới
                db.get("EXEC DANGKY N'" + hotenkh + "', N'" + diachikh + "', '" + dienthoai + "', '" + tendn + "', '" + matkhau + "', " + (ngaysinh.HasValue ? "'" + ngaysinh.Value.ToString("yyyy-MM-dd") + "'" : "NULL") + ", " + gioitinh + ", '" + email + "';");

                TempData["SuccessMessage"] = "Đăng ký thành công!";
                return RedirectToAction("DangNhap", "Home"); // Chuyển hướng sau khi đăng ký thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                TempData["ErrorMessage"] = "Đăng ký không thành công: " + ex.Message;
                return RedirectToAction("DangKy"); // Chuyển về trang đăng ký
            }
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC KIEMTRADANGNHAP '" + username + "','" + password + "'");
            if (ViewBag.list.Count > 0)
            {
                Session["taikhoan"] = username;
                TempData["SuccessMessage"] = "Đăng nhập thành công!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                return RedirectToAction("DangNhap", "Home");
            }

        }
        public ActionResult DangXuat()
        {
            // Xóa thông tin người dùng khỏi session
            Session["taikhoan"] = null; // Hoặc Session.Clear() để xóa tất cả

            // Thiết lập thông báo cho TempData
            TempData["Message"] = "Bạn đã đăng xuất thành công.";

            // Chuyển hướng về trang chính
            return RedirectToAction("Index", "Home");
        }


    }
}