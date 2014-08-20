using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using VideoServiceWithOAuth2.Providers;
using Microsoft.Practices.Unity;

[assembly: OwinStartup(typeof(VideoServiceWithOAuth2.Startup))]

namespace VideoServiceWithOAuth2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                // the endpoint path
                TokenEndpointPath = new PathString("/api/token"),
             
                //Provider is a class which inherits from OAuthAuthorizationServerProvider.
                Provider = new SimpleAuthorizationServerProvider(new Repository.LINQVideoRepository()),

                AllowInsecureHttp = false
            });

            // indicate our intent to use bearer authentication
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AuthenticationType = "Bearer",
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active
            });

            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
