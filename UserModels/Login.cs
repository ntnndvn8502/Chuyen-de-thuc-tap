using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Simple.UserModels
{
    public class Login
    {
        [Required(ErrorMessage = "Chưa điền tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Chưa điền mật khẩu")]
        public string Password { get; set; }
    }
}