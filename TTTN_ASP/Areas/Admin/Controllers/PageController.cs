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
    public class PageController : BaseController
    {
        PostDAO postDAO = new PostDAO();
        LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Page
        public ActionResult Index()
        {
            return View(postDAO.getList("Index", "Page"));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Page";
                post.Slug = XString.Str_Slug(post.Title);
                post.Created_By = Convert.ToInt32(Session["UserId"].ToString());
                post.Created_At = DateTime.Now;
                //Upload hình ảnh
                var img = Request.Files["img"];//Lấy thông tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //Kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        string PathDir = "~/Public/images/pages/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Upload hình xong
                        img.SaveAs(PathFile);
                        //Lưu vào CSDL
                        post.Img = imgName;
                    }
                }
                //End upload hình ảnh
                if (postDAO.Insert(post) == 1)
                {
                    Link link = new Link();
                    link.Slug = post.Slug;
                    link.TableId = post.Id;
                    link.TypeLink = "page";
                    post.TopicId = null;

                    linkDAO.Insert(link);
                }

                TempData["message"] = new XMessage("success", "Thêm thành công");

                return RedirectToAction("Index");
            }
            return View(post);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Page";
                post.Slug = XString.Str_Slug(post.Title);
                post.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                post.Updated_At = DateTime.Now;
                //Upload hình ảnh
                var img = Request.Files["img"];//Lấy thông tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //Kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        string PathDir = "~/Public/images/pages/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Xoá hình cũ
                        if (post.Img != null)
                        {
                            string PathFileDelete = Path.Combine(Server.MapPath(PathDir), post.Img);
                            System.IO.File.Delete(PathFileDelete);
                        }
                        //Upload hình xong
                        img.SaveAs(PathFile);
                        //Lưu vào CSDL
                        post.Img = imgName;
                    }
                }
                //End upload hình ảnh
                if (postDAO.Update(post) == 1)
                {
                    Link link = linkDAO.getRow(post.Id, "page");
                    link.Slug = post.Slug;
                    post.TopicId = null;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(postDAO.getList("Index"), "Orders", "Name", 0);
            return View(post);
        }
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            Link link = linkDAO.getRow(post.Id, "page");
            if (postDAO.Delete(post) == 1)
            {
                linkDAO.Delete(link); 
            }
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Trash", "Page");
        }

        public ActionResult Trash()
        {
            return View(postDAO.getList("Trash", "Page"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Trang đơn không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Trang đơn không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            post.Updated_At = DateTime.Now;

            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Page");
        }
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Trang đơn không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Trang đơn không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = 0;//trạng thái rác bằng 0
            post.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            post.Updated_At = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Page");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            post.Status = 2;//trạng thái rác bằng 2
            post.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            post.Updated_At = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Page");
        }
    }
}