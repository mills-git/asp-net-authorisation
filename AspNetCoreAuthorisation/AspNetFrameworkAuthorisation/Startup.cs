using AspNetFrameworkAuthorisation.Models;
using Microsoft.Owin;
using Owin;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(AspNetFrameworkAuthorisation.Startup))]

namespace AspNetFrameworkAuthorisation {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            Debug.WriteLine("Testing that this runs");

            app.UseClaimsTransformation(incoming => {
                var dbContext = new AppDbContext();

                var user = dbContext.Users.Where(data => data.Username == "Stefan").FirstOrDefault();

                if (user == null) {
                    return Task.FromResult(incoming); 
                }

                if (user.Manager) {
                    incoming.Identities.First().AddClaim(new Claim("Manager", "True"));
                }

                if (user.Admin) {
                    incoming.Identities.First().AddClaim(new Claim("Admin", "True"));
                }

                Debug.WriteLine(user.Id);

                return Task.FromResult(incoming);
            });
        }
    }
}