using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_CaseStudy
{
    [TestFixture]
    internal class RestfulBookerTest : CoreCodes
    {
        [Test]
        [TestCase(5)]
        public void GetSingleBooking(int uid)
        {
            test = extent.CreateTest("Get Single Book");
            Log.Information("GetSingleBooking test started");

            var req = new RestRequest("/booking/"+uid, Method.Get);
            req.AddHeader("Accept", "application/json");
            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API  Response :{response.Content}");
                var userdata = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
                BookingdatesData? bookingdatesData = userdata?.Bookingdates;
                Assert.NotNull(userdata);
                Log.Information("User returned");
               
                Assert.IsNotEmpty(bookingdatesData.Checkin);
                Log.Information("Checkin is not empty");
                Log.Information("GetSingleUser All tests passed");
                test.Pass("GetSingleUser All tests passed");
            }
            catch (AssertionException)
            {
                test.Fail("GetSingleUser test failed ");
            }
        }
        [Test]
        public void CreateBooking()
        {
            test = extent.CreateTest("Create User");
            Log.Information("CreateUser test started");
            var req = new RestRequest("/booking", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Accept", "application/json");
            req.AddJsonBody(new {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new {
                checkin = "2018-01-01",
        checkout = "2019-01-01"
                },
    additionalneeds = "Breakfast" });
            //, TotalPrice="231", AdditionalNeeds="No Need", checkin= "2018-01-01"

            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API  Response :{response.Content}");
                var user = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
                Assert.NotNull(user);
                Log.Information("user Created and returned");
                Assert.IsNotEmpty(user.FirstName);
                Log.Information("FirstName is not empty");
                Log.Information("Create user all tests passed ");
                test.Pass("Create user test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Create user Test Failed.");
            }
            // Assert.IsNotEmpty(user.Email);

        }
        [Test]
        [Order(1)]
        public void GetAuth()
        {

            test = extent.CreateTest("Auth User Test");
            var req = new RestRequest("/auth", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { username = "admin", password = "password123" });
            var res = client.Execute(req);

            try
            {
                Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{res.Content}");
                var userdata = JsonConvert.DeserializeObject<Cookies>(res.Content);
                Assert.NotNull(userdata);
                Log.Information("Get UserData Test Passed");
                Console.WriteLine(userdata.Token);
                Assert.IsNotEmpty(userdata.Token);
                Log.Information("User Title Is Correct");

                test.Pass("GetSingle User Test Passed");
            }
            catch (AssertionException)
            {

                test.Fail("GetSingle User Test Fail");
            }

        }
    
        [Test]
        [TestCase(7)]
        public void UpdateBooking(int uid)
        {
            test = extent.CreateTest("Update User");
            Log.Information("Update User test started");
           

            try
            {
                var reqput = new RestRequest("booking/12", Method.Put);
                reqput.AddHeader("Content-Type", "Application/Json");
                reqput.AddHeader("Cookie", "token=" + GetAut());
                reqput.AddJsonBody(new
                {
                    firstname = "amal",
                    lastname = "k",
                    totalprice = 1231,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2023-03-11",
                        checkout = "2023-03-18"
                    },
                    additionalneeds = "Extra pillow"


                });

            }
            catch (AssertionException)
            {

                test.Pass("Update User Test Failed");
            }
        }

        [Test]
        [Order(4)]
        [TestCase(7)]
        public void DeleteBooking(int usrid)
        {
            
           
            test = extent.CreateTest("Delete User");
            Log.Information("Delete user test started");
        
            try
            {
          
                var req = new RestRequest("/booking/" + usrid, Method.Delete)
                     .AddHeader("Content-Type", "application/json");
                req.AddHeader("Cookie", "token=" + GetAut());              
            var response = client.Execute(req);
                req.AddJsonBody(new
                {
                    firstname = "Aromal",
                    lastname = "S",
                    totalprice = 1231,
                    depositpaid = true,
                    bookingdates = new
                    {
                        checkin = "2023-03-11",
                        checkout = "2023-03-18"
                    },
                    additionalneeds = "Extra pillow"


                });

            }
            catch (AssertionException)
            {

                test.Pass("Update User Test Failed");
            }

            // Assert.IsNotEmpty(user.Email);

        }
        [Test]
        [Order(5)]
        public void GetNonExistingBooking()
        {
            test = extent.CreateTest("Get Non Existing Booking");
            Log.Information("GetNonExistingUser test started");
            var req = new RestRequest("/booking/456", Method.Get);
            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));

                Log.Information("GetNonExistingBooking Test Passed");
                test.Pass("GetNonExistingBooking Test Passed");
            }
            catch (AssertionException)
            {
                test.Fail("GetNonExistingBooking Test Failed");
            }

        }
        /*
        public string ReturnToken()
        {

            var req = new RestRequest("/booking", Method.Get);
            var res = client.Execute(req);
        }
        */
        [Test]
        public void GetAllBookings()
        {
            test = extent.CreateTest("Get All User");
            Log.Information("GetAllUser test started");
            var req = new RestRequest("/booking", Method.Get);
            var res = client.Execute(req);
            Assert.That(res.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Log.Information($"API  Response :{res.Content}");
            List<UserDataResponse> bookingData = JsonConvert.DeserializeObject<List<UserDataResponse>>(res.Content);
            Assert.NotNull(bookingData);
            Log.Information("Booking All ids Passed");
            Log.Information("GetAllUser All tests passed");
            test.Pass("GetAllUser All tests passed");

        }
    }
}
