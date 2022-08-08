using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "Chưa chọn chủ đề bài viết!")]

        public int? TopicId { get; set; }
        [Required(ErrorMessage = "Tiêu đề bài viết không được để trống!")]
        public string Title { get; set; }

        public string Slug { get; set; }
        [Required(ErrorMessage = "Chi tiết bài viết không được để trống!")]

        public string Detail { get; set; }
        [Required(ErrorMessage = "Chưa chọn hình ảnh!")]

        public string Img { get; set; }
        public string PostType { get; set; }
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
