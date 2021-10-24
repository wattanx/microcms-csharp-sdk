using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace microCMS.SDK
{
    public class MicroCMSDate
    {
        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonProperty("publishedAt")]
        public string PublishedAt { get; set; }

        [JsonProperty("revisedAt")]
        public string RevisedAt { get; set; }
    }
}
