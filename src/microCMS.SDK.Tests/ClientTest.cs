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
            var client = new MicroCMSClient(serviceDomain, apiKey);
            var response = client.Get<MicroCMSListResponse<Category>>(new GetRequest() { Endpoint = "categories", Queries = queries }).Result;

            response.TotalCount.Is(3);
        }

        [TestMethod]
        public void GetTest_Detail()
        {
            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey);
            var response = client.Get<Blog>(new GetRequest() { Endpoint = "blog", ContentId = "hyqi1_v--eq" }).Result;

            response.Id.Is("hyqi1_v--eq");
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
            var client = new MicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList<Category>(new GetListRequest() { Endpoint = "categories", Queries = queries }).Result;

            response.TotalCount.Is(3);
        }

        [TestMethod]
        public void GetListDetailTest()
        {
            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey);
            var response = client.GetListDetail<Blog>(new GetListDetailRequest() { Endpoint = "blog", ContentId = "hyqi1_v--eq" }).Result;

            response.Id.Is("hyqi1_v--eq");
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
            var client = new MicroCMSClient(serviceDomain, apiKey);
            var response = client.GetObject<PopularArticles>(new GetObjectRequest() { Endpoint = "popular-articles", Queries = queries }).Result;

            response.Articles.FirstOrDefault().Id.Is("hyqi1_v--eq");
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
            var client = new MicroCMSClient(serviceDomain, apiKey);
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
            var client = new MicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList<Blog>(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            response.Contents.Count().Is(1);
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
            var client = new MicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList<Blog>(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            response.Contents.FirstOrDefault().Id.Is("hyqi1_v--eq");
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
            var client = new MicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList<Blog>(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            response.Contents.FirstOrDefault().Id.Is("elj-nqloy4ni");
        }

        [TestMethod]
        public void GetListTest_Filters()
        {
            var queries = new MicroCMSQueries()
            {
                Filters = "id[equals]hyqi1_v--eq"
            };

            apiKey = TestContext.Properties["ApiKey"].ToString();
            serviceDomain = TestContext.Properties["ServiceDomain"].ToString();
            var client = new MicroCMSClient(serviceDomain, apiKey);
            var response = client.GetList<Blog>(new GetListRequest() { Endpoint = "blog", Queries = queries }).Result;

            response.Contents.Count().Is(1);
        }
    }
}
