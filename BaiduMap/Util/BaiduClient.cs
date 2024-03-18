using BaiduMap.Request;
using BaiduMap.Response;
using System.Net.Http;
using Newtonsoft.Json;
using BaiduMap.Request.Models;
using System.Threading.Tasks;
using System;
using BaiduMap.Extensions;

namespace BaiduMap.Util
{
    public class BaiduClient
    {
        public string Ak { get; private set; }
        public string Sk { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ak">appKey</param>
        public BaiduClient(string ak) 
        {
            this.Ak = ak;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ak">appKey</param>
        /// <param name="sk">secretKey</param>
        public BaiduClient(string ak, string sk) : this(ak)
        {
            this.Sk = sk;
        }

        /// <summary>
        /// 执行查询接口，根据构造函数sk值的不同，调用不同验证方式的接口
        /// 如果sk为空，调用 ip白名单验证
        /// 否则调用sn验证方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public T Execute<T>(IBaiduRequest<T> request)
            where T : BaiduResponse
        {
            var queryString = BuildQueryString(request);
            return GetResponse<T>(request.Host, request.Address, queryString);
        }

        /// <summary>
        /// 异步调用接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<T> ExecuteAsync<T>(IBaiduRequest<T> request)
            where T: BaiduResponse
        {
            return Task.Run(() => Execute(request));
        }

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public string ExecuteReadString<T>(IBaiduRequest<T> request)
            where T: BaiduResponse
        {
            var queryString = BuildQueryString(request);
            return GetResponseString(request.Host, request.Address, queryString);
        }

        /// <summary>
        /// 异步调用返回字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<string> ExecuteReadStringAsync<T>(IBaiduRequest<T> request)
            where T: BaiduResponse
        {
            return Task.Run(() => ExecuteReadString(request));
        }
        
        /// <summary>
        /// 获取返回结果字符串
        /// </summary>
        /// <param name="host"></param>
        /// <param name="address"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private string GetResponseString(string host, string address, string queryString)
        {
            var url = host + address + "?" + queryString;
            var client = new HttpClient();
            return client.GetStringAsync(url).Result;
        }

        /// <summary>
        /// 采用Get方式发送请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="host"></param>
        /// <param name="address"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private T GetResponse<T>(string host, string address, string queryString)
            where T : BaiduResponse
        {
            var respString = GetResponseString(host, address, queryString);
            var result = JsonConvert.DeserializeObject<T>(respString);
            result.Meta = respString;
            return result;
        }

        /// <summary>
        /// 构建查询字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private string BuildQueryString<T>(IBaiduRequest<T> request)
            where T: BaiduResponse
        {
            return string.IsNullOrWhiteSpace(Sk) ? BuildAkQueryString(request) : BuildSNQueryString(request);
        }

        /// <summary>
        /// 构建AK查询字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private string BuildAkQueryString<T>(IBaiduRequest<T> request)
            where T : BaiduResponse
        {
            var dictionary = request.GetParameters();
            dictionary.Add("ak", Ak);
            return UrlUtil.BuildQuery(dictionary);
        }

        /// <summary>
        /// 构建SN查询字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private string BuildSNQueryString<T>(IBaiduRequest<T> request)
            where T : BaiduResponse
        {
            var dictionary = request.GetParameters();
            if (request.RequiredTimestamp)
            {
                // 用户未设置timestamp时自动增加，若已经设置timestamp，则使用用户设置的值
                if (!dictionary.ContainsKey("timestamp"))
                {
                    dictionary.Add("timestamp", DateTime.Now.ToTimestamp().ToString());
                }
            }

            var sn = AKSNCaculator.CaculateAKSN(Ak, Sk, request.Address, dictionary);
            dictionary.Add("sn", sn);
            return UrlUtil.BuildQuery(dictionary);
        }
    }
}
