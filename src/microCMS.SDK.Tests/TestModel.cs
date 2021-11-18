using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using microCMS.SDK.Client;
using microCMS.SDK.Query;

namespace microCMS.SDK.Tests
{
    public class Category : MicroCMSListContent
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Tag : MicroCMSListContent
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Author : MicroCMSListContent
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class PopularArticles : MicroCMSObjectContent
    {
        [JsonProperty("articles")]
        public IEnumerable<Blog> Articles { get; set; }
    }

    public class Blog : MicroCMSListContent
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("tag")]
        public IEnumerable<Tag> Tag { get; set; }

        [JsonProperty("toc_visible")]
        public bool TocVisible { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ogimage")]
        public MicroCMSImage OgImage { get; set; }

        [JsonProperty("writer")]
        public Author Writer { get; set; }

        [JsonProperty("partner")]
        public string Partner { get; set; }

        [JsonProperty("related_blogs")]
        public IEnumerable<Blog> RelatedBlog { get; set; }
    }
}
