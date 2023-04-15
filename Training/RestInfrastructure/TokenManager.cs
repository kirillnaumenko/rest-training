using System;
using System.Net.Http.Headers;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using Training.RestInfrastructure.Models;

namespace Training.RestInfrastructure
{
	public class TokenManager
	{
        private static TokenManager instance;
        private readonly HttpClient httpClient;
        private readonly string baseUrl;
        private readonly string userName;
        private readonly string password;
        private string readToken;
        private string writeToken;

        private TokenManager()
        {
            httpClient = new HttpClient();
            baseUrl = "http://localhost:8082/";
            userName = "0oa157tvtugfFXEhU4x7";
            password = "X7eBCXqlFC7x-mjxG5H91IRv_Bqe1oq7ZwXNA8aq";
        }

        public static TokenManager Instance()
        {
            return instance ??= new TokenManager();
        }

        public string GetReadToken()
        {
            return readToken ??= GetAccessToken("read");
        }

        public string GetWriteToken()
        {
            return readToken ??= GetAccessToken("write");
        }

        private string GetAccessToken(string scope)
        {
            var tokenUrl = $"{baseUrl}/oauth/token";
            var credentials = $"{userName}:{password}";
            var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);
            request.Content = new StringContent($"grant_type=client_credentials&scope={scope}", Encoding.UTF8, "application/x-www-form-urlencoded");

            //var request = new RestRequest("oauth/token", Method.Post);
            //request.AddQueryParameter("grant_type", "client_credentials");
            //request.AddQueryParameter("scope", scope);
            //request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{userName}:{password}"))}");

            var response = httpClient.Execute<TokenResponseModel>(request);

            return response.Data.AccessToken;
        }
    }
}

