using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TTTN_ASP.Controllers
{
    public class LienheController : Controller
    {
        ContactDAO contactDAO = new ContactDAO();
        UserDAO userDAO = new UserDAO();
        // GET: LienHe
        public ActionResult Index()
        {
            if (Session["UserCustomer"].Equals(""))
            {
                TempData["message"] = new XMessage("danger", "Bạn cần đăng nhập để truy cập trang này");
                return Redirect("~/dang-nhap"); //chuyển hướng đến URL
            }
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            return View();
        }
        public ActionResult GuiMail(FormCollection filed)
        {
            int userid = int.Parse(Session["CustomerId"].ToString());//Mã người đăng nhập
            //Lấy thông tin
            String fullname = filed["fullname"];
            String email = filed["email"];
            String phone = filed["phone"];
            String title = filed["title"];
            String detail = filed["detail"];

            //Tạo một đối tượng thành viên
            Contact contact = new Contact();
            contact.UserId = userid;
            contact.FullName = fullname;
            contact.Email = email;
            contact.Phone = phone;
            contact.Title = title;
            contact.Detail = detail;
            contact.Created_At = DateTime.Now;
            contact.Created_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            contact.Status = 1;
            TempData["message"] = new XMessage("success", "Gửi thành công");
            contactDAO.Insert(contact);
            return Redirect("~/lien-he");
        }
    }
}