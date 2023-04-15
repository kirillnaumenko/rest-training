using System;
using NUnit.Framework;
using System.Net;
using RestSharp;
using Training.RestInfrastructure.Models;
using System.Linq;

namespace Training.Tasks
{
    [TestFixture]
	public class Task30 : BaseTest
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

            var userRequest = new RestRequest("/users", Method.Post);
            userRequest.AddBody(userModel);
            var userResponse = WriteRestClient.Execute(userRequest);

            var getZipRequest = new RestRequest("/zip-codes", Method.Get);
            var avaliableZipList = ReadRestClient.Execute<List<string>>(getZipRequest).Data;

            Assert.AreEqual(HttpStatusCode.Created, userResponse.StatusCode);
            Assert.IsFalse(avaliableZipList.Contains(zipToAdd.First()));
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
                Age = new Random().Next(10, 50),
                Name = new Random().Next(1, 1000).ToString(),
                Sex = "FEMALE",
                ZipCode = zipToAdd.First()
            };

            var userRequest = new RestRequest("/users", Method.Post);
            userRequest.AddBody(userModel);
            var userResponse = WriteRestClient.Execute(userRequest);

            Assert.AreEqual(HttpStatusCode.Created, userResponse.StatusCode);
        }

        [Test]
        public void Scenario3()
        {
            var userModel = new UserModel()
            {
                Age = new Random().Next(10, 50),
                Name = new Random().Next(1, 1000).ToString(),
                Sex = "FEMALE",
                ZipCode = "Not avaliable zip code"
            };

            var userRequest = new RestRequest("/users", Method.Post);
            userRequest.AddBody(userModel);
            var userResponse = WriteRestClient.Execute(userRequest);

            Assert.AreEqual(HttpStatusCode.FailedDependency, userResponse.StatusCode);
        }

        [Test]
        public void Scenario4()
        {
            var userModel = new UserModel()
            {
                Age = new Random().Next(10, 50),
                Name = new Random().Next(1, 1000).ToString(),
                Sex = "FEMALE",
            };

            var userRequest = new RestRequest("/users", Method.Post);
            userRequest.AddBody(userModel);
            var userResponse = WriteRestClient.Execute(userRequest);
            userResponse = WriteRestClient.Execute(userRequest);

            Assert.AreEqual(HttpStatusCode.BadRequest, userResponse.StatusCode);
        }
    }
}

