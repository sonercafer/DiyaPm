using DiyaPM.UI.Class.Interface;
using DiyaPM.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Class
{
    public class DatabaseLogger : ILogger
    {
        DiyaPMContext diyaPMContext = new DiyaPMContext();
        public void Log(string _exception, string _username)
        {
            Log log = new Log();
            log.Message = _exception;
            log.Username = _username;
            diyaPMContext.Logs.Add(log);
            diyaPMContext.SaveChanges();
        }
    }
}