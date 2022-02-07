using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuukoBlog.Models.ViewModels;

namespace YuukoBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected ApiResult ApiResult(int code = 200, string message = null)
        {
            message = message ?? "Succeeded";
            Response.StatusCode = code;
            return new ApiResult
            {
                Code = code,
                Message = message
            };
        }

        protected ApiResult<T> ApiResult<T>(T data)
        {
            return new ApiResult<T>
            {
                Code = 200,
                Message = "Succeeded",
                Data = data
            };
        }

        protected ApiResult<T> ApiResult<T>(int code = 200, string message = null)
        {
            message = message ?? "Succeeded";
            Response.StatusCode = code;
            return new ApiResult<T>
            {
                Code = code,
                Message = message
            };
        }

        protected PagedApiResult PagedApiResult(int code = 200, string message = null)
        {
            message = message ?? "Succeeded";
            Response.StatusCode = code;
            return new PagedApiResult
            {
                Code = code,
                Message = message
            };
        }

        protected async ValueTask<PagedApiResult<T>> PagedApiResultAsync<T>(
            IQueryable<T> data,
            int currentPage,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var totalRecords = await data.CountAsync(cancellationToken);
            data = data.Skip((currentPage - 1) * pageSize).Take(pageSize);

            return new PagedApiResult<T>
            {
                Code = 200,
                Message = "Succeeded",
                Data = await data.ToListAsync(cancellationToken),
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (totalRecords + pageSize - 1) / pageSize
            };
        }

        protected PagedApiResult<T> PagedApiResult<T>(int code = 200, string message = null)
        {
            message = message ?? "Succeeded";
            Response.StatusCode = code;
            return new PagedApiResult<T>
            {
                Code = code,
                Message = message
            };
        }
    }
}
