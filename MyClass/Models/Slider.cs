using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Sliders")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên Slider không được để trống!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Liên kết không được để trống!")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Chưa chọn hình ảnh!")]

        public string Img { get; set; }
        public int? Orders { get; set; }
        public string Position { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public int? Status { get; set; }
    }
}
