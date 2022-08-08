using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Note { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryEmail { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryAddress { get; set; }
        //public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int? Status { get; set; }
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
    }
}
