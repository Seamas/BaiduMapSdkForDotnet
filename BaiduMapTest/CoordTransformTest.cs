using System;
using BaiduMap.Request;
using BaiduMap.Request.Models;
using Xunit;
using Shouldly;

namespace BaiduMapTest
{
    public class CoordTransformTest
    {

        [Fact]
        public void Test() 
        {
            var client = ClientCreator.Create();

            var model = new CoordTransfModel
            {
                Coords = "114.21892734521,29.575429778924"
            };

            var req = new CoordTransformRequest(model);
            var resp = client.Execute(req);

            resp.Status.ShouldBe(0);
        }

    }
}
