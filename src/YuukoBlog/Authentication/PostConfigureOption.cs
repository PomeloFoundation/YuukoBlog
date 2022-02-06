using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace YuukoBlog.Authentication
{
    [ExcludeFromCodeCoverage]
    public class TokenPostConfigureOptions : IPostConfigureOptions<TokenOptions>
    {
        public void PostConfigure(string name, TokenOptions options)
        {
        }
    }
}