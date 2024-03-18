using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using BaiduMap.Request.Models;
using BaiduMap.Request;
using BaiduMap.Response.Models;
using System.Threading.Tasks;

namespace BaiduMapTest
{
    public class PathPlanTest
    {
        [Fact]
        public void PublicTest()
        {
            var originAndDestionation = GetOriginAndDestination();
            var startPoint = originAndDestionation.startLocation;
            var endPoint = originAndDestionation.endLocation;

            var model = new PublicPlanModel()
            {
                Origin = $"{startPoint.Lat},{startPoint.Lng}",
                Destination = $"{endPoint.Lat},{endPoint.Lng}"
            };

            var req = new PublicPlanRequest(model);
            var client = ClientCreator.Create();
            var result = client.Execute(req);
            result.Status.ShouldBe(0);
        }

        [Fact]
        public void RideTest()
        {
            var originAndDestionation = GetOriginAndDestination();
            var startPoint = originAndDestionation.startLocation;
            var endPoint = originAndDestionation.endLocation;

            var model = new RidePlanModel()
            {
                Origin = $"{startPoint.Lat},{startPoint.Lng}",
                Destination = $"{endPoint.Lat},{endPoint.Lng}"
            };

            var req = new RidePlanRequest(model);

            var client = ClientCreator.Create();
            var result = client.Execute(req);
            result.Status.ShouldBe(0);
        }

        [Fact]
        public void DriveTest()
        {
            var originAndDestionation = GetOriginAndDestination();
            var startPoint = originAndDestionation.startLocation;
            var endPoint = originAndDestionation.endLocation;

            var model = new DrivePlanModel()
            {
                Origin = $"{startPoint.Lat},{startPoint.Lng}",
                Destination = $"{endPoint.Lat},{endPoint.Lng}"
            };

            var req = new DrivePlanRequest(model);

            var client = ClientCreator.Create();
            var result = client.Execute(req);
            result.Status.ShouldBe(0);
        }

        private (Location startLocation, Location endLocation) GetOriginAndDestination()
        {
            var client = ClientCreator.Create();
            var startModel = new GeoCoderModel()
            {
                Address = "杭州市西湖区浙大路38号",
            };
            var startRequest = new GeoCoderRequest(startModel);
            var startTask = client.ExecuteAsync(startRequest);

            var endModel = new GeoCoderModel()
            {
                Address = "浙江省杭州市拱墅区环城北路318号",
            };
            var endRequest = new GeoCoderRequest(endModel);
            var endTask = client.ExecuteAsync(endRequest);

            Task.WaitAll(startTask, endTask);

            var startResponse = startTask.Result;
            var endResponse = endTask.Result;

            var startPoint = startResponse.Result.Location;
            var endPoint = endResponse.Result.Location;
            return (startPoint, endPoint);
        }
    }
}
