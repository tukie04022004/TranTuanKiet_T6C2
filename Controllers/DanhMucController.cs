using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaSach.Models;

namespace WebNhaSach.Controllers
{
    public class DanhMucController : Controller
    {
        // GET: DanhMuc
        public ActionResult Index()
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("SELECT * FROM CHUDE");
            return View();
        }
        public ActionResult ThemCD()
        {

            DataModels db = new DataModels();
            ViewBag.list = db.get("SELECT * FROM CHUDE");
            return View();
        }
        [HttpPost]
        public ActionResult ThemCD(string tenchude)
        {

            try
            {
                    DataModels db = new DataModels();
                    db.get("EXEC THEMDANHMUC N'" + tenchude + "';");
                    TempData["SuccessMessageT"] = "Thêm chủ đề thành công";
                    return RedirectToAction("Index", "DanhMuc");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
                return View();
            }
           
        }
        public ActionResult EditCD(string id)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC XUATCHUDE "+ id);
            return View();
        }
        [HttpPost]
        public ActionResult EditCD( string id, string tendanhmuc)
        {
            DataModels db = new DataModels();
            try
            {
                db.get("EXEC SUACHUDE "+ id +", " + "N'"+tendanhmuc+"'");

                // Thông báo thành công
                TempData["SuccessMessage"] = "Cập nhật tên danh mục thành công!";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                ViewBag.Message = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction("EditCD", "DanhMuc"); // Trả về view với thông báo lỗi
            }


            // Chuyển hướng về trang danh sách kho
            return RedirectToAction("Index", "DanhMuc");

        }
        public ActionResult Xoa(String id)
        {
            DataModels db = new DataModels();

            // Gọi thủ tục lưu trữ để xóa chủ đề theo mã chủ đề (macd)
            try
            {
                // Thực thi thủ tục xóa chủ đề
                db.get("EXEC XOACHUDE " + id);

                // Nếu xóa thành công
                TempData["SuccessMessage"] = "Xóa chủ đề thành công!";
            }
            catch (SqlException ex)
            {
                // Kiểm tra lỗi phát sinh từ thủ tục
                if (ex.Number == 50000) // Giả định rằng RAISERROR sẽ trả về lỗi với số 50000
                {
                    TempData["ErrorMessage"] = ex.Message; // Hiển thị thông báo lỗi từ thủ tục
                }
                else
                {
                    TempData["ErrorMessage"] = "Đã có lỗi xảy ra khi xóa chủ đề.";
                }
            }

            return RedirectToAction("Index", "DanhMuc");
        }
    }
}