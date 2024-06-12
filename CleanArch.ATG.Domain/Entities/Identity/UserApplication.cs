using Microsoft.AspNetCore.Identity;

namespace CleanArch.ATG.Domain.Entities.Identity
{
    public class UserApplication : IdentityUser<Guid>
    {
        public virtual byte LockoutEnabled { get; set; }
        [PersonalData]
        public virtual byte EmailConfirmed { get; set; }
        [PersonalData]
        public virtual byte PhoneNumberConfirmed { get; set; }
        [PersonalData]
        public virtual byte TwoFactorEnabled { get; set; }
        public string Password { get; set; }
    }
}
