using System;
using System.Collections.Generic;
using System.Text;

using microCMS.SDK.Query;

namespace microCMS.SDK
{
    public class GetRequest
    {
        public string Endpoint { get; set; }

        public string ContentId { get; set; }

        public MicroCMSQueries Queries { get; set; } = new MicroCMSQueries();
    }
}
