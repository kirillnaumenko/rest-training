using System;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Training.RestInfrastructure.Models;
using System.Text.Json;

namespace Training.Tasks
{
	public class Task70 : BaseTest
	{
        [Test]
        public void Scenario1()
        {
            var usersToAdd = new List<UserModel>();
            for (int i = 0; i < 2; i++)
            {
                var userModel = new UserModel()
                {
                    Age = new Random().Next(10, 50),
                    Name = new Random().Next(1, 1000).ToString(),
                    Sex = "FEMALE",
                };

                usersToAdd.Add(userModel);
            }

            var json = JsonSerializer.Serialize(usersToAdd);
            File.WriteAllText($"{Directory.GetCurrentDirectory()}/JsonWithUsers.json", json);
            
            var userRequest = new RestRequest("/users/upload", Method.Post);
            userRequest.AddFile("file", $"{Directory.GetCurrentDirectory()}/JsonWithUsers.json");
            var userResponse = WriteRestClient.Execute(userRequest);

            Assert.AreEqual(HttpStatusCode.Created, userResponse.StatusCode);
        }

        [Test]
        public void Scenario2()
        {
            // Preconditions
            // Add zip and user
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
            WriteRestClient.Execute(userRequest);

            // Upload json with users that have used zip from previous steps
            var usersToAdd = new List<UserModel>();
            for (int i = 0; i < 2; i++)
            {
                userModel = new UserModel()
                {
                    Age = new Random().Next(10, 50),
                    Name = new Random().Next(1, 100000).ToString(),
                    Sex = "MALE",
                    ZipCode = zipToAdd.First(),
                };

                usersToAdd.Add(userModel);
            }

            var json = JsonSerializer.Serialize(usersToAdd);
            File.WriteAllText($"{Directory.GetCurrentDirectory()}/JsonWithUsers.json", json);

            var uploadRequest = new RestRequest("/users/upload", Method.Post);
            uploadRequest.AddFile("file", $"{Directory.GetCurrentDirectory()}/JsonWithUsers.json");
            var uploadResponse = WriteRestClient.Execute(userRequest);

            Assert.AreEqual(HttpStatusCode.FailedDependency, uploadResponse.StatusCode);
        }

        [Test]
        public void Scenario3()
        {
            var usersToAdd = new List<UserModel>();
            for (int i = 0; i < 2; i++)
            {
                var userModel = new UserModel()
                {
                    Name = new Random().Next(1, 1000).ToString(),
                    //Sex = "FEMALE",
                };

                usersToAdd.Add(userModel);
            }

            var json = JsonSerializer.Serialize(usersToAdd);
            File.WriteAllText($"{Directory.GetCurrentDirectory()}/JsonWithUsers.json", json);

            var userRequest = new RestRequest("/users/upload", Method.Post);
            userRequest.AddFile("file", $"{Directory.GetCurrentDirectory()}/JsonWithUsers.json");
            var userResponse = WriteRestClient.Execute(userRequest);

            Assert.AreEqual(HttpStatusCode.Conflict, userResponse.StatusCode);
        }
    }
}

