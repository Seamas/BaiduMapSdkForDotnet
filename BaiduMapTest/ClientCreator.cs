using BaiduMap.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaiduMapTest
{
    public static class ClientCreator
    {
        static ClientCreator()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("test.json");

            var configuration = builder.Build();
            ak = configuration["ak"];
            sk = configuration["sk"];
        }

        private static readonly string ak = "";
        private static readonly string sk = "";

        public static BaiduClient Create()
        {
            return new BaiduClient(ak, sk);
        }
    }
}
