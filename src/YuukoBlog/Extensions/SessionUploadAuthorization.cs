using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.AspNetCore.Extensions.BlobStorage;
using YuukoBlog.Extensions;

namespace YuukoBlog.Extensions
{
    public class SessionUploadAuthorization : IBlobUploadAuthorizationProvider
    {
        private IServiceProvider services;

        public SessionUploadAuthorization(IServiceProvider provider)
        {
            services = provider;
        }

        public bool IsAbleToUpload()
        {
            var val = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session.GetString("Admin");
            if (val == "true")
                return true;
            return false;
        }
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SignedUserUploadAuthorizationProviderServiceCollectionExtensions
    {
        public static IBlobStorageBuilder AddSessionUploadAuthorization(this IBlobStorageBuilder self)
        {
            self.Services.AddSingleton<IBlobUploadAuthorizationProvider, SessionUploadAuthorization>();
            return self;
        }
    }
}