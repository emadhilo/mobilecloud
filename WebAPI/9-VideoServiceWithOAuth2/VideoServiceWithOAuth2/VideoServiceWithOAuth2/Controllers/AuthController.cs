using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using VideoServiceWithOAuth2.Models;
using VideoServiceWithOAuth2.Filters;

namespace VideoServiceWithOAuth2.Controllers
{
    public class AuthController : ApiController
    {
        //Path will be: /api/auth/Login
        [HttpPost]
        [RequireHttps]
        public HttpResponseMessage Login(Login userlogin)
        {
            if (Membership.ValidateUser(userlogin.Username, userlogin.password))
            {
                FormsAuthentication.SetAuthCookie(userlogin.Username, false);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }

            return this.Request.CreateResponse(HttpStatusCode.Unauthorized, userlogin);
        }

        //Path will be: /api/auth/logout
        [HttpGet]
        [RequireHttps]
        public HttpResponseMessage Logout()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return this.Request.CreateResponse(HttpStatusCode.OK, "Logged out successfully");
            }
            else
            {
                return this.Request.CreateResponse(HttpStatusCode.OK, "Not logged in");
            }
        }

    }
}
