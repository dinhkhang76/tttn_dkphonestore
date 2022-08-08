using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TTTN_ASP.Controllers
{
    public class GiohangController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        UserDAO userDAO = new UserDAO();
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderdetailDAO = new OrderDetailDAO();
        XCart xcart = new XCart();
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> listcart = xcart.getCart();
            return View("Index", listcart);
        }

        public ActionResult CartAdd(int productid)
        {
            Product product = productDAO.getRow(productid);//Chi tiết sản phẩm
            CartItem cartitem = new CartItem(product.Id, product.Name, product.Img, product.Price,product.PriceSale, 1);
            //Add vào giỏ hàng
            List<CartItem> listcart = xcart.AddCart(cartitem);

            return RedirectToAction("Index", "Giohang");
        }

        public ActionResult CartDel(int productid)
        {
            xcart.DelCart(productid);
            return RedirectToAction("Index", "Giohang");
        }

        public ActionResult CartUpdate(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["CAPNHAT"]))
            {
                var listqty = form["qty"];
                var listarr = listqty.Split(',');
                xcart.UpdateCart(listarr);
            }
            return RedirectToAction("Index", "Giohang");
        }

        public ActionResult CartDelAll()
        {
            xcart.DelCart();
            return RedirectToAction("Index", "Giohang");
        }

        //Thanh toán
        public ActionResult ThanhToan()
        {
            List<CartItem> listcart = xcart.getCart();
            if (Session["UserCustomer"].Equals(""))
            {
                TempData["message"] = new XMessage("danger", "Vui lòng đăng nhập trước khi thanh toán");
                return Redirect("~/dang-nhap");

            }
            int userid = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            return View("ThanhToan", listcart);
        }

        public ActionResult DatMua(FormCollection field)
        {
            //luu thong tin vao bang Order va OrderDetail
            int userid = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.getRow(userid);
            //lay thong tin
            String note = field["Note"];
            String DeliveryName = field["DeliveryName"];
            String DeliveryPhone = field["DeliveryPhone"];
            String DeliveryEmail = field["DeliveryEmail"];
            String DeliveryAddress = field["DeliveryAddress"];

            //tao don hang
            Order order = new Order();
            order.UserId = userid;
            order.Note = note;
            order.DeliveryName = DeliveryName;
            order.DeliveryPhone = DeliveryPhone;
            order.DeliveryEmail = DeliveryEmail;
            order.DeliveryAddress = DeliveryAddress;
            order.Created_At = DateTime.Now;
            order.Status = 1;
            if (orderDAO.Insert(order) == 1)
            {
                //Thêm vào OrderDetail
                List<CartItem> listcart = xcart.getCart();
                foreach (CartItem cartItem in listcart)
                {
                    OrderDetail orderdetail = new OrderDetail();
                    orderdetail.OrderId = order.Id;
                    orderdetail.ProductId = cartItem.ProductId;
                    orderdetail.Price = cartItem.Price;
                    orderdetail.PriceSale = cartItem.PriceSale;
                    orderdetail.Qty = cartItem.Qty;
                    orderdetail.Amount = cartItem.Amount;
                    orderdetailDAO.Insert(orderdetail);
                    ViewBag.thongtin = orderdetail;
                }
            }
            xcart.DelCart();
            return Redirect("~/thanh-cong");
        }

        public ActionResult ThanhCong()
        {
            List<CartItem> listcart = xcart.getCart();

            return View("ThanhCong");
        }

    }
}