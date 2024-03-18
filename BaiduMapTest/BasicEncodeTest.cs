using BaiduMap.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace BaiduMapTest
{
    public class BasicEncodeTest
    {
        [Fact]
        public void Test()
        {
            // http://lbsyun.baidu.com/index.php?title=webapi/appendix
            // from Java Sample Code
            var dic = new Dictionary<string, string>
            {
                { "address", "百度大厦" },
                { "output", "json" },
                { "ak", "yourak" }
            };

            var queryString = UrlUtil.BuildEncodeQuery(dic);
            queryString.ShouldBe("address=%E7%99%BE%E5%BA%A6%E5%A4%A7%E5%8E%A6&output=json&ak=yourak");
            var url = "/geocoder/v2/?" + queryString + "yoursk";
            var tmp = UrlUtil.Encode(url);
            var md5 = EncryptUtil.MD5(tmp);
            md5.ShouldBe("7de5a22212ffaa9e326444c75a58f9a0");
        }
    }
}
