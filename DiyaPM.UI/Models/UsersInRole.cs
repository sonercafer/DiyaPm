using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models
{
    public class UsersInRole
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; } 
        public int role_id { get; set; } 
    }
}