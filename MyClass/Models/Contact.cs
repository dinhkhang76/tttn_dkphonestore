using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int? UserId { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(255)]
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public string Detail { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int? Status { get; set; }
    }
}
