using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace microCMS.SDK
{
    public class MicroCMSListContent : MicroCMSDate
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
