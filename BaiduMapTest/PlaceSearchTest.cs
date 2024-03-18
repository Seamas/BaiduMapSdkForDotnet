using System;
using System.Collections.Generic;
using BaiduMap.Response.Place;
using BaiduMap.Util;
using Xunit;
using System.Linq;
using System.Web;
using Shouldly;
using BaiduMap.Request;
using BaiduMap.Request.Models;

namespace BaiduMapTest
{
    public class PlaceSearchTest
    {
        [Fact]
        public void ExecuteRegionSearch()
        {
            var baiduClient = ClientCreator.Create();
            var regionModel = new PlaceRegionModel
            {
                Query = "ATM机",
                Tag = "银行",
                Region = "杭州"
            };
            var req = new PlaceSearchRequest<PlaceRegionModel>(regionModel);
            PlaceSearchResponse resp = baiduClient.Execute(req);
            resp.Status.ShouldBe(0);
            resp.Message.ShouldBe("ok");
            resp.Results.Count.ShouldBe(10);
        }

        [Fact]
        public void ExecuteCircumSearch()
        {
            var baiduClient = ClientCreator.Create();
            var circumModel = new PlaceCircumModel
            {
                Query = "银行",
                Location = "39.915,116.404",
                Radius = "2000"
            };
            var req = new PlaceSearchRequest<PlaceCircumModel>(circumModel);
            PlaceSearchResponse resp = baiduClient.Execute(req);
            resp.Status.ShouldBe(0);
            resp.Message.ShouldBe("ok");
        }

        [Fact]
        public void ExecuteRectangeSearch()
        {
            var baiduClient = ClientCreator.Create();
            var rectangeModel = new PlaceRectangeModel
            {
                Query = "银行",
                Bounds = "39.915,116.404,39.975,116.414"
            };
            var req = new PlaceSearchRequest<PlaceRectangeModel>(rectangeModel);
            PlaceSearchResponse resp = baiduClient.Execute(req);
            resp.Status.ShouldBe(0);
            resp.Message.ShouldBe("ok");
        }
    }
}
