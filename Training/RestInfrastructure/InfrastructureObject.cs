using System;
using RestSharp;

namespace Training.RestInfrastructure
{
	public class InfrastructureObject
	{
        protected RestClient ReadRestClient = ReadScopeRestClient.Instance;
        protected RestClient WriteRestClient = WriteScopeRestClient.Instance;
    }
}

