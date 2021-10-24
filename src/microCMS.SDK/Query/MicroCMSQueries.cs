using System;
using System.Collections.Generic;
using System.Text;

namespace microCMS.SDK.Query
{
    public class MicroCMSQueries
    {
        public string DraftKey { get; set; }

        public int? Limit { get; set; }

        public int? Offset { get; set; } 

        public string Orders { get; set; }

        public string Fields { get; set; }

        public string Q { get; set; }

        public int? Depth { get; set; }

        public string Ids { get; set; }

        public string Filters { get; set; }
    }
}
