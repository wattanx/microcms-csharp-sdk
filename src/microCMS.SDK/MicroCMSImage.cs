using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace microCMS.SDK
{
    public class MicroCMSImage
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
