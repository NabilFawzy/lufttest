using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;

namespace IdentityServer.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly TestUserStore _users;

        public CustomProfileService(TestUserStore users)
        {
            _users = users;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _users.FindBySubjectId(context.Subject.GetSubjectId());

            if (user != null)
            {
                // Include only the requested claim types
                var claims = user.Claims
                    .Where(claim => context.RequestedClaimTypes.Contains(claim.Type))
                    .ToList();

                context.IssuedClaims = claims;
            }

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _users.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = user != null;
            return Task.CompletedTask;
        }
    }
}
