using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using System.Text;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        /// <summary>
        /// 创建分页链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageInfo">(当前)页面信息</param>
        /// <param name="pageUrl">生成链接字符串的委托</param>
        /// <returns></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tagBuilder = new TagBuilder("a");
                tagBuilder.MergeAttribute("href", pageUrl(i));
                tagBuilder.InnerHtml = i.ToString();
                if(pageInfo.CurrentPage == i)
                {
                    tagBuilder.AddCssClass("selected");
                    tagBuilder.AddCssClass("btn-primary");
                }
                tagBuilder.AddCssClass("btn btn-default");
                
                stringBuilder.Append(tagBuilder);
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}