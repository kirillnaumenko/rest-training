using System;
using RestSharp;

namespace Training.RestInfrastructure
{
	public class Actions: InfrastructureObject
	{
		public List<string> GetZipCodes()
		{
            var request = new RestRequest("/zip-codes", Method.Get);
            var response = ReadRestClient.Execute<List<string>>(request);
            var data = response.Data;

			return data;
        }
	}
}

