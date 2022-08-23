using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using microCMS.SDK.Query;

namespace microCMS.SDK.Client
{
    /// <summary>
    /// Non-deserialized client
    /// </summary>
    public class SimpleMicroCMSClient
    {
        private static readonly HttpClient httpClient;
        private const string BASE_DOMAIN = "microcms.io";
        private const string API_VERSION = "v1";
        private string BaseUrl => $"https://{ServiceDomain}.{BASE_DOMAIN}/api/{API_VERSION}";

        static SimpleMicroCMSClient()
        {
            httpClient = new HttpClient();
        }

        public SimpleMicroCMSClient(string serviceDomain, string apiKey)
        {
            this.serviceDomain = serviceDomain;
            this.apiKey = apiKey;
        }

        private string serviceDomain;
        public string ServiceDomain
        {
            get => serviceDomain;
            set => serviceDomain = value;
        }

        private string apiKey;
        public string ApiKey
        {
            get => apiKey;
            set => apiKey = value;
        }

        /// <summary>
        /// Get list and object API data for microCMS
        /// </summary>
        public async Task<string> Get(GetRequest request)
        {
            return await Send(request.Endpoint, request.ContentId, request.Queries);
        }

        /// <summary>
        /// Get list API data for microCMS
        /// </summary>
        public async Task<string> GetList(GetListRequest request)
        {
            return await Send(request.Endpoint, "", request.Queries);
        }

        /// <summary>
        /// Get list API detail data for microCMS
        /// </summary>
        public async Task<string> GetListDetail(GetListDetailRequest request)
        {
            if (string.IsNullOrEmpty(request.ContentId))
            {
                throw new ArgumentException("contentId is required");
            }
            return await Send(request.Endpoint, request.ContentId, request.Queries);
        }

        /// <summary>
        /// Get object API data for microCMS
        /// </summary>
        public async Task<string> GetObject(GetObjectRequest request)
        {
            return await Send(request.Endpoint, "", request.Queries);
        }

        private async Task<string> Send(string endpoint, string contentId, MicroCMSQueries queries)
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentException("endpoint is required");
            }

            var queryString = new QueryBuilder(queries).Build();

            var url = CreateUrl(endpoint, contentId, queryString);
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("X-MICROCMS-API-KEY", ApiKey);

            try
            {
                var response = await httpClient.SendAsync(request);
                return await response.Content.ReadAsStringAsync();

            }
            catch (WebException ex)
            {
                throw new WebException($"API response status: {ex.Status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new Exception($"serviceDomain or endpoint may be wrong.\n Details: {ex.Message}");
            }
        }

        private string CreateUrl(string endpoint, string contentId, string queryString)
        {
            var url = $"{BaseUrl}/{endpoint}";
            if (!string.IsNullOrEmpty(contentId))
            {
                url += $"/{contentId}";
            }
            if (!string.IsNullOrEmpty(queryString))
            {
                url += $"?{queryString}";
            }

            return url;
        }
    }
}
