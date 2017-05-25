using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Utils
{
    /// <summary>
    /// 分页帮助类
    /// </summary>
    public class PageHelper
    {
        /// <summary>
        /// 得到分页HTML
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string GetPageHtml(long recordCount, int pageNumber, int pageSize, string query)
        {
           
            //var navigationUrl = RequestHelper.GetScriptUrl;
            //var queryString = RequestHelper.GetScriptNameQueryString;

            //得到共有多少页
            long PageCount = recordCount <= 0 ? 1 : recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1;

            long pNumber = pageNumber;
            if (pNumber < 1)
            {
                pNumber = 1;
            }
            else if (pNumber > PageCount)
            {
                pNumber = PageCount;
            }

            //如果只有一页则返回空分页字符串
            if (PageCount <= 1)
            {
                return "";
            }

            StringBuilder ReturnPagerString = new StringBuilder(1500);
            string JsFunctionName = string.Empty;

            //构造分页字符串
            int DisplaySize = 10;//中间显示的页数  

            ReturnPagerString.Append("<div class=\"pagination\"><div class=\"pull-right\"><div class=\"input-append\"><input class=\"input-medium\" id=\"redirect_page\" type=\"text\" value=\"\"><button class=\"btn\" type=\"button\" onclick=\"javascript:_toPage_" + JsFunctionName + "($('#redirect_page').val()," + pageSize.ToString() + ");\">直达页面</button></div></div><ul><li class=\"disabled\"><a href=\"#\">页数：" + pageNumber + " / " + PageCount + "</a></li>");
            if (pNumber > 1)
                ReturnPagerString.Append("<li><a  href=\"javascript:_toPage_" + JsFunctionName + "(" + (pNumber - 1).ToString() + "," + pageSize.ToString() + ");\">上页</a></li>");
            //添加第一页
            if (pNumber >= DisplaySize / 2 + 3)
                ReturnPagerString.Append("<li><a    href=\"javascript:_toPage_" + JsFunctionName + "(1," + pageSize.ToString() + ");\">1…</a></li>");
            else
                ReturnPagerString.Append("<li class=\"" + (1 == pNumber ? "disabled" : "") + "\"><a  href=\"javascript:_toPage_" + JsFunctionName + "(1," + pageSize.ToString() + ");\">1</a></li>");

            //添加中间数字
            long star = pNumber - DisplaySize / 2;
            long end = pNumber + DisplaySize / 2;
            if (star < 2)
            {
                end += 2 - star;
                star = 2;
            }
            if (end > PageCount - 1)
            {
                star -= end - (PageCount - 1);
                end = PageCount - 1;
            }
            if (star < 2)
                star = 2;

            for (long i = star; i <= end; i++)
                ReturnPagerString.Append("<li class=\"" + (i == pNumber ? "disabled" : "") + "\"><a  href=\"javascript:_toPage_" + JsFunctionName + "(" + i.ToString() + "," + pageSize.ToString() + ");\">" + i.ToString() + "</a><li>");
            //添加最后页
            if (pNumber <= PageCount - (DisplaySize / 2))
                ReturnPagerString.Append("<li><a  href=\"javascript:_toPage_" + JsFunctionName + "(" + PageCount.ToString() + "," + pageSize.ToString() + ");\">…" + PageCount.ToString() + "</a></li>");
            else if (PageCount > 1)
                ReturnPagerString.Append("<li class=\"" + (PageCount == pNumber ? "disabled" : "") + "\"> <a  href=\"javascript:_toPage_" + JsFunctionName + "(" + PageCount.ToString() + "," + pageSize.ToString() + ");\">" + PageCount.ToString() + "</a><li>");
            if (pNumber < PageCount)
                ReturnPagerString.Append("<li><a  href=\"javascript:_toPage_" + JsFunctionName + "(" + (pNumber + 1).ToString() + "," + pageSize.ToString() + ");\">下页</a><li>");

            ReturnPagerString.Append("</ul></div>");


            //构造分页js函数
            ReturnPagerString.Append("<script type=\"text/javascript\">");
            ReturnPagerString.Append("function _toPage_" + JsFunctionName + "(page,size){");
            ReturnPagerString.Append("var par=\"" + query + "\";");
            ReturnPagerString.Append("window.location=\"?pageNumber=\"+page+\"&pageSize=\"+size+par;");
            ReturnPagerString.Append("}");
            ReturnPagerString.Append("</script>");
            return ReturnPagerString.ToString();
        }
    }
}
