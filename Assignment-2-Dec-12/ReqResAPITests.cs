using Newtonsoft.Json;
using NUnit.Framework.Internal;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_Dec_12

{
    [TestFixture]
    internal class ReqResAPITests : CoreCodes
    {

        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";
        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        [TestCase(2)]
        public void GetSingleUser(int usrid)
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("GetSingleUser test started");
            var req = new RestRequest("posts/"+usrid, Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Log.Information($"API  Response :{response.Content}");
            var userdata = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(userdata);
            Log.Information("User returned");
            Assert.That(userdata.Id, Is.EqualTo(usrid));
            Log.Information("user id matches with fetch");
            Log.Information("GetSingleUser All tests passed");
            test.Pass("GetSingleUser All tests passed");

        }
        [Test]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User");
            Log.Information("Create User test started");
            var req = new RestRequest("posts", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { title = "How to kill a Mocking Bird", body = "Shoot It " });

            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            Log.Information($"API  Response :{response.Content}");
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
            Log.Information("User returned");
            
            Log.Information("CreateUser All tests passed");
            test.Pass("CreateUser All tests passed");
            // Assert.IsNotEmpty(user.Email);

        }

        [Test]
        [TestCase(2)]
        public void UpdateUser(int uid)
        {
            test = extent.CreateTest("Update User");
            Log.Information("UpdateUser test started");
            var req = new RestRequest("posts/"+uid, Method.Put);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { title = "How to kill a Mocking Bird 2", body = "Shoot It Again " });

            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Log.Information($"API  Response :{response.Content}");

            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
            Log.Information("User returned");          
            Assert.That(user.Id, Is.EqualTo(uid));
            Log.Information("user id matches with fetch");
            Log.Information("Update User All tests passed");
            test.Pass("Update user All tests passed");
        }

        [Test]
        [TestCase(1)]
        public void DeleteUser(int Uid)
        {
            test = extent.CreateTest("Delete User");
            Log.Information("DeleteUser test started");
            
            var req = new RestRequest("posts/"+Uid, Method.Delete);

            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            Log.Information("Delete User All tests passed");
            test.Pass("Delete user All tests passed");


            // Assert.IsNotEmpty(user.Email);

        }
        [Test]
        [Order(5)]
        public void GetNonExistingUser()
        {
            test = extent.CreateTest("GetNonExisting User");
            Log.Information("GetNonExistingUser test started");

            var req = new RestRequest("posts/1111", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));

            Log.Information("GetNonExistingUser All tests passed");
            test.Pass("GetNonExistingUser All tests passed");

        }

        [Test]
        public void GetAllUser()
        {
            test = extent.CreateTest("Get All User");
            Log.Information("GetAllUser test started");
            var req = new RestRequest("posts", Method.Get);
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Log.Information($"API  Response :{res.Content}");
            List<UserData> user = JsonConvert.DeserializeObject<List<UserData>>(res.Content);
            Log.Information("GetAllUser All tests passed");
            test.Pass("GetAllUser All tests passed");

        }
    }
}
