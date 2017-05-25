using Murphy.Business;
using Murphy.Entity;
using Murphy.Utils;
using Murphy.Core;
using Murphy.Web.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;

namespace Murphy.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {

        BaseUserBll userBll = new BaseUserBll();
        BaseModuleBll moduleBll = new BaseModuleBll();
        AjaxMsg ajaxMsg = new AjaxMsg();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginUser()
        {
          
            return View();
        }

        /// <summary>
        /// 验证登陆
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string LoginUser(FormCollection collection)
        {
            NameValueCollection frmCol = Request.Form;
            LoginUser loginUser = new LoginUser();
            loginUser.UserName = frmCol["UserName"];
            loginUser.UserPassword = frmCol["UserPassword"];
            loginUser.IsAlways = frmCol["IsAlways"];
            return JsonConvert.SerializeObject(userBll.LoginUser(loginUser.UserName,loginUser.UserPassword,loginUser.IsAlways));
        }

        /// <summary>
        /// 无权限显示错误页面
        /// </summary>
        /// <returns></returns>
        public ActionResult NoPermission()
        {
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateCode()
        {
           //首先实例化验证码的类
           ValidateCode validateCode = new ValidateCode();
           //生成验证码指定的长度
           string code = validateCode.CreateValidateCode(5);
           //将验证码赋值给Cache变量
           Session["ValidateCode"] = code;
           //创建验证码的图片
           byte[] bytes = validateCode.CreateValidateGraphic(code);
           //最后将验证码返回
           return File(bytes, @"image/jpeg");
        }

        /// <summary>
        /// 切换顶部菜单
        /// </summary>
        /// <param name="moduleId"></param>
        [HttpPost]
        public void ChangeTopModule(string moduleId)
        {
            CacheTopModule cacheTopModule = new CacheTopModule();
            cacheTopModule.ModuleId = moduleId;
            RequestCache.AddCacheTopModuleId(cacheTopModule);
        }
    }
}
