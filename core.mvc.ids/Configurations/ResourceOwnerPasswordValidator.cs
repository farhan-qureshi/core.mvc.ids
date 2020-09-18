using IdentityServer4.Validation;
using System.Threading.Tasks;

namespace core.mvc.ids
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;
        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _userRepository.GetUserAsync(context.UserName, context.Password);
             
            if(user == null || user.Result == null)
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidRequest, "Username or password is incorrect.");
                return Task.FromResult(0);
            }

            context.Result = new GrantValidationResult(user.Id.ToString(), "password");
            return Task.FromResult(0);
        }
    }
}
