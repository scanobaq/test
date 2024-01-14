using test.domain.Entities;

namespace test.app.DTOs;
public class UserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}



