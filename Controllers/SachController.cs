using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNhaSach.Models;


namespace WebNhaSach.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        public ActionResult Index()
        {
            if (Session["taikhoanad"] == null)
            {
                return RedirectToAction("DangNhapAd", "loginAdmin");
            }
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC XuatDuLieuSachN");
            return View();
        }
        public ActionResult Details(String id)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC TIMKIEMSACHTHEOIDN " + id + ";");
            return View();
        }
        public ActionResult Edit(string id)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC TIMKIEMSACHTHEOID " + id + ";");
            ViewBag.listCD = db.get("SELECT * FROM CHUDE");
            ViewBag.listNXB = db.get("SELECT * FROM NHAXUATBAN");
            ViewBag.listTG = db.get("EXEC TACGIAMASACH " + id + ";");
            return View();
        }
        [HttpPost]
        public ActionResult Edit(string tensach, string dongia, string mota, HttpPostedFileBase hinhminhhoa, string machude, string manhaxuatban, string tentacgia, string id)
        {
            try
            {
                string imageFileName = null;
                string currentImage = ""; // Biến lưu tên hình ảnh hiện tại

                // Lấy tên hình ảnh hiện tại từ ViewBag
                if (ViewBag.list != null && ViewBag.list.Count > 0)
                {
                    currentImage = ViewBag.list[0][5]?.ToString(); // Tên hình ảnh hiện tại
                }

                if (hinhminhhoa != null && hinhminhhoa.ContentLength > 0)
                {
                    // Lưu hình ảnh mới
                    imageFileName = Path.GetFileName(hinhminhhoa.FileName);
                    string path = Path.Combine(Server.MapPath("~/Hinh"), imageFileName);
                    hinhminhhoa.SaveAs(path);
                }
                else
                {
                    // Nếu không có hình ảnh mới, gán giá trị hình ảnh thành NULL
                    imageFileName = null;
                }

                // Gọi hàm cập nhật sách với các tham số đã cập nhật
                DataModels db = new DataModels();

                // Cập nhật gọi thủ tục với các tham số
                db.get("EXEC SUASACH " + id + ", N'" + tensach + "', " + dongia + ", N'" + mota + "', " + (imageFileName != null ? "'" + imageFileName + "'" : "NULL") + ", " + machude + ", " + manhaxuatban + ", N'" + tentacgia + "';");

                TempData["SuccessMessageS"] = "Cập nhật thông tin sản phẩm thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessageS"] = "Cập nhật không thành công: " + ex.Message;
            }
            return RedirectToAction("Index", "Sach");
        }



        public ActionResult ThemSachMoi()
        {
            if (Session["taikhoanad"] == null)
            {
                return RedirectToAction("DangNhapAd", "loginAdmin");
            }
            DataModels db = new DataModels();
            ViewBag.listCD = db.get("SELECT * FROM CHUDE");
            ViewBag.listNXB = db.get("SELECT * FROM NHAXUATBAN");
            

            return View();
        }

        [HttpPost]
        public ActionResult ThemSachMoi(string tensach,
                                        string dongia,
                                        string mota,
                                        HttpPostedFileBase hinhminhhoa,
                                        string machude,
                                        string manhaxuatban,
                                        string tentacgia
            )
        {
            try
            {
                if (hinhminhhoa != null && hinhminhhoa.ContentLength > 0)
                {
                    string filename = Path.GetFileName(hinhminhhoa.FileName);
                    string path = Path.Combine(Server.MapPath("~/Hinh"), filename);
                    hinhminhhoa.SaveAs(path);
                    DataModels db = new DataModels();
                    db.get("EXEC THEMSACH N'" + tensach + "'," + dongia + ",N'" + mota +
                   "','" + hinhminhhoa.FileName + "'," + machude + "," + manhaxuatban + ",N'" + tentacgia+ "';");
                    TempData["SuccessMessageT"] = "Thêm sản phẩm thành công";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra: " + ex.Message;
                return View();
            }
            return RedirectToAction("Index", "Sach");

        }
        public ActionResult XoaSach(string id)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC XOASACHTHEOID " + id);
            TempData["SuccessMessageX"] = "Xóa sản phẩm thành công!";
            return RedirectToAction("Index", "Sach");
        }
        public ActionResult TimKiemTenSach(string tensach)
        {
            DataModels db = new DataModels();
            ViewBag.list = db.get("EXEC TIMKIEMSACHTHEOTENAD '" + tensach + "'");
            ViewBag.searchTerm = tensach;
            return View();
        }



    }
}