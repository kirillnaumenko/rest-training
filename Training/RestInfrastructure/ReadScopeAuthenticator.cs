using RestSharp;
using RestSharp.Authenticators;
using Training.RestInfrastructure.Models;

namespace Training.RestInfrastructure
{
    public class ReadScopeAuthenticator : AuthenticatorBase
    {
        readonly string _baseUrl;
        readonly string _clientId;
        readonly string _clientSecret;

        public ReadScopeAuthenticator() : base("")
        {
            _baseUrl = "http://localhost:8082/";
            _clientId = "0oa157tvtugfFXEhU4x7";
            _clientSecret = "X7eBCXqlFC7x-mjxG5H91IRv_Bqe1oq7ZwXNA8aq";
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            Token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, Token);
        }

        async Task<string> GetToken()
        {
            var options = new RestClientOptions(_baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(_clientId, _clientSecret),
            };
            using var client = new RestClient(options);

            var request = new RestRequest("oauth/token")
                .AddQueryParameter("grant_type", "client_credentials")
                .AddQueryParameter("scope", "read");
            var response = await client.PostAsync<TokenResponseModel>(request);
            return $"{response!.TokenType} {response!.AccessToken}";
        }
    }
}

