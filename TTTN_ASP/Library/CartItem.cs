using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTTN_ASP
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public int Qty { get; set; } //so luong
        public decimal Amount { get; set; }  //thành tiền
        public CartItem() { }
        public CartItem(int proid, string name, string img, decimal price, decimal pricesale,int qty)
        {
            this.ProductId = proid;
            this.Name = name;
            this.Img = img;
            this.Price = price;
            this.PriceSale = pricesale;
            this.Qty = qty;
            this.Amount = qty * pricesale; //thành tiền = số lượng x giá
        }
    }
}