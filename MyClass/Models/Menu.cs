using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên Menu không được để trống!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Liên kết không được để trống!")]
        public string Link { get; set; }
        public int? TableId { get; set; }
        [Required(ErrorMessage = "Kiểu Menu không được để trống!")]

        public string TypeMenu { get; set; }
        public string Position { get; set; }
        public int? ParentId { get; set; }
        public int? Orders { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int? Status { get; set; }

    }
}
