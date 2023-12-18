using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Module3_CaseStudy
{
    public class CoreCodes
    {
        protected ExtentReports extent;
        protected ExtentTest test;
        protected RestClient client;
        protected ExtentSparkReporter sparkReporter;
        private string baseUrl = "https://restful-booker.herokuapp.com/";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            String currdir = Directory.GetParent(@"../../../").FullName;
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currdir + "/ExtentReports/extent-report"
                + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
            extent.AttachReporter(sparkReporter);
            string logfilepath = currdir + "/Logs/log_" + DateTime.Now.ToString("yyyymmdd_HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();
        }

        public string GetAut()
        {

            test = extent.CreateTest("Auth User Test");
            var req = new RestRequest("/auth", Method.Post);
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new { username = "admin", password = "password123" });
            var res = client.Execute(req);
            var userdata = JsonConvert.DeserializeObject<Cookies>(res.Content);

            return userdata.Token;
        }
    }
            
}
