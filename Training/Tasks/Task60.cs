using System;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Training.RestInfrastructure.Models;

namespace Training.Tasks
{
    [TestFixture]
	public class Task60 : BaseTest
	{
        [Test]
        public void Scenario1()
        {
            var zipToAdd = new List<string>(){
                new Random().Next(1, 10000).ToString(),
            };

            var request = new RestRequest("/zip-codes/expand", Method.Post);
            request.AddBody(zipToAdd);
            WriteRestClient.Execute(request);

            var userModel = new UserModel()
            {
                Age = new Random().Next(10, 50),
                Name = new Random().Next(1, 1000).ToString(),
                Sex = "FEMALE",
                ZipCode = zipToAdd.First()
            };

            var userRequest = new RestRequest("/users", Method.Delete);
            userRequest.AddBody(userModel);
            var userResponse = WriteRestClient.Execute(userRequest);

            Assert.AreEqual(HttpStatusCode.NoContent, userResponse.StatusCode);
        }

        [Test]
        public void Scenario2()
        {
            var zipToAdd = new List<string>(){
                new Random().Next(1, 10000).ToString(),
            };

            var request = new RestRequest("/zip-codes/expand", Method.Post);
            request.AddBody(zipToAdd);
            WriteRestClient.Execute(request);

            var userModel = new UserModel()
            {
                Name = new Random().Next(1, 1000).ToString(),
                Sex = "FEMALE",
                ZipCode = zipToAdd.First()
            };

            var userRequest = new RestRequest("/users", Method.Delete);
            userRequest.AddBody(userModel);
            var userResponse = WriteRestClient.Execute(userRequest);

            Assert.AreEqual(HttpStatusCode.NoContent, userResponse.StatusCode);
        }

        [Test]
        public void Scenario3()
        {
            var zipToAdd = new List<string>(){
                new Random().Next(1, 10000).ToString(),
            };

            var request = new RestRequest("/zip-codes/expand", Method.Post);
            request.AddBody(zipToAdd);
            WriteRestClient.Execute(request);

            var userModel = new UserModel()
            {
                Name = new Random().Next(1, 1000).ToString(),
                ZipCode = zipToAdd.First()
            };

            var userRequest = new RestRequest("/users", Method.Delete);
            userRequest.AddBody(userModel);
            var userResponse = WriteRestClient.Execute(userRequest);

            Assert.AreEqual(HttpStatusCode.Conflict, userResponse.StatusCode);
        }
    }
}

