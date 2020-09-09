using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models
{
    public class Log
    {
        [Key]
        public int id { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
    }
}