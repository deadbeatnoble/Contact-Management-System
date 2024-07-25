using Microsoft.AspNetCore.Identity;

namespace cms_pract.Models
{
    public class CmsPractUser : IdentityUser
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
    }
}
