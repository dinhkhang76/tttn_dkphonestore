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
using TTTN_ASP;
using TTTN_ASP.Areas.Admin.Controllers;

namespace TTTN_ASP.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        SliderDAO sliderDAO = new SliderDAO();

        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View(sliderDAO.getList("Index"));
        }

        // GET: Admin/Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/slider/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(sliderDAO.getList("Index"), "id", "Name", 0);
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                if (slider.Orders == null)
                {
                    slider.Orders = 1;
                }
                else
                {
                    slider.Orders += 1;
                }
                //begin Upload file
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtention = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtention.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = XString.Str_Slug(slider.Name) + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        slider.Img = imgName;
                        string PathDir = "~/Public/images/sliders/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                //end upload file
                slider.Created_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                slider.Created_At = DateTime.Now;
                slider.Position = "slideshow";
                sliderDAO.Insert(slider);
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(sliderDAO.getList("Index"), "id", "Name", 0);
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/slider/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                if (slider.Orders == null)
                {
                    slider.Orders = 1;
                }
                else
                {
                    slider.Orders += 1;
                }
                slider.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                slider.Updated_At = DateTime.Now;
                slider.Position = "slideshow";

                //begin Upload file
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtention = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //kiểm tra tập tin
                    if (FileExtention.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = XString.Str_Slug(slider.Name) + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        slider.Img = imgName;
                        string PathDir = "~/Public/images/sliders/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                //end upload file
                sliderDAO.Update(slider);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = sliderDAO.getRow(id);
            sliderDAO.Delete(slider);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Trash", "Slider");
        }
        public ActionResult Trash()
        {
            return View(sliderDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã slider không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = (slider.Status == 1) ? 2 : 1;
            slider.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            slider.Updated_At = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã slider không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = 0;//trạng thái rác bằng 0
            slider.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            slider.Updated_At = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Slider");
            }
            slider.Status = 2;//trạng thái rác bằng 2
            slider.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            slider.Updated_At = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Slider");
        }
    }
}
