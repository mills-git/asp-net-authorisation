using System.Linq;
using System.Security.Claims;

namespace AspNetFrameworkAuthorisation
{
	public class AuthorisationManager: ClaimsAuthorizationManager
	{
		public override bool CheckAccess(AuthorizationContext context) {
			string resource = context.Resource.First().Value; 

			if (resource.Equals("Admin")) {
				return context.Principal.HasClaim("Admin", "True"); 
			}

			return false; 
		}
	}
}