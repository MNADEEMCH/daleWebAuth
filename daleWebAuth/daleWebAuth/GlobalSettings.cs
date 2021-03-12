
namespace daleWebAuth
{
    public class GlobalSettings
    {

        public GlobalSettings()
        {
            BaseIdentityEndpoint = "https://auth-dev.mdapps-staging.com";
            BaseApiEndpoint = "https://core-api-dev.mdapps-staging.com";

            ClientId = "XamainAuthDemoApp";
            ClientSecret = "secret";
            Callback = "dalewebauth://";


            var connectBaseEndpoint = $"{BaseIdentityEndpoint}/connect";
            AuthorizeEndpoint = $"{connectBaseEndpoint}/authorize";
            TokenEndpoint = $"{connectBaseEndpoint}/token";
        }


        public static GlobalSettings Instance { get; } = new GlobalSettings();
        public string BaseIdentityEndpoint { get; set; }
        public string BaseApiEndpoint { get; set; }

        public string AuthorizeEndpoint { get; set; }
        public string TokenEndpoint { get; set; }

        public string ClientId { get; internal set; }
        public string ClientSecret { get; internal set; }
        public string Callback { get; internal set; }
    }
}