using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace microCMS.SDK.Client
{
    public interface IMicroCmsClient
    {
        Task<T> Get<T>(GetRequest request);

        /// <summary>
        /// Get list API data for microCMS
        /// </summary>
        Task<MicroCMSListResponse<T>> GetList<T>(GetListRequest request) where T : MicroCMSListContent;

        /// <summary>
        /// Get list API detail data for microCMS
        /// </summary>
        Task<T> GetListDetail<T>(GetListDetailRequest request) where T : MicroCMSListContent;

        /// <summary>
        /// Get object API data for microCMS
        /// </summary>
        Task<T> GetObject<T>(GetObjectRequest request) where T : MicroCMSObjectContent;
    }
}
