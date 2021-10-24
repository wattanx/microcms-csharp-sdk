using Microsoft.VisualStudio.TestTools.UnitTesting;
using microCMS.SDK.Query;

namespace microCMS.SDK.Tests
{
    [TestClass]
    public class QueryBuilderTests
    {
        [TestMethod]
        public void QueryObject_To_QueryString()
        {
            var queryObject = new MicroCMSQueries()
            {
                Fields = "id,author",
                Limit = 10,
                Offset = 0,
                Depth = 1,
                Q = "test",
                Orders = "-createdAt",
                Ids = "1,2",
                Filters = "gender[equals]women",
            };

            var query = new QueryBuilder(queryObject).Build();
            query.Is("orders=-createdAt&fields=id%2cauthor&q=test&ids=1%2c2&filters=gender%5bequals%5dwomen&limit=10&offset=0&depth=1");
        }
    }
}
