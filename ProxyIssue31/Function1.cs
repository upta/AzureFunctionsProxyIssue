using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ProxyIssue31
{
    public class Function1
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Function1(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [FunctionName(nameof(TheRoute))]
        public string TheRoute
        (
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "the-route")] HttpRequest req
        )
        {
            _httpContextAccessor.HttpContext.Response.Redirect("http://www.redirects-only-without-proxy.com");

            return "returns-if-proxied";
        }
    }
}
