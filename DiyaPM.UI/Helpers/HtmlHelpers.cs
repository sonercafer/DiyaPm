using DiyaPM.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiyaPM.UI.Helpers
{
    public static class HtmlHelpers
    {
        public static string ActivePage(this HtmlHelper helper)
        { 
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString(); 

            DiyaPMContext diyaPMContext = new DiyaPMContext();
            var menuCode = diyaPMContext.Menus.Where(x => x.Controller == currentController && x.Action == currentAction).FirstOrDefault();

            return menuCode.Code.ToString();
        }
    }
}