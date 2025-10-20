using FlyingProject.Project.core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FlyingProject.Project.core.ServiceContrect
{
    public interface IJwtService
    {
        Task<string> CreateToken(AppUser appUser,UserManager<AppUser> userManager);
    }
}
