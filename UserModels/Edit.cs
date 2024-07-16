using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Simple.UserModels
{
    public class Edit
    {
        public string Id { get; set; }
        public string RealName { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email ko hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
    }
}