using System;
using System.Linq;
using System.Collections.Generic;

namespace YuukoBlog.Utils.Pagination
{
    public static class Paging
    {
        public static PagingInfo GetPagingInfo<T>(ref IEnumerable<T> src, int PageSize = 50, int Page = 1)
        {
            var ret = new PagingInfo();
            ret.Count = src.LongCount();
            ret.PageCount = Convert.ToInt32((src.LongCount() + PageSize - 1) / PageSize);
            ret.PageSize = PageSize;
            ret.Start = Page - 5;
            if (ret.Start < 1)
                ret.Start = 1;
            ret.End = (ret.Start + 9) > ret.PageCount ? ret.PageCount : (ret.Start + 9);
            if (ret.End - ret.Start + 1 < 10)
                ret.Start -= 4;
            if (ret.Start < 1)
                ret.Start = 1;
            if (ret.End < ret.Start) ret.End = ret.Start;
            if (ret.PageCount == 0) ret.PageCount = 1;
            return ret;
        }

        public static void PlainDivide<T>(ref IEnumerable<T> src, int PageSize = 50, int Page = 1)
        {
            src = src.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
        }

        public static PagingInfo Divide<T>(ref IEnumerable<T> src, int PageSize = 50, int Page = 1)
        {
            var ret = new PagingInfo();
            ret.Count = src.LongCount();
            ret.PageCount = Convert.ToInt32((src.LongCount() + PageSize - 1) / PageSize);
            ret.PageSize = PageSize;
            ret.Start = Page - 5;
            if (ret.Start < 1)
                ret.Start = 1;
            ret.End = (ret.Start + 9) > ret.PageCount ? ret.PageCount : (ret.Start + 9);
            if (ret.End - ret.Start + 1 < 10)
                ret.Start -= 4;
            if (ret.Start < 1)
                ret.Start = 1;
            if (ret.End < ret.Start) ret.End = ret.Start;
            if (ret.PageCount == 0) ret.PageCount = 1;
            src = src.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
            return ret;
        }
    }
}
