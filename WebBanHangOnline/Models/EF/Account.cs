using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("Account")]
    public class Account
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 ký tự")]
        public string Username { get; set; }
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 ký tự")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(4000)]
        public int RoleId { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}