using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace To_Do.API.Attributes;

/// <summary>
/// https://learn.microsoft.com/en-us/aspnet/web-api/overview/security/working-with-ssl-in-web-api
/// https://www.c-sharpcorner.com/article/how-to-enable-https-in-asp-net-web-api/
/// </summary>
public class RequireHttpsAttribute : AuthorizationFilterAttribute
{
    public override void OnAuthorization(HttpActionContext actionContext)
    {
        if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
        {
            actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
            {
                ReasonPhrase = "HTTPS Required"
            };
        }
        else
        {
            base.OnAuthorization(actionContext);
        }
    }
}
