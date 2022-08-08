using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace TTTN_ASP.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private MyDBContext db = new MyDBContext();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(FormCollection field)
        {
            ViewBag.Error = "";
            string username = field["username"];
            string password = XString.ToMD5(field["password"]);
            User user = db.Users
                .Where(m => /*m.Roles == "Admin" &&*/ m.Status == 1 && (m.UserName == username || m.Email == username))
                .FirstOrDefault();
            if (user != null)
            {
                if (user.Roles != "Admin")
                {
                    ViewBag.Error = "<p class='login-box-msg text-danger'>Bạn không có quyền truy cập trang này. Vui lòng liên hệ người quản lý</p>";
                }
                else
                {
                    if (user.Password.Equals(password))
                    {
                        Session["UserAdmin"] = username;
                        Session["UserId"] = user.Id.ToString();
                        Session["Fullname"] = user.FullName;
                        Session["Img"] = user.Img;
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        if (user.CountError == null)
                        {
                            user.CountError = 0;
                        }
                        else
                        {
                            user.CountError += 1;
                        }
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Error = "<p class='login-box-msg text-danger'>Tài khoản hoặc mật khẩu không đúng</p>"; //Mat khau sai
                    }
                }
            }
            else
            {
                ViewBag.Error = "<p class='login-box-msg text-danger'>*Tài khoản hoặc mật khẩu không đúng </p>"; //Tai khoan ko ton tai
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
            Session["Fullname"] = "";
            Session["Img"] = "";
            //return Redirect("~/Admin/login");
            return Redirect("~/dang-nhap-trang-quan-tri");
        }
    }
}