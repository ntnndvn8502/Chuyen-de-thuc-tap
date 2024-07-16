using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Simple.UserModels
{
    public class Register
    {
        [Required(ErrorMessage = "Bạn chưa điền tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền mật khẩu")]
        [MinLength(5,ErrorMessage = "Mật khẩu chưa đủ độ dài")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage ="Xác nhận mật khẩu không khớp")]
        public string ConfirmedPassword { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền họ tên")]
        public string RealName { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email ko hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }






    }
}