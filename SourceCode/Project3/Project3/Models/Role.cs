using Microsoft.AspNetCore.Identity;

namespace Project3.Models
{
    public enum Roles
    {
        ADMIN, MANAGER, ADVISOR, USER
    }
    public class Role : IdentityRole<int>
    {
    }
}
