using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Rol Adı")]
        public string Name { get; set; }
    }
}