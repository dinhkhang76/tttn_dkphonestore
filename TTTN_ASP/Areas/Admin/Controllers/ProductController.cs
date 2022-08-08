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
    public class ProductController : BaseController
    {
        ProductDAO productDAO = new ProductDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        private MyDBContext db = new MyDBContext();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.GetList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(productDAO.GetList("Index"), "Orders", "Name", 0);
            ViewBag.List = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                //Upload hình ảnh
                var img = Request.Files["img"];//Lấy thông tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //Kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        string PathDir = "~/Public/images/products/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Upload hình xong
                        img.SaveAs(PathFile);
                        //Lưu vào CSDL
                        product.Img = imgName;
                    }
                }
                //End upload hình ảnh
                product.Slug = XString.Str_Slug(product.Name);
                product.Created_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                product.Created_At = DateTime.Now;
                productDAO.Insert(product);
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index","Product");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoryDAO.getList("Index"), "Orders", "Name", 0);
            ViewBag.List = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoryDAO.getList("Index"), "Orders", "Name", 0);
            ViewBag.List = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                product.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                product.Updated_At = DateTime.Now;
                //Upload hình ảnh
                var img = Request.Files["img"];//Lấy thông tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    //Kiểm tra tập tin
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        string PathDir = "~/Public/images/products/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Xoá hình cũ
                        if (product.Img != null)
                        {
                            string PathFileDelete = Path.Combine(Server.MapPath(PathDir), product.Img);
                            System.IO.File.Delete(PathFileDelete);
                        }
                        //Upload hình xong
                        img.SaveAs(PathFile);
                        //Lưu vào CSDL
                        product.Img = imgName;
                    }
                }
                //End upload hình ảnh
                product.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                product.Updated_At = DateTime.Now;
                productDAO.Update(product);
                //Notification.set_flash("Đã cập nhật lại thông tin sản phẩm!", "success");
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(categoryDAO.getList("Index"), "Orders", "Name", 0);
            ViewBag.List = new SelectList(db.Categorys.Where(m => m.Status != 0).ToList(), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            TempData["message"] = new XMessage("success", "Xóa thành công");

            return RedirectToAction("Trash", "Product");
        }
        public ActionResult Trash()
        {
            return View(productDAO.GetList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                //Notification.set_flash("Mã sản phẩm không tồn tại!", "danger");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Sản phẩm không tồn tại");
                //Notification.set_flash("Mẫu tin không tồn tại!", "danger");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            product.Updated_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            //Notification.set_flash("Thay đổi trạng thái thành công", "success");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {

                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                //Notification.set_flash("Mã sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Sản phẩm không tồn tại");
                //Notification.set_flash("Mẫu tin không tồn tại", "danger");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;//trạng thái rác bằng 0
            product.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            product.Updated_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            //Notification.set_flash("Xóa vào thùng rác thành công", "success");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2; //status = 0 = Xóa vào thùng rác
            product.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            product.Updated_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Product");
        }
        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            Product mProduct = db.Products.Find(id);
            mProduct.Status = (mProduct.Status == 1) ? 2 : 1;

            mProduct.Updated_At = DateTime.Now;
            mProduct.Updated_By = int.Parse(Session["UserId"].ToString());
            db.Entry(mProduct).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { Status = mProduct.Status });
        }
        public string ProductImg(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Img;
            }
        }
        public string ProductName(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
    }

}
