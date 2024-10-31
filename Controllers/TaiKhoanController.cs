using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaSach.Models;

namespace WebNhaSach.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        public ActionResult Index(string usn)
        {
            DataModels db = new DataModels();

            // Lấy tên đăng nhập từ Session
             usn = Session["taikhoan"]?.ToString(); // Sử dụng toán tử null-conditional để tránh NullReferenceException

            // Kiểm tra nếu người dùng đã đăng nhập
            if (!string.IsNullOrEmpty(usn))
            {
                // Lấy thông tin khách hàng từ cơ sở dữ liệu
                ViewBag.list = db.get($"EXEC THONGTINKHACHHANG '{usn}';"); // Sử dụng cú pháp interpolated string
            }
            else
            {
                // Nếu chưa đăng nhập, thiết lập thông báo
                ViewBag.Message = "Bạn chưa đăng nhập.";
            }

            return View();
        }
        public ActionResult EditKH(string usn)
        {
            DataModels db = new DataModels();

            // Lấy tên đăng nhập từ Session
            usn = Session["taikhoan"]?.ToString();
            if (!string.IsNullOrEmpty(usn))
            {
                // Lấy thông tin khách hàng từ cơ sở dữ liệu
                ViewBag.list = db.get($"EXEC THONGTINKHACHHANG '{usn}';"); // Sử dụng cú pháp interpolated string
            }
            else
            {
                // Nếu chưa đăng nhập, thiết lập thông báo
                ViewBag.Message = "Bạn chưa đăng nhập.";
            }

            return View();

        }
        [HttpPost]
        public ActionResult EditKH(string hoten, string diachi, string dienthoai, DateTime ngaysinh, bool? gioitinh, string email)
        {
            string tendn = Session["taikhoan"]?.ToString(); // Lấy tên đăng nhập từ session
            if (!string.IsNullOrEmpty(tendn))
            {
                // Khởi tạo kết nối đến cơ sở dữ liệu
                DataModels db = new DataModels();

                // Kiểm tra nếu gioitinh có giá trị
                if (gioitinh.HasValue)
                {
                    // Câu lệnh SQL để cập nhật thông tin khách hàng
                    int gioitinhValue = gioitinh.Value ? 1 : 0; // Chuyển đổi bool sang int (1 hoặc 0)

                    db.get("EXEC SUAKHACHHANG N'" + tendn + "', N'" + hoten + "', N'" +
                            diachi + "', N'" + dienthoai + "', '" + ngaysinh.ToString("yyyy-MM-dd") + "', " + gioitinhValue + ", N'" + email + "';");
                }
                else
                {
                    // Xử lý trường hợp gioitinh không được chọn
                    ViewBag.Message = "Vui lòng chọn giới tính.";
                    return View(); // Trả về view để thông báo lỗi
                }

                return RedirectToAction("Index", "TaiKhoan"); // Chuyển hướng về danh sách khách hàng
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