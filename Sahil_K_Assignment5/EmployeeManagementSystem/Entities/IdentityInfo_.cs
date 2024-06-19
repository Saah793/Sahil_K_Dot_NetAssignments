using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.Entities
{
    public class IdentityInfo_
    {
        [JsonProperty(PropertyName = "PAN", NullValueHandling = NullValueHandling.Ignore)]
        public string PAN { get; set; }

        [JsonProperty(PropertyName = "Aadhar", NullValueHandling = NullValueHandling.Ignore)]
        public string Aadhar { get; set; }


        [JsonProperty(PropertyName = "Nationality", NullValueHandling = NullValueHandling.Ignore)]
        public string Nationality { get; set; }


        [JsonProperty(PropertyName = "PassportNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PassportNumber { get; set; }

        [JsonProperty(PropertyName = "PFNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PFNumber { get; set; }
    }
}
