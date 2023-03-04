using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace PaymentGateway.API.Features.Authentication
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <returns>The requested access token</returns>
        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            //To Do: handle any errors(taken care of by the global exception handler)
            var client = new HttpClient();

            var discoveryDocument = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                //this is the base address of the identity server project
                Address = "https://localhost:5001",
                Policy =
                {
                    RequireKeySet = false
                }
            });

            //all values are from the Identity server project
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "pg-client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
                Scope = "pg-api"
            });
            //Console.WriteLine($"Token : {tokenResponse.AccessToken}");
            return Ok(tokenResponse.AccessToken);
        }
    }
}
