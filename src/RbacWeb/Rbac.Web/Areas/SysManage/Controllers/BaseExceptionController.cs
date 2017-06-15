using Rbac.Entity;
using Rbac.Utils;
using Rbac.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.SysManage.Controllers
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class BaseExceptionController : BaseController
    {
        public ActionResult Index()
        {
            List<BaseFile> baseFileList = new List<BaseFile>();
            GetAllFile(ConfigHelper.GetValue("ExceptionLog"), baseFileList);
            ViewBag.TotalItems = baseFileList.Count;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(baseFileList);
        }


        public void GetAllFile(string path, List<BaseFile> baseFileList)
        {
            string[] fileList= Directory.GetFiles(path);
            for (int i = 0; i < fileList.Length; i++)
            {
                BaseFile baseFile = new BaseFile()
                {
                    FileName = System.IO.Path.GetFileName(fileList[i]),
                    FileSize = (new FileInfo(fileList[i]).Length / 1024).ToString() + "kb",
                    FilePath = HashEncrypt.Encrypt(fileList[i])
                };
                baseFileList.Add(baseFile);
            }
        }

        /// <summary>
        /// 查看日志明细
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ActionResult FileText(string filePath)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            if (!string.IsNullOrEmpty(filePath))
            {
                filePath = HashEncrypt.Decrypt(filePath);
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                ViewBag.FileText = sr.ReadToEnd().ToString();
                fs.Close();
                sr.Close();
            }
            return View();
        }

        /// <summary>
        /// 下载日志文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public FilePathResult DownloadFile(string filePath)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
           filePath = HashEncrypt.Decrypt(filePath);
           string fileName = System.IO.Path.GetFileName(filePath);
           return File(filePath, "text/plain", fileName);
        }
    }
}
