using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Chưa chọn danh mục!")]

        public int CatId { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Chi tiết sản phẩm không được để trống!")]
        public string Detail { get; set; }
        [Required(ErrorMessage = "Chưa chọn hình!")]

        public string Img { get; set; }
        [Required(ErrorMessage = "Giá tiền không được để trống!")]

        public decimal Price { get; set; }
        [Required(ErrorMessage = "Giá khuyến mãi không được để trống!")]
        public decimal PriceSale { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống!")]

        public string Number { get; set; }
        [Required(ErrorMessage = "Từ khóa không được để trống!")]

        public string MetaKey { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống!")]

        public string MetaDesc { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int? Status { get; set; }
    }
}
