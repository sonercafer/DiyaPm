using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [DisplayName("Şifre")]
        public string Password { get; set; }
    }
}