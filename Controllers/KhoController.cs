using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaSach.Models;

namespace WebNhaSach.Controllers
{
    public class KhoController : Controller
    {
        // GET: Kho
        public ActionResult Index()
        {
            if (Session["taikhoanad"] == null)
            {
                return RedirectToAction("DangNhapAd", "loginAdmin");
            }
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC XUATDULIEU");
            return View();
        }
        public ActionResult EditKho(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Message = "ID sách không hợp lệ.";
                return View();
            }
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC TimMaSachID " + id + ";");
            return View();
        }
        [HttpPost]
        public ActionResult EditKho(int soluongton, string id)
        {
            DataModels db = new DataModels();
            // Kiểm tra thông tin đầu vào
            if (soluongton < 0)
            {
                ViewBag.Message = "Số lượng tồn không hợp lệ."; // Thông báo lỗi nếu số lượng tồn không hợp lệ
                return View(); // Trả về view để thông báo lỗi
            }

            // Khởi tạo kết nối đến cơ sở dữ liệu
                try
                {
                    // Gọi stored procedure để cập nhật số lượng tồn
                    db.get("EXEC SUAKHO "+ id+ ", " + soluongton);

                // Thông báo thành công
                TempData["SuccessMessage"] = "Cập nhật số lượng thành công!";
            }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    ViewBag.Message = "Có lỗi xảy ra: " + ex.Message;
                    return View(); // Trả về view với thông báo lỗi
                }
            

            // Chuyển hướng về trang danh sách kho
            return RedirectToAction("Index", "Kho"); 
        }
        public ActionResult Xoa(String id)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC XOASANPHAM " + id);
            TempData["SuccessMessage1"] = "Xóa sản phẩm thành công!";
            return RedirectToAction("Index", "Kho");
        }


    }
}