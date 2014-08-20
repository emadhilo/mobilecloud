using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using VideoServiceWithOAuth2.Repository;

namespace VideoServiceWithOAuth2.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private IVideoRepository _repository;

        public SimpleAuthorizationServerProvider(IVideoRepository repository)
        {
            _repository = repository;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId, clientSecret;

            //gets the clientid and client secret from the header.
            context.TryGetBasicCredentials(out clientId, out clientSecret);
            
            // Use this if the credentials are in the form.
            //context.TryGetFormCredentials(out clientId, out clientSecret);

            // Check if the client id/secret is valid
            if (_repository.AuthenticateClient(clientId, clientSecret))
                context.Validated();
            else
            {
                context.SetError("Incorrect Client"); 
                context.Rejected();
            }

            return Task.FromResult(0);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Check if the passed username and password are correct.
            if (_repository.Authenticate(context.UserName, context.Password))
            {
                var roles = _repository.UserRoles(context.UserName);
                
                // Create a ClaimsIdentity based on the roles the user has access to.
                System.Security.Claims.ClaimsIdentity ci = new System.Security.Claims.ClaimsIdentity(context.Options.AuthenticationType);
                foreach (var role in roles)
                {
                    ci.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                context.Validated(ci);
            }
            else
            {
                context.SetError("Incorrect Credentials");
                context.Rejected();
            }
            return Task.FromResult(0);
        }

    }
}