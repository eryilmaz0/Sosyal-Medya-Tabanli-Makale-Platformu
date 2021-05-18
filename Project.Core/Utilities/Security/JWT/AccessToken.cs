using System;

namespace Project.Core.Utilities.Security.JWT
{
    //HELPER CLASS
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string UserRole { get; set; }
        public DateTime Created { get; set; }

    }
}