using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace TTTN_ASP.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        UserDAO userDAO = new UserDAO();
        // GET: Admin/user
        public ActionResult Index()
        {
            return View(userDAO.getList("Index"));
        }

        // GET: Admin/user/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/user/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(userDAO.getList("Index"), "id", "Name", 0);
            ViewBag.ListOrder = new SelectList(userDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/user/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                user.Created_At = DateTime.Now;
                user.Password = XString.ToMD5(user.Password);
                //Upload hình ảnh
                var img = Request.Files["img"];//Lấy thông tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //Kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = user.FullName + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        string PathDir = "~/Public/images/users/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Upload hình xong
                        img.SaveAs(PathFile);
                        //Lưu vào CSDL
                        user.Img = imgName;
                    }
                }
                //End upload hình ảnh
                userDAO.Insert(user);
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(userDAO.getList("Index"), "id", "Name", 0);
            ViewBag.ListOrder = new SelectList(userDAO.getList("Index"), "Orders", "Name", 0);
            return View(user);
        }

        // GET: Admin/user/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListOrder = new SelectList(userDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/user/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = XString.ToMD5(user.Password);
                user.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                user.Updated_At = DateTime.Now;
                //Upload hình ảnh
                var img = Request.Files["img"];//Lấy thông tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //Kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = user.FullName + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        string PathDir = "~/Public/images/users/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Upload hình xong
                        img.SaveAs(PathFile);
                        //Lưu vào CSDL
                        user.Img = imgName;
                    }
                }
                //End upload hình ảnh
                userDAO.Update(user);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(userDAO.getList("Index"), "Orders", "Name", 0);
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userDAO.getRow(id);
            userDAO.Delete(user);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Trash", "User");
        }
        public ActionResult Trash()
        {
            return View(userDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã người dùng");
                return RedirectToAction("Index", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "User");
            }
            user.Status = (user.Status == 1) ? 2 : 1;
            user.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            user.Updated_At = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "User");
        }
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã người dùng không tồn tại");
                return RedirectToAction("Index", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Người dùng không tồn tại");
                return RedirectToAction("Index", "User");
            }
            user.Status = 0;//trạng thái rác bằng 0
            user.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            user.Updated_At = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "User");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            user.Status = 2;//trạng thái rác bằng 2
            user.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            user.Updated_At = DateTime.Now;
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "User");
        }
        public string NameCustomer(int? userid)
        {
            User user = userDAO.getRow(userid);
            if (userid == null)
            {
                return "";
            }
            else
            {
                return user.FullName;
            }
        }
        public string AddressCustomer(int? userid)
        {
            User user = userDAO.getRow(userid);
            if (userid == null)
            {
                return "";
            }
            else
            {
                return user.Address;
            }
        }
        public string EmailCustomer(int? userid)
        {
            User user = userDAO.getRow(userid);
            if (userid == null)
            {
                return "";
            }
            else
            {
                return user.Email;
            }
        }
        public string PhoneCustomer(int? userid)
        {
            User user = userDAO.getRow(userid);
            if (userid == null)
            {
                return "";
            }
            else
            {
                return user.Phone;
            }
        }
    }
}