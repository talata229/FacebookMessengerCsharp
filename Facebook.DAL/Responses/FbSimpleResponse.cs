using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.DAL.Responses
{
    public class FbSimpleResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
