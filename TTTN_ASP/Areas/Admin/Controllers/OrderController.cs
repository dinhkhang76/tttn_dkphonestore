using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using TTTN_ASP.Areas.Admin.Controllers;

namespace TTTN_ASP.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderdetailDAO = new OrderDetailDAO();

        // GET: Admin/Order
        public ActionResult Index()
        {
            List<Order> list = orderDAO.getList("Index");
            return View(list);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListChiTiet = orderdetailDAO.getList(id);
            return View(order);
        }

        // GET: Admin/order/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(orderDAO.getList("Index"), "id", "Name", 0);
            ViewBag.ListOrder = new SelectList(orderDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                order.Created_At = DateTime.Now;
                orderDAO.Insert(order);
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(orderDAO.getList("Index"), "id", "Name", 0);
            ViewBag.ListOrder = new SelectList(orderDAO.getList("Index"), "Orders", "Name", 0);
            return View(order);
        }

        // GET: Admin/order/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListOrder = new SelectList(orderDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                order.Updated_At = DateTime.Now;
                orderDAO.Update(order);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(orderDAO.getList("Index"), "Orders", "Name", 0);
            return View(order);
        }

        // GET: Admin/order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListChiTiet = orderdetailDAO.getList(id);
            return View(order);
        }

        // POST: Admin/order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
            orderDAO.Delete(order);
            TempData["message"] = new XMessage("success", "Xóa đơn hàng thành công");
            return RedirectToAction("Trash", "Order");
        }
        public ActionResult Trash()
        {
            return View(orderDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = (order.Status == 1) ? 2 : 1;
            order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            order.Updated_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            order.Status = 0;//trạng thái rác bằng 0
            order.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            order.Updated_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Trash", "Order");
            }
            order.Status = 1;//trạng thái rác bằng 2
            order.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            order.Updated_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Order");
        }
        public ActionResult Destroy(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == null)
            {
                TempData["message"] = new XMessage("danger", "Đơn hàng không tồn tại ");
                return RedirectToAction("Index", "Order");
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
                    TempData["message1"] = new XMessage1("danger", "Đơn hàng đang vận chuyển, không thể hủy");
                }
                if (order.Status == 4)
                {
                    TempData["message1"] = new XMessage1("danger", "Đơn hàng đã giao, không thể hủy");
                }
                return RedirectToAction("Details", "Order", new { id });
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Hủy đơn hàng thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult Confirm(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == null)
            {
                TempData["message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1 || order.Status == 2 || order.Status == 3 || order.Status == 4)
            {
                order.Status = 2;
                order.Updated_At = DateTime.Now;
                order.Updated_By = 1;

            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Xác nhận đơn hàng thành công");
            //return RedirectToAction("Index", "Order");
            return RedirectToAction("Details", "Order",new { id });
        }
        public ActionResult Shipping(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == null)
            {
                TempData["message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1 || order.Status == 2 || order.Status == 3 || order.Status == 4)
            {
                order.Status = 3;
                order.Updated_At = DateTime.Now;
                order.Updated_By = 1;

            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Đã giao cho đơn vị vận chuyển");
            //return RedirectToAction("Index", "Order");
            return RedirectToAction("Details", "Order", new { id });

        }
        public ActionResult Success(int? id)
        {
            Order order = orderDAO.getRow(id);
            if (order.Status == null)
            {
                TempData["message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Index", "Order");
            }
            if (order.Status == 1 || order.Status == 2 || order.Status == 3 || order.Status == 4)
            {
                order.Status = 4;
                order.Updated_At = DateTime.Now;
                order.Updated_By = 1;

            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Giao hàng thành công");
            //return RedirectToAction("Index", "Order");
            return RedirectToAction("Details", "Order", new { id });

        }
    }
}