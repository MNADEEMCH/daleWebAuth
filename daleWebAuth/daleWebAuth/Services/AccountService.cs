using daleWebAuth.helpers;
using IdentityModel.Client;
using PCLCrypto;
using static PCLCrypto.WinRTCrypto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static daleWebAuth.helpers.Common;
using IdentityModel;

namespace daleWebAuth.Services
{
    public class AccountService : IAccountService
    {
        private string _codeVerifier;
        public string CreateAuthorizationRequest()
        {
            // Create URI to authorization endpoint
            var authorizeRequest = new AuthorizeRequest("https://auth-dev.mdapps-staging.com/connect/authorize");

            // Dictionary with values for the authorize request
            var dic = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", "XamainAuthDemoApp" },
                { "client_secret", "secret" },
                { "redirect_uri", "daleWebAuth://" },
                { "scope", "openid email profile apiApp" },
                //{ "grant_type", "authorization_code"},
                { "nonce", Guid.NewGuid().ToString("N") },
                { "code_challenge", CreateCodeChallenge() },
                { "code_challenge_method", "S256" }
            };

            // Add CSRF token to protect against cross-site request forgery attacks.
            var currentCSRFToken = Guid.NewGuid().ToString("N");
            dic.Add("state", currentCSRFToken);

            //dic.Add("acr_values", "idp:Google");

            var authorizeUri = authorizeRequest.Create(dic);
            return authorizeUri;
        }

        private string CreateCodeChallenge()
        {
            _codeVerifier = RandomNumberGenerator.CreateUniqueId();
            var sha256 = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha256);
            var challengeBuffer = sha256.HashData(CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes(_codeVerifier)));
            byte[] challengeBytes;
            CryptographicBuffer.CopyToByteArray(challengeBuffer, out challengeBytes);
            return Base64Url.Encode(challengeBytes);
        }

        public async Task<Tuple<Common.CallStatus, TokenResponse>> ExchangeCodeForToken(string code)
        {
            var client = new HttpClient();
            var response = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
            {
                Address = "https://auth-dev.mdapps-staging.com/",

                ClientId = "XamainAuthDemoApp",
                ClientSecret = "secret",

                Code = code,
                RedirectUri = "https://www.lifemiles.com/",

                // optional PKCE parameter
                CodeVerifier = "jkd783yhu"
            });

            if (!response.IsError)
            {
                return Tuple.Create(CallStatus.Success, response);
            }

            return Tuple.Create(CallStatus.Error, response);
        }
    }
}
