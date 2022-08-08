using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using System.IO;

namespace TTTN_ASP.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private PostDAO postDAO = new PostDAO();
        private TopicDAO topicDAO = new TopicDAO();
        public ActionResult Index()
        {
            return View(postDAO.getListJoin("Index", "Post"));
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
            ViewBag.ListCat = new SelectList(postDAO.getList("Index"), "id", "Name", 0);
            ViewBag.ListOrder = new SelectList(postDAO.getList("Index"), "Orders", "Name", 0);
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), " Id", "Name");

            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Post";
                post.Slug = XString.Str_Slug(post.Title);
                //begin Upload file
                var img = Request.Files["Img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtention = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtention.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        string PathDir = "~/Public/images/posts/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                        post.Img = imgName;
                    }
                }
                //end upload file
                post.Created_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                post.Created_At = DateTime.Now;
                postDAO.Insert(post);
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(postDAO.getList("Index"), "id", "Name", 0);
            ViewBag.ListOrder = new SelectList(postDAO.getList("Index"), "Orders", "Name", 0);
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), " Id", "Name");

            return View(post);
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCat = new SelectList(postDAO.getList("Index"), "id", "Name", 0);
            ViewBag.ListOrder = new SelectList(postDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), " Id", "Name");

            return View(post);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Post";
                post.Slug = XString.Str_Slug(post.Title);
                //begin Upload file
                var img = Request.Files["Img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtention = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtention.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        string PathDir = "~/Public/images/posts/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                        post.Img = imgName;
                    }
                }
                //end upload file
                post.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                post.Updated_At = DateTime.Now;
                postDAO.Update(post);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), " Id", "Name");

            ViewBag.ListCat = new SelectList(postDAO.getList("Index"), "id", "Name", 0);
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
            postDAO.Delete(post);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Trash", "Post");
        }

        public ActionResult Trash()
        {
            return View(postDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã bài viết không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Bài viết không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            post.Updated_At = DateTime.Now;

            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã bài viết không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Bài viết không tồn tạii");
                return RedirectToAction("Index", "Post");
            }
            post.Status = 0;//trạng thái rác bằng 0
            post.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            post.Updated_At = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã bài viết phẩm không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Bài viết không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            post.Status = 2;//trạng thái rác bằng 2
            post.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            post.Updated_At = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Post");
        }
    }
}
