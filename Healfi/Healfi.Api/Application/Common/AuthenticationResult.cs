using System;

namespace Healfi.Api.Application.Common
{
    public class AuthenticationResult
    {
        public string AccessToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}