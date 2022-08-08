using MyClass.DAO;
using MyClass.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TTTN_ASP.Controllers
{
    public class KhachhangController : Controller
    {
        UserDAO userDAO = new UserDAO();
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderdetailDAO = new OrderDetailDAO();
        // GET: Khachhang
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection field)
        {
            String username = field["username"];
            String password = XString.ToMD5(field["password"]);
            User row_user = userDAO.getRow(username, "Customer");
            String strError = "";
            if (row_user == null)
            {
                strError = "Tài khoản hoặc mật khẩu không đúng"; // tai khoan ko ton tai
            }
            else
            {
                if (password.Equals(row_user.Password))
                {
                    Session["UserCustomer"] = username;
                    Session["NameCustomer"] = row_user.FullName;
                    Session["CustomerId"] = row_user.Id;
                    return Redirect("~/");
                }
                else
                {
                    strError = "Tài khoản hoặc mật khẩu không đúng"; // mat khau ko dung
                }
            }
            ViewBag.Error = "<div class='alert alert-danger'>" + strError + "</div>";
            return View("DangNhap");
        }

        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]

        public ActionResult DangKy(FormCollection field)
        {
            //Lấy thông tin
            String username = field["username"];
            String password = XString.ToMD5(field["password"]);
            String fullname = field["fullname"];
            String email = field["email"];
            String gender = field["gender"];
            String phone = field["phone"];
            String address = field["address"];
            //Tạo một đối tượng thành viên
            User user = new User();
            user.UserName = username;
            user.Password = password;
            user.FullName = fullname;
            user.Email = email;
            user.Gender = Convert.ToInt32(gender);
            user.Phone = phone;
            user.Address = address;
            user.Roles = "Customer";
            user.Created_At = DateTime.Now;
            user.Status = 1;
            userDAO.Insert(user);
            //User row_user = userDAO.getRow(username, "Customer");
            //Session["NameCustomer"] = row_user.FullName;
            //Session["UserCustomer"] = row_user.UserName;
            //Session["CustomerId"] = row_user.Id;
            TempData["message"] = new XMessage("success", "Đăng kí thành công");
            return RedirectToAction("DangNhap", "Khachhang");
            //return RedirectToAction("DangNhap", "Khachhang");



        }

        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = "";
            Session["CustomerId"] = "";
            return Redirect("~/dang-nhap");
        }

        public ActionResult ThongTin()
        {
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            ViewBag.order = orderDAO.getList(userid);
            ViewBag.detail = orderdetailDAO.getList();
            return View("ThongTin");
        }

        public ActionResult DonHang(/*int? page*/)
        {
            //int pageNumber = page ?? 1;//Trang hiện tại
            //int pageSize = 4; //Số mẫu tin hiển thị trên 1 trang
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            ViewBag.order = orderDAO.getList(userid);
            ViewBag.detail = orderdetailDAO.getList();
            //IPagedList<OrderInfo> list = orderDAO.getList("Order", pageSize, pageNumber);
            return View("DonHang"/*,list*/);
        }

        public ActionResult HuyDon(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return Redirect("~/don-hang");
            }
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 0;
                order.Updated_At = DateTime.Now;
                order.Updated_By = 1;
            }
            else
            {
                if (order.Status == 3)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đang vận chuyển, không thể hủy");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger", "Đơn hàng đã giao không thể hủy");
                }
                return Redirect("~/don-hang");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Hủy đơn hàng thành công");
            return Redirect("~/don-hang");
        }
    }
}