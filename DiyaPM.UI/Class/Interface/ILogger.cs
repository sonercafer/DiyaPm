using DiyaPM.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyaPM.UI.Class.Interface
{
    public interface ILogger
    {
        void Log(string _exception, string _username);
    }
}
