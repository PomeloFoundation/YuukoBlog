using System;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace YuukoBlog.Utils.Authorization
{
    public class Antifogery : IAntiforgeryAdditionalDataProvider
    {
        public string GetAdditionalData(HttpContext context)
        {
            return "";
        }

        public bool ValidateAdditionalData(HttpContext context, string additionalData)
        {
            return true;
        }
    }
}
