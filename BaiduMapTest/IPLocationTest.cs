using BaiduMap.Request;
using BaiduMap.Request.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace BaiduMapTest
{
    public class IPLocationTest
    {
        [Fact]
        public void Test()
        {
            var client = ClientCreator.Create();
            var model = new IPModel()
            {
                //IP = "183.134.216.113"
            };
            var req = new IPLocationRequest(model);
            var resp = client.Execute(req);
            resp.Address.ShouldNotBeNull();
            resp.Content.ShouldNotBeNull();
        }
    }
}
