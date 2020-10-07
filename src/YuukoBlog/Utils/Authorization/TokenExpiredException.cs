using System;
using System.Diagnostics.CodeAnalysis;

namespace YuukoBlog.Utils.Authorization
{
    [ExcludeFromCodeCoverage]
    public class TokenExpiredException : Exception
    {
        public string Token { get; private set; }

        public TokenExpiredException(string token) : base($"Token {token} is already expired.")
        {
            this.Token = token;
        }
    }
}
