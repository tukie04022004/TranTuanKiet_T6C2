using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaSach.Models;

namespace WebNhaSach.Controllers
{
    public class loginAdminController : Controller
    {
        public ActionResult DangNhapAd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhapAd(string usernamead, string passwordad)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC KIEMTRADANGNHAPADMIN '" + usernamead + "','" + passwordad + "'");
            if (ViewBag.list.Count > 0)
            {
                Session["taikhoanad"] = usernamead;
                TempData["SuccessMessage"] = "Đăng nhập thành công!";
                return RedirectToAction("Index", "Sach");
            }
            else
            {
                TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không chính xác.";
                return RedirectToAction("DangNhapAd", "loginAdmin");
            }

        }
        public ActionResult TaiKhoanNV(string usn)
        {
            DataModels db = new DataModels();

            // Lấy tên đăng nhập từ Session
            usn = Session["taikhoanad"]?.ToString(); // Sử dụng toán tử null-conditional để tránh NullReferenceException

            // Kiểm tra nếu người dùng đã đăng nhập
            if (!string.IsNullOrEmpty(usn))
            {
                // Lấy thông tin khách hàng từ cơ sở dữ liệu
                ViewBag.list = db.get($"EXEC THONGTINNHANVIEN '{usn}';"); // Sử dụng cú pháp interpolated string
            }
            else
            {
                // Nếu chưa đăng nhập, thiết lập thông báo
                ViewBag.Message = "Bạn chưa đăng nhập.";
            }

            return View();
        }
        public ActionResult EditNV(string usn)
        {
            DataModels db = new DataModels();

            // Lấy tên đăng nhập từ Session
            usn = Session["taikhoanad"]?.ToString(); // Sử dụng toán tử null-conditional để tránh NullReferenceException

            // Kiểm tra nếu người dùng đã đăng nhập
            if (!string.IsNullOrEmpty(usn))
            {
                // Lấy thông tin khách hàng từ cơ sở dữ liệu
                ViewBag.list = db.get($"EXEC THONGTINNHANVIEN '{usn}';"); // Sử dụng cú pháp interpolated string
            }
            else
            {
                // Nếu chưa đăng nhập, thiết lập thông báo
                ViewBag.Message = "Bạn chưa đăng nhập.";
            }

            return View();
        }   
        [HttpPost]
        public ActionResult EditNV(string hoten, string diachi, string dienthoai)
        {
            string tendn = Session["taikhoanad"]?.ToString(); // Lấy tên đăng nhập từ session
            if (!string.IsNullOrEmpty(tendn))
            {
                // Khởi tạo kết nối đến cơ sở dữ liệu
                DataModels db = new DataModels();
                    db.get("EXEC SUANHANVIEN N'" + tendn + "', N'" + hoten + "', N'" +
                            diachi + "', N'" + dienthoai + "';");
                TempData["SuccessMessage"] = "Sửa thông tin nhân viên thành công";
                return RedirectToAction("TaiKhoanNV", "loginAdmin"); // Chuyển hướng về danh sách khách hàng
            }
            else
            {
                // Nếu chưa đăng nhập, thiết lập thông báo
                ViewBag.Message = "Bạn chưa đăng nhập.";
                return View(); // Trả về view mà không thực hiện cập nhật
            }
        }
    }
}