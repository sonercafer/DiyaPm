using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models.Dto
{
    public class MenuDTO
    {
        [Key]
        public int id { get; set; }
        public string MenuName { get; set; }
        public int ParentID { get; set; }
        public string IconName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Code { get; set; }
    }
}