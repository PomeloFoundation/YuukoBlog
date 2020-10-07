using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace YuukoBlog.Utils.Authorization
{
    [ExcludeFromCodeCoverage]
    public class TokenPostConfigureOptions : IPostConfigureOptions<TokenOptions>
    {
        public void PostConfigure(string name, TokenOptions options)
        {
        }
    }
}