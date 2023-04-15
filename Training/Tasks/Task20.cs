using System;
using System.Net;
using Microsoft.Extensions.Logging;
using NLog;
//using Microsoft.Extensions.Logging;
using NUnit.Framework;
using RestSharp;
using Training.RestInfrastructure;

namespace Training.Tasks
{
    [TestFixture]
	public class Task20 : BaseTest
	{
        [Test]
        public void Scenario1()
        {
            var request = new RestRequest("/zip-codes", Method.Get);
            var response = ReadRestClient.Execute<List<string>>(request);
            var data = response.Data;

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsNotNull(data);
        }

        [Test]
        public void Scenario2()
        {
            var zipToAdd = new List<string>(){
                "test1",
                "test2"
            };

            var request = new RestRequest("/zip-codes/expand", Method.Post);
            request.AddBody(zipToAdd);
            var response = WriteRestClient.Execute<List<string>>(request);
            var data = response.Data;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsTrue(zipToAdd.All(zip => data.Contains(zip)));
        }

        [Test]
        public void Scenario3()
        {
            var zipToAdd = new List<string>(){
                "test1",
                "test2",
                "test1",
                "test2"
            };

            var request = new RestRequest("/zip-codes/expand", Method.Post);
            request.AddBody(zipToAdd);
            var response = WriteRestClient.Execute<List<string>>(request);
            var data = response.Data;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void Scenario4()
        {
            var getZipRequest = new RestRequest("/zip-codes", Method.Get);
            var avaliableZipList = ReadRestClient.Execute<List<string>>(getZipRequest).Data;

            var request = new RestRequest("/zip-codes/expand", Method.Post);
            request.AddBody(avaliableZipList);
            var response = WriteRestClient.Execute<List<string>>(request);
            var data = response.Data;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}

