using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sportify_back.Identity // Ajusta el namespace si tienes otra estructura
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<IdentityUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor) { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            // Agregar claim personalizado según el perfil del usuario
            if (user.Email == "admin@sportify.com") // Ejemplo de verificación de perfil
            {
                identity.AddClaim(new Claim("Profile", "Administrador"));
            }
            else
            {
                identity.AddClaim(new Claim("Profile", "Usuario Final"));
            }

            return identity;
        }
    }
}
