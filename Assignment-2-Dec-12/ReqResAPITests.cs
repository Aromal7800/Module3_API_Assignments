using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_Dec_12

{
    [TestFixture]
    internal class ReqResAPITests
    {

        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";
        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        public void GetSingleUser()
        {

            var req = new RestRequest("posts/1", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var userdata = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(userdata);
            Assert.That(userdata.Id, Is.EqualTo(1));
            Console.WriteLine("user :" + response.Content);

        }
        [Test]
        public void CreateUser()
        {

            var req = new RestRequest("posts", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { title = "How to kill a Mocking Bird", body = "Shoot It " });

            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
            // Assert.IsNotEmpty(user.Email);

        }

        [Test]
        public void UpdateUser()
        {

            var req = new RestRequest("posts/1", Method.Put);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { title = "How to kill a Mocking Bird 2", body = "Shoot It Again " });

            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
            // Assert.IsNotEmpty(user.Email);

        }

        [Test]
        public void DeleteUser()
        {

            var req = new RestRequest("posts/1", Method.Delete);

            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            // Assert.IsNotEmpty(user.Email);

        }
        [Test]
        [Order(5)]
        public void GetNonExistingUser()
        {
            var req = new RestRequest("posts/1111", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));


        }

        [Test]
        public void GetAllUser()
        {

            var req = new RestRequest("posts", Method.Get);
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            List<UserData> user = JsonConvert.DeserializeObject<List<UserData>>(res.Content);
            Console.WriteLine("get all users :" + res.Content);

        }
    }
}
