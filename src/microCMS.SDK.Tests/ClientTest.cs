using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using microCMS.SDK.Client;
using microCMS.SDK.Query;

namespace microCMS.SDK.Tests
{
    [TestClass]
    public class ClientTest
    {
        public TestContext TestContext { get; set; }
        private string apiKey;
        private string serviceDomain;

        [TestMethod]
        public void GetTest_List()
        {
            var queries = new MicroCMSQueries()
            {
                Limit = 10
            };
            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.Get<MicroCMSListResponse<Category>>(new GetRequest() { Endpoint = "categories", Queries = queries }).Result;

            response.TotalCount.Is(3);
        }

        [TestMethod]
        public void GetTest_Detail()
        {
            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.Get<Blog>(new GetRequest() { Endpoint = "blog", ContentId = "m_5fm20iyh" }).Result;

            response.Id.Is("m_5fm20iyh");
        }

        [TestMethod]
        public void GetListTest()
        {
            var queries = new MicroCMSQueries()
            {
                Limit = 10
            };
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            apiKey = TestContext.Properties["ApiKey"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.GetList<Category>(new GetListRequest() { Endpoint = "categories", Queries = queries }).Result;

            response.TotalCount.Is(3);
        }

        [TestMethod]
        public void GetListDetailTest()
        {
            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.GetListDetail<Blog>(new GetListDetailRequest() { Endpoint = "blog", ContentId = "m_5fm20iyh" }).Result;

            response.Id.Is("m_5fm20iyh");
        }

        [TestMethod]
        public void GetObjectTest()
        {
            var queries = new MicroCMSQueries()
            {
                Limit = 10
            };

            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.GetObject<PopularArticles>(new GetObjectRequest() { Endpoint = "popular-articles", Queries = queries }).Result;

            response.Articles.FirstOrDefault().Id.Is("m_5fm20iyh");
        }

        [TestMethod]
        public void GetListTest_Limit()
        {
            var queries = new MicroCMSQueries()
            {
                Limit = 10
            };

            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.GetList<Blog>(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            response.Contents.Count().Is(10);
        }

        [TestMethod]
        public void GetListTest_Ids()
        {
            var queries = new MicroCMSQueries()
            {
               Ids = "m_5fm20iyh,hyqi1_v--eq"
            };

            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.GetList<Blog>(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            response.Contents.Count().Is(2);
        }

        [TestMethod]
        public void GetListTest_Fields()
        {
            var queries = new MicroCMSQueries()
            {
                Fields = "id,writer"
            };

            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.GetList<Blog>(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            response.Contents.FirstOrDefault().Id.Is("m_5fm20iyh");
            response.Contents.FirstOrDefault().Writer.Name.Is("wattanx");
        }

        [TestMethod]
        public void GetListTest_Orders()
        {
            var queries = new MicroCMSQueries()
            {
                Orders = "-createdAt"
            };

            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.GetList<Blog>(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            response.Contents.FirstOrDefault().Id.Is("elj-nqloy4ni");
        }

        [TestMethod]
        public void GetListTest_Filters()
        {
            var queries = new MicroCMSQueries()
            {
                Filters = "id[equals]m_5fm20iyh"
            };

            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey, "");
            var response = client.GetList<Blog>(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            response.Contents.Count().Is(1);
        }
    }

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
