using MyClass.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDAO
    {
        private MyDBContext db = new MyDBContext();
        public List<Order> getList(int userid)
        {
            List<Order> list = db.Orders.Where(m => m.UserId == userid && m.Status != 0).ToList();
            return list;
        }
        public List<Order> getList(string status = "All")
        {
            List<Order> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders.ToList();
                        break;
                    }
            }
            return list; //select * from Categorys
        }


        //public IPagedList<OrderInfo> getList(string type, int pageSize, int pageNumber)
        //{
        //    IPagedList<OrderInfo> list = db.Orders
        //                    .Join(
        //                    db.OrderDetails,
        //                    o => o.Id,
        //                        od => od.OrderId,
        //                        (o, od) => new OrderInfo
        //                        {
        //                            Id = o.Id,
        //                            UserId = o.UserId,
        //                            Note = o.Note,
        //                            DeliveryName = o.DeliveryName,
        //                            DeliveryEmail = o.DeliveryEmail,
        //                            DeliveryPhone = o.DeliveryPhone,
        //                            DeliveryAddress = o.DeliveryAddress,
        //                            Created_At = o.Created_At,
        //                            Updated_By = o.Updated_By,
        //                            Updated_At = o.Updated_At,
        //                            OrderId = od.OrderId,
        //                            Status = o.Status,
        //                            ProductId = od.ProductId,
        //                            Price = od.Price,
        //                            Qty = od.Qty,
        //                            Amount = od.Amount
        //                        }
        //                    )
        //                    .Where(m => m.Status != 0)
        //                    .OrderByDescending(m => m.Created_At)
        //                    .ToPagedList(pageNumber, pageSize);
        //    return list;
        //}

        public List<OrderInfo> getListJoin(string status = "All")
        {
            List<OrderInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders
                            .Join(
                                db.OrderDetails,
                                o => o.Id,
                                od => od.OrderId,
                                (o, od) => new OrderInfo
                                {
                                    Id = o.Id,
                                    UserId = o.UserId,
                                    Note = o.Note,
                                    DeliveryName = o.DeliveryName,
                                    DeliveryEmail = o.DeliveryEmail,
                                    DeliveryPhone = o.DeliveryPhone,
                                    DeliveryAddress = o.DeliveryAddress,
                                    Created_At = o.Created_At,
                                    Updated_By = o.Updated_By,
                                    Updated_At = o.Updated_At,
                                    OrderId = od.OrderId,
                                    Status = o.Status,
                                    ProductId = od.ProductId,
                                    Price = od.Price,
                                    Qty = od.Qty,
                                    Amount = od.Amount
                                }
                            )
                            .Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders
                            .Join(
                                db.OrderDetails,
                                o => o.Id,
                                od => od.OrderId,
                                (o, od) => new OrderInfo
                                {
                                    Id = o.Id,
                                    UserId = o.UserId,
                                    Note = o.Note,
                                    DeliveryName = o.DeliveryName,
                                    DeliveryEmail = o.DeliveryEmail,
                                    DeliveryPhone = o.DeliveryPhone,
                                    DeliveryAddress = o.DeliveryAddress,
                                    Created_At = o.Created_At,
                                    Updated_By = o.Updated_By,
                                    Updated_At = o.Updated_At,
                                    OrderId = od.OrderId,
                                    Status = o.Status,
                                    ProductId = od.ProductId,
                                    Price = od.Price,
                                    Qty = od.Qty,
                                    Amount = od.Amount
                                }
                            )
                            .Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders
                            .Join(
                                db.OrderDetails,
                                o => o.Id,
                                od => od.OrderId,
                                (o, od) => new OrderInfo
                                {
                                    Id = o.Id,
                                    UserId = o.UserId,
                                    Note = o.Note,
                                    DeliveryName = o.DeliveryName,
                                    DeliveryEmail = o.DeliveryEmail,
                                    DeliveryPhone = o.DeliveryPhone,
                                    DeliveryAddress = o.DeliveryAddress,
                                    Created_At = o.Created_At,
                                    Updated_By = o.Updated_By,
                                    Updated_At = o.Updated_At,
                                    OrderId = od.OrderId,
                                    Status = o.Status,
                                    ProductId = od.ProductId,
                                    Price = od.Price,
                                    Qty = od.Qty,
                                    Amount = od.Amount
                                }
                            )
                            .ToList();
                        break;
                    }
            }
            return list;
        }



        public Order getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }

        public int Insert(Order row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật
        public int Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xóa
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
    }
}
