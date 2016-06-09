namespace YuukoBlog.Extensions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Pomelo.AspNetCore.Extensions.BlobStorage;

    public class SessionUploadAuthorization : IUploadAuthorizationProvider
    {
        private ISession Session;
        public SessionUploadAuthorization(IHttpContextAccessor accessor)
        {
            Session = accessor.HttpContext.Session;
        }

        public bool IsAbleToUpload()
        {
            var val = Session.GetString("Admin");
            if (val == "true")
                return true;
            return false;
        }
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    using Pomelo.AspNetCore.Extensions.BlobStorage;
    using YuukoBlog.Extensions;

    public static class SignedUserUploadAuthorizationProviderServiceCollectionExtensions
    {
        public static IBlobStorageBuilder AddSessionUploadAuthorization(this IBlobStorageBuilder self)
        {
            self.Services.AddSingleton<IUploadAuthorizationProvider, SessionUploadAuthorization>();
            return self;
        }
    }
}