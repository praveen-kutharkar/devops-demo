using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace devops_demo
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("HelloWorldFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var currentTime= DateTime.Now;
            var messageString = "Welcome to Azure Functions!, run-1 CICD test-1";
            var returnmessage= "Current Server Time is: " + currentTime.ToString()+ "\n" + messageString;
            return new OkObjectResult(returnmessage);
        }
    }
}
