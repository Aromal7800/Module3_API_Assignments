using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_CaseStudy
{
    public class BookingdatesData
    {
        [JsonProperty("checkin")]
        public string Checkin { get; set; }

        [JsonProperty("checkout")]
        public string Checkout { get; set; }


    }
    public class UserDataResponse
    {
        [JsonProperty("bookingdates")]
        public BookingdatesData? Bookingdates { get; set; }

        [JsonProperty("additionalneeds")]
        public string AdditionalNeeds { get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }


        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("totalprice")]
        public string TotalPrice { get; set; }

        [JsonProperty("depositpaid")]
        public string Depositpaid { get; set; }
    }
}
