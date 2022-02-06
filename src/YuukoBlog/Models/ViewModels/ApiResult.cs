using System.Collections.Generic;

namespace YuukoBlog.Models.ViewModels
{
    public class ApiResult<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ApiResult : ApiResult<object>
    { }

    public class PagedApiResult<T> : ApiResult<IEnumerable<T>>
    {
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

    public class PagedApiResult : PagedApiResult<object>
    { }
}
