using BaiduMap.Request;
using BaiduMap.Request.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace BaiduMapTest
{
    public class PlaceSuggestionTest
    {
        [Fact]
        public void Test()
        {
            var client = ClientCreator.Create();
            var model = new PlaceSuggestionModel()
            {
                Query = "浙江大学",
                Region = "杭州"
            };
            var req = new PlaceSuggestionRequest(model);
            var res = client.Execute(req);
            res.Status.ShouldBe(0);
            res.Result.ShouldNotBeNull();
        }
    }
}
