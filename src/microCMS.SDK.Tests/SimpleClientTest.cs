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
    public class SimpleClientTest
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
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.Get(new GetRequest() { Endpoint = "categories", Queries = queries }).Result;

            var result = JsonConvert.DeserializeObject<MicroCMSListResponse<Category>>(response);

            result.TotalCount.Is(3);
        }

        [TestMethod]
        public void GetTest_Detail()
        {
            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.Get(new GetRequest() { Endpoint = "blog", ContentId = "m_5fm20iyh" }).Result;

            var result = JsonConvert.DeserializeObject<Blog>(response);
            result.Id.Is("m_5fm20iyh");
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
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList(new GetListRequest() { Endpoint = "categories", Queries = queries }).Result;

            var result = JsonConvert.DeserializeObject<MicroCMSListResponse<Category>>(response);
            result.TotalCount.Is(3);
        }

        [TestMethod]
        public void GetListDetailTest()
        {
            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.GetListDetail(new GetListDetailRequest() { Endpoint = "blog", ContentId = "m_5fm20iyh" }).Result;

            var result = JsonConvert.DeserializeObject<Blog>(response);
            result.Id.Is("m_5fm20iyh");
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
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.GetObject(new GetObjectRequest() { Endpoint = "popular-articles", Queries = queries }).Result;

            var result = JsonConvert.DeserializeObject<PopularArticles>(response);
            result.Articles.FirstOrDefault().Id.Is("m_5fm20iyh");
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
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            var result = JsonConvert.DeserializeObject<MicroCMSListResponse<Blog>>(response);
            result.Contents.Count().Is(10);
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
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            var result = JsonConvert.DeserializeObject<MicroCMSListResponse<Blog>>(response);
            result.Contents.Count().Is(2);
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
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            var result = JsonConvert.DeserializeObject<MicroCMSListResponse<Blog>>(response);
            result.Contents.FirstOrDefault().Id.Is("m_5fm20iyh");
            result.Contents.FirstOrDefault().Writer.Name.Is("wattanx");
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
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            var result = JsonConvert.DeserializeObject<MicroCMSListResponse<Blog>>(response);
            result.Contents.FirstOrDefault().Id.Is("elj-nqloy4ni");
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
            var client = new SimpleMicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            var result = JsonConvert.DeserializeObject<MicroCMSListResponse<Blog>>(response);
            result.Contents.Count().Is(1);
        }
    }
}
