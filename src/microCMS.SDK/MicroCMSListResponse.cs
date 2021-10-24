using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace microCMS.SDK
{
    public class MicroCMSListResponse<T> where T : MicroCMSListContent
    {
        [JsonProperty("contents")]
        public IEnumerable<T> Contents { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}
