using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models
{
    public class MenusInRole
    {
        [Key]
        public int id { get; set; }
        public int role_id { get; set; }
        public int menu_id { get; set; }
    }
}