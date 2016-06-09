using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuukoBlog.Models;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class RawHelper
    {
        public static bool IsRaw(this IHtmlHelper self)
        {
            if (self.ViewContext.HttpContext.Request.Query["raw"] == "true")
                return true;
            else
                return false;
        }
    }
}
