using Microsoft.AspNetCore.Identity;

namespace test.domain.Entities;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Company Company { get; set; }
        
    }
