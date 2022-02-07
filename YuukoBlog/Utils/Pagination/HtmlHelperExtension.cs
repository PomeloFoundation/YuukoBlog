﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using YuukoBlog.Utils.Pagination;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    using Microsoft.AspNetCore.Html;

    public static class PagingHtmlHelper
    {
        public static HtmlString Paging(this IHtmlHelper self, string PlainClass = "", string ActiveClass = "active", string OuterClass = "pagination", string PageNumberFormat = null, IEnumerable<string> IgnoreParam = null, string PagingInfo = "PagingInfo")
        {
            StringBuilder ret = new StringBuilder();
            if (self.ViewContext.ViewData["__Performance"] != null && Convert.ToInt32(self.ViewContext.ViewData["__Performance"]) == 1)
            {
                ret.AppendLine("<ul id=\"" + self.ViewContext.ViewData["__PagingDomId"] + "\" class=\"" + OuterClass + "\" data-plain-class=\"" + PlainClass + "\" data-active-class=\"" + ActiveClass + "\">");
                ret.AppendLine("</ul>");
                ret.AppendLine("<script>");
                ret.AppendLine("    var __contentSelector = '" + self.ViewContext.ViewData["__ContentSelector"] + "';");
                ret.AppendLine("    var __performance = '" + self.ViewContext.ViewData["__Performance"] + "';");
                ret.AppendLine("    var __pagingSelector = '#" + self.ViewContext.ViewData["__PagingDomId"] + "';");
                ret.AppendLine("    var __formSelector = '" + self.ViewContext.ViewData["__FormSelector"] + "';");
                ret.AppendLine("    var __url = '" + self.ViewContext.HttpContext.Request.Path.Value + "';");
                ret.AppendLine("    __PomeloAjaxEvents[__url] = {};");
                ret.AppendLine("</script>");
            }
            else if (self.ViewContext.ViewData["__Performance"] != null && Convert.ToInt32(self.ViewContext.ViewData["__Performance"]) == 0)
            {
                ret.AppendLine("<script>");
                ret.AppendLine("    var __contentSelector = '" + self.ViewContext.ViewData["__ContentSelector"] + "';");
                ret.AppendLine("    var __performance = '" + self.ViewContext.ViewData["__Performance"] + "';");
                ret.AppendLine("    var __pagingSelector = '#" + self.ViewContext.ViewData["__PagingDomId"] + "';");
                ret.AppendLine("    var __formSelector = '" + self.ViewContext.ViewData["__FormSelector"] + "';");
                ret.AppendLine("    var __url = '" + self.ViewContext.HttpContext.Request.Path.Value + "';");
                ret.AppendLine("    __PomeloAjaxEvents[__url] = {};");
                ret.AppendLine("</script>");
            }
            else
            {
                var httpContextAccessor = self.ViewContext.HttpContext.RequestServices.GetService<IHttpContextAccessor>();
                IDictionary<string, object> RouteValueTemplate = new Dictionary<string, object>();
                foreach (var q in httpContextAccessor.HttpContext.Request.Query)
                {
                    var str = "";
                    foreach (var s in q.Value)
                        str += s + ", ";
                    RouteValueTemplate[q.Key] = str.TrimEnd(' ').TrimEnd(',');
                }
                if (IgnoreParam != null)
                {
                    foreach (var x in IgnoreParam)
                        if (RouteValueTemplate.ContainsKey(x))
                            RouteValueTemplate.Remove(x);
                }
                var CurrentPage = httpContextAccessor.HttpContext.Request.Query.ContainsKey("p")
                    ? int.Parse(httpContextAccessor.HttpContext.Request.Query["p"].ToString())
                    : self.ViewContext.RouteData.Values["p"] != null
                    ? int.Parse(self.ViewContext.RouteData.Values["p"].ToString())
                    : 1;
                var tmp = (PagingInfo)self.ViewData[PagingInfo];
                ret.AppendLine("<ul class=\"" + OuterClass + "\">");
                RouteValueTemplate["p"] = "1";
                ret.AppendLine("<li class=\"" + PlainClass + "\">" + (self.ActionLink("«", self.ViewContext.RouteData.Values["action"].ToString(), self.ViewContext.RouteData.Values["controller"].ToString(), RouteValueTemplate, null) as TagBuilder).GetString() + "</li>");
                for (var i = tmp.Start; i <= tmp.End; i++)
                {
                    RouteValueTemplate["p"] = i.ToString();
                    if (CurrentPage == i)
                    {
                        ret.AppendLine("<li class=\"" + PlainClass + " " + ActiveClass + "\">" + (self.ActionLink(
                            PageNumberFormat == null ? i.ToString() : i.ToString(PageNumberFormat),
                            self.ViewContext.RouteData.Values["action"].ToString(),
                            self.ViewContext.RouteData.Values["controller"].ToString(),
                            RouteValueTemplate,
                            null) as TagBuilder).GetString() + "</li>");
                    }
                    else
                    {
                        ret.AppendLine("<li class=\"" + PlainClass + "\">" + (self.ActionLink(
                            PageNumberFormat == null ? i.ToString() : i.ToString(PageNumberFormat),
                            self.ViewContext.RouteData.Values["action"].ToString(),
                            self.ViewContext.RouteData.Values["controller"].ToString(),
                            RouteValueTemplate,
                            null) as TagBuilder).GetString() + "</li>");
                    }
                }
                RouteValueTemplate["p"] = tmp.PageCount.ToString();
                ret.AppendLine("<li class=\"" + PlainClass + "\">" + (self.ActionLink(
                     "»",
                     self.ViewContext.RouteData.Values["action"].ToString(),
                     self.ViewContext.RouteData.Values["controller"].ToString(),
                     RouteValueTemplate,
                     null) as TagBuilder).GetString() + "</li>");
                ret.AppendLine("</ul>");
            }
            return new HtmlString(ret.ToString());
        }
    }
}

public static class TagBuilderExtensions
{
    public static string GetString(this TagBuilder self)
    {
        using (var writer = new System.IO.StringWriter())
        {
            self.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}