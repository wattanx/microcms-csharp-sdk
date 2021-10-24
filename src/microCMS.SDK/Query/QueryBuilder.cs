using System;
using System.Collections.Generic;
using System.Text;

using System.Web;

namespace microCMS.SDK.Query
{
    public class QueryBuilder
    {
        private readonly MicroCMSQueries queryObject;

        public QueryBuilder(MicroCMSQueries queryObject)
        {
            this.queryObject = queryObject;
        }

        public string Build()
        {
            var query = HttpUtility.ParseQueryString("");
            
            if (!string.IsNullOrEmpty(queryObject.DraftKey)) { query.Add(nameof(MicroCMSQueries.DraftKey).ToLower(), queryObject.DraftKey); }
            if (!string.IsNullOrEmpty(queryObject.Orders)) { query.Add(nameof(MicroCMSQueries.Orders).ToLower(), queryObject.Orders); }
            if (!string.IsNullOrEmpty(queryObject.Fields)) { query.Add(nameof(MicroCMSQueries.Fields).ToLower(), queryObject.Fields); }
            if (!string.IsNullOrEmpty(queryObject.Q)) { query.Add(nameof(MicroCMSQueries.Q).ToLower(), queryObject.Q); }
            if (!string.IsNullOrEmpty(queryObject.Ids)) { query.Add(nameof(MicroCMSQueries.Ids).ToLower(), queryObject.Ids); }
            if (!string.IsNullOrEmpty(queryObject.Filters)) { query.Add(nameof(MicroCMSQueries.Filters).ToLower(), queryObject.Filters); }
            if (queryObject.Limit.HasValue) { query.Add(nameof(MicroCMSQueries.Limit).ToLower(), queryObject.Limit.ToString()); }
            if (queryObject.Offset.HasValue) { query.Add(nameof(MicroCMSQueries.Offset).ToLower(), queryObject.Offset.ToString()); }
            if (queryObject.Depth.HasValue)
            {
                if (queryObject.Depth < 1 || queryObject.Depth > 3)
                {
                    throw new ArgumentOutOfRangeException("Depth", "The depth must be between 1 and 3.");
                }
                query.Add(nameof(MicroCMSQueries.Depth).ToLower(), queryObject.Depth.ToString());
            }

            return query.ToString();
        }
    }
}
