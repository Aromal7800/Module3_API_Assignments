﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_CaseStudy
{
    public class Cookies
    {
        [JsonProperty("token")]
        public string? Token { get; set; }
    }
}
