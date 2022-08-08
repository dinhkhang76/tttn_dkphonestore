using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyClass.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " Họ tên không được để trống!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = " Tên đăng nhập không được để trống!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email không được để trống!")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Chưa chọn quyền")]

        public string Roles { get; set; }
        [Required(ErrorMessage = "Chưa chọn giới tính")]

        public int? Gender { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống!")]

        public string Address { get; set; }
        //[Required(ErrorMessage = "Chưa chọn hình!")]

        public string Img { get; set; }
        public int? CountError { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int? Status { get; set; }
    }
}
