using IKnowTechnology.CORE.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IKnowTechnology.UI.Utils
{
    internal class ClaimService
    {
        private readonly UserManager<User> userManager;

        public ClaimService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<bool> AddClaimsToUser(User user)
        {
            await userManager.AddClaimAsync(user, new Claim("userId", user.Id));
            await userManager.AddClaimAsync(user, new Claim("firstName", user.FirstName));
            await userManager.AddClaimAsync(user, new Claim("photoPath", user.ImagePath));

            return true;
        }

        public async Task<bool> RemoveClaims(User user)
        {
            await userManager.RemoveClaimsAsync(user, await userManager.GetClaimsAsync(user));
            return true;
        }

        public async Task<bool> ReplaceClaims(User user)
        {
            await userManager.RemoveClaimsAsync(user, await userManager.GetClaimsAsync(user));
            await userManager.AddClaimAsync(user, new Claim("userId", user.Id));
            await userManager.AddClaimAsync(user, new Claim("firstName", user.FirstName));
            await userManager.AddClaimAsync(user, new Claim("photoPath", user.ImagePath));
            return true;
        }
    }
}