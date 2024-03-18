using BaiduMap.Request;
using BaiduMap.Request.Models;
using BaiduMap.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace BaiduMapTest
{
    public class PlaceDetailTest
    {
        [Fact]
        public void Test()
        {
            var baiduClient = ClientCreator.Create();
            var detailMode = new PlaceDetailModel()
            {
                Uid = "d5101afd619d12c7f423100e",
            };
            
            var req = new PlaceDetailRequest(detailMode);
            PlaceDetailResponse resp = baiduClient.Execute(req);
            resp.Result.ShouldNotBeNull();
        }
    }
}
