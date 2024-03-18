using BaiduMap.Request;
using BaiduMap.Request.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace BaiduMapTest
{
    public class GeoCoderTest
    {
        [Fact]
        public void Test()
        {
            var baiduClient = ClientCreator.Create();
            var model = new GeoCoderModel()
            {
                Address = "杭州市西湖区浙大路38号",
            };
            var req = new GeoCoderRequest(model);
            var resp = baiduClient.Execute(req);
            resp.Status.ShouldBe(0);
        }
    }
}
