using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using microCMS.SDK.Query;

namespace microCMS.SDK.Client
{
    public class MicroCMSClient : IMicroCmsClient
    {
        private static readonly HttpClient httpClient;
        private const string BASE_DOMAIN = "microcms.io";
        private const string API_VERSION = "v1";
        private string BaseUrl => $"https://{ServiceDomain}.{BASE_DOMAIN}/api/{API_VERSION}";

        static MicroCMSClient()
        {
            httpClient = new HttpClient();
        }

        public MicroCMSClient(string serviceDomain, string apiKey, string globalDraftKey)
        {
            this.serviceDomain = serviceDomain;
            this.apiKey = apiKey;
            this.globalDraftKey = globalDraftKey;
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

        private string globalDraftKey;
        public string GlobalDraftKey
        {
            get => globalDraftKey;
            set => globalDraftKey = value;
        }

        /// <summary>
        /// Get list and object API data for microCMS
        /// </summary>
        public async Task<T> Get<T>(GetRequest request)
        {
            return await Send<T>(request.Endpoint, request.ContentId, request.Queries, request.UseGlobalDraftKey);
        }

        /// <summary>
        /// Get list API data for microCMS
        /// </summary>
        public async Task<MicroCMSListResponse<T>> GetList<T>(GetListRequest request) where T : MicroCMSListContent
        {
            return await Send<MicroCMSListResponse<T>>(request.Endpoint, "", request.Queries, request.UseGlobalDraftKey);
        }

        /// <summary>
        /// Get list API detail data for microCMS
        /// </summary>
        public async Task<T> GetListDetail<T>(GetListDetailRequest request) where T : MicroCMSListContent
        {
            if (string.IsNullOrEmpty(request.ContentId))
            {
                throw new ArgumentException("contentId is required");
            }
            return await Send<T>(request.Endpoint, request.ContentId, request.Queries, request.UseGlobalDraftKey);
        }

        /// <summary>
        /// Get object API data for microCMS
        /// </summary>
        public async Task<T> GetObject<T>(GetObjectRequest request) where T : MicroCMSObjectContent
        {
            return await Send<T>(request.Endpoint, "", request.Queries, request.UseGlobalDraftKey);
        }

        private async Task<T> Send<T>(string endpoint, string contentId, MicroCMSQueries queries, bool useGlobalDraftKey)
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentException("endpoint is required");
            }

            var queryString = new QueryBuilder(queries).Build();

            var url = CreateUrl(endpoint, contentId, queryString);
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            // WIP Need to change to X-MICROCMS-API-KEY
            request.Headers.Add("X-API-KEY", ApiKey);

            if (!string.IsNullOrEmpty(GlobalDraftKey) && useGlobalDraftKey)
            {
                request.Headers.Add("X-GLOBAL-DRAFT-KEY", GlobalDraftKey);
            }

            try
            {
                var response = await httpClient.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(body);

            } 
            catch(WebException ex)
            {
                throw new WebException($"API response status: {ex.Status}");
            } 
            catch(Exception ex)
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
