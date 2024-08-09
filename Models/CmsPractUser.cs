using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace cms_pract.Models
{
    public class CmsPractUser : IdentityUser
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public ICollection<ContactsEmail> ContactsEmail { get; set; } = new List<ContactsEmail>();
    }
}
