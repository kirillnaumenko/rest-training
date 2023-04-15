using System;
using RestSharp;

namespace Training.RestInfrastructure
{
	public class WriteScopeRestClient
	{
        private static readonly RestClient instance;

        static WriteScopeRestClient()
        {
            var options = new RestClientOptions("http://localhost:8082/")
            {
                Authenticator = new WriteScopeAuthenticator()
            };

            instance = new RestClient(options);
        }

        public static RestClient Instance
        {
            get { return instance; }
        }
    }
}

