using System;
using BaiduMap.Request.Models;
using BaiduMap.Util;
using Xunit;
using Shouldly;

namespace BaiduMapTest
{
    public class DictionaryUtilTest
    {
        [Fact]
        public void GetDictionary()
        {
            var model = new PlaceRegionModel
            {
                Query = "测试",
                Region = "杭州"
            };
            var dic = DictionaryUtil.GetDictionary(model);
            dic.ShouldContainKey("query");
            dic.ShouldContainKey("region");
            dic.ShouldContainKey("output");

            dic["query"].ShouldBe("测试");
            dic["region"].ShouldBe("杭州");
            dic["output"].ShouldBe("json");
        }
    }
}
