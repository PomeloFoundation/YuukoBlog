using System.Collections.Generic;
using YuukoBlog.Models;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class TagHelper
    {
        public static string TagSerialize(this IHtmlHelper self, IEnumerable<PostTag> Tags)
        {
            var ret = "";
            foreach (var t in Tags)
                ret += t.Tag + ", ";
            return ret.TrimEnd(' ').TrimEnd(',');
        }
    }
}
