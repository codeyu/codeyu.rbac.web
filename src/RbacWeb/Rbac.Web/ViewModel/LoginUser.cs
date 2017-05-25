using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Murphy.Web.ViewModel
{
    /// <summary>
    /// 登陆视图模型
    /// </summary>
    public class LoginUser
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string IsAlways { get; set; }
    }
}