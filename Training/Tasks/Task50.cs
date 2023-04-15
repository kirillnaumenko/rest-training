using System;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Training.RestInfrastructure.Models;

namespace Training.Tasks
{
	public class Task50 : BaseTest
	{
        [Test]
        public void Scenario1()
        {
            // Add new zip code and new user
            var zipToAdd = new List<string>(){
                new Random().Next(1, 10000).ToString(),
            };

            var request = new RestRequest("/zip-codes/expand", Method.Post);
            request.AddBody(zipToAdd);
            WriteRestClient.Execute(request);

            var userToChange = new UserModel()
            {
                Age = new Random().Next(10, 70),
                Name = new Random().Next(1, 1000).ToString(),
                Sex = "FEMALE",
                ZipCode = zipToAdd.First()
            };

            var userNewValues = new UserModel()
            {
                Age = new Random().Next(10, 70),
                Name = userToChange.Name,
                Sex = userToChange.Sex,
                ZipCode = userToChange.ZipCode
            };

            var updateModel = new UpdateUserModel()
            {
                userNewValues = userNewValues,
                userToChange = userToChange
            };

            var userRequest = new RestRequest("/users", Method.Post);
            userRequest.AddBody(userToChange);
            WriteRestClient.Execute(userRequest);

            // Update existing user
            var updateRequest = new RestRequest("/users", Method.Put);
            updateRequest.AddBody(updateModel);    
            var updateResponse = WriteRestClient.Execute(updateRequest);

            Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
        }

        [Test]
        public void Scenario2()
        {
            // Add new zip code and new user
            var zipToAdd = new List<string>(){
                new Random().Next(1, 10000).ToString(),
            };

            var request = new RestRequest("/zip-codes/expand", Method.Post);
            request.AddBody(zipToAdd);
            WriteRestClient.Execute(request);

            var userToChange = new UserModel()
            {
                Age = new Random().Next(10, 70),
                Name = new Random().Next(1, 1000).ToString(),
                Sex = "FEMALE",
                ZipCode = zipToAdd.First()
            };

            var userNewValues = new UserModel()
            {
                Age = new Random().Next(10, 70),
                Name = userToChange.Name,
                Sex = userToChange.Sex,
                ZipCode = "Incorrect zip code"
            };

            var updateModel = new UpdateUserModel()
            {
                userNewValues = userNewValues,
                userToChange = userToChange
            };

            var userRequest = new RestRequest("/users", Method.Post);
            userRequest.AddBody(userToChange);
            WriteRestClient.Execute(userRequest);

            // Update existing user
            var updateRequest = new RestRequest("/users", Method.Put);
            updateRequest.AddBody(updateModel);
            var updateResponse = WriteRestClient.Execute(updateRequest);

            Assert.AreEqual(HttpStatusCode.FailedDependency, updateResponse.StatusCode);
        }

        [Test]
        public void Scenario3()
        {
            // Add new zip code and new user
            var zipToAdd = new List<string>(){
                new Random().Next(1, 10000).ToString(),
            };

            var request = new RestRequest("/zip-codes/expand", Method.Post);
            request.AddBody(zipToAdd);
            WriteRestClient.Execute(request);

            var userToChange = new UserModel()
            {
                Age = new Random().Next(10, 70),
                Name = new Random().Next(1, 1000).ToString(),
                Sex = "FEMALE",
                ZipCode = zipToAdd.First()
            };

            var userNewValues = new UserModel()
            {
                Age = new Random().Next(10, 70),
                ZipCode = userToChange.ZipCode
            };

            var updateModel = new UpdateUserModel()
            {
                userNewValues = userNewValues,
                userToChange = userToChange
            };

            var userRequest = new RestRequest("/users", Method.Post);
            userRequest.AddBody(userToChange);
            WriteRestClient.Execute(userRequest);

            // Update existing user
            var updateRequest = new RestRequest("/users", Method.Put);
            updateRequest.AddBody(updateModel);
            var updateResponse = WriteRestClient.Execute(updateRequest);

            Assert.AreEqual(HttpStatusCode.Conflict, updateResponse.StatusCode);
        }
    }
}

