using System;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Training.RestInfrastructure.Models;

namespace Training.Tasks
{
    [TestFixture]
	public class Task40 : BaseTest
	{
        [Test]
        public void Scenario1()
        {
            var userRequest = new RestRequest("/users", Method.Get);
            var userResponse = ReadRestClient.Execute<List<UserModel>>(userRequest);
            var data = userResponse.Data;

            Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);
            Assert.IsNotNull(data);
        }

        [Test]
        public void Scenario2()
        {
            var userRequest = new RestRequest("/users", Method.Get);
            userRequest.AddParameter("olderThan", 30);
            var userResponse = ReadRestClient.Execute<List<UserModel>>(userRequest);
            var data = userResponse.Data;

            Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);

            foreach (var user in data)
            {
                Assert.IsTrue(user.Age >= 30);
            }
        }

        [Test]
        public void Scenario3()
        {
            var userRequest = new RestRequest("/users", Method.Get);
            userRequest.AddParameter("youngerThan", 30);
            var userResponse = ReadRestClient.Execute<List<UserModel>>(userRequest);
            var data = userResponse.Data;

            Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);

            foreach (var user in data)
            {
                Assert.IsTrue(user.Age <= 30);
            }
        }

        [Test]
        public void Scenario4()
        {
            var userRequest = new RestRequest("/users", Method.Get);
            userRequest.AddParameter("sex", "FEMALE");
            var userResponse = ReadRestClient.Execute<List<UserModel>>(userRequest);
            var data = userResponse.Data;

            Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);

            foreach (var user in data)
            {
                Assert.AreEqual("FEMALE", user.Sex);
            }
        }
    }
}

