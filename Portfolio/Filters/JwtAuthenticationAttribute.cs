using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Portfolio.Filters
{
    public class JwtAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Allow OPTIONS requests for CORS preflight
            if (actionContext.Request.Method == HttpMethod.Options)
            {
                return;
            }

            if (actionContext.Request.Headers.Authorization == null || 
                actionContext.Request.Headers.Authorization.Scheme != "Bearer")
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            var token = actionContext.Request.Headers.Authorization.Parameter;
            if (string.IsNullOrEmpty(token))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            try
            {
                var principal = ValidateToken(token);
                if (principal == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                else
                {
                    actionContext.RequestContext.Principal = principal;
                }
            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var bytes = Convert.FromBase64String(token);
                var tokenString = Encoding.UTF8.GetString(bytes);
                var parts = tokenString.Split(':');
                if (parts.Length < 2) return null;

                var username = parts[0];
                // In this simple mode, we just trust the token if it's properly formatted
                var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, "Basic");
                return new ClaimsPrincipal(identity);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
