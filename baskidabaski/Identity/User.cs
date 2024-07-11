using Microsoft.AspNetCore.Identity;

namespace baskidabaski.Identity
{
    public class User : IdentityUser<string>
    {
        public string FirtsName { get; set; }
        public string LastName { get; set; }
    }
}
