using System;
using RestSharp;
using RestSharp.Authenticators;

namespace Training.RestInfrastructure
{
	public class ReadScopeRestClient
    {
        private static readonly RestClient instance;

        static ReadScopeRestClient()
        {
            var options = new RestClientOptions("http://localhost:8082/")
            {
                Authenticator = new ReadScopeAuthenticator()
            };

            instance = new RestClient(options);
        }

        public static RestClient Instance
        {
            get { return instance; }
        }
    }
}

