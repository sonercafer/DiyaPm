using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models
{
    public class Menu
    {
        [Key]
        public int id { get; set; }
        public string MenuName { get; set; }
        public int ParentID { get; set; }
        public string IconName { get; set; }

    }
}