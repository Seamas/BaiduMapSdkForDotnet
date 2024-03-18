using System;
using System.Text;
using System.Web;
using System.Linq;
using System.Collections.Generic;

namespace BaiduMap.Util
{
    public static class UrlUtil
    {
        public static string Encode(string str)
        {
            str = HttpUtility.UrlEncode(str);
            byte[] buf = Encoding.ASCII.GetBytes(str);
            for (int i = 0; i < buf.Length; i++)
                if (buf[i] == '%')
                {
                    if (buf[i + 1] >= 'a') buf[i + 1] -= 32;
                    if (buf[i + 2] >= 'a') buf[i + 2] -= 32;
                    i += 2;
                }
            return Encoding.ASCII.GetString(buf);
        }

        /// <summary>
        /// 列表中的字符在参数的value中时，不应该进行url.encode转码
        /// </summary>
        static readonly List<string> exceptList = new List<string> { ","};
        
        /// <summary>
        /// 转码参数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string EncodeParam(string str)
        {
            var result = Encode(str);
            foreach(var except in exceptList)
            {
                result = result.Replace(Encode(except), except);
            }
            return result;
        }

        public static string BuildQuery(IDictionary<string, string> dictionary)
        {
            var list = dictionary.Select(item => $"{item.Key}={item.Value}");
            return String.Join("&", list);
        }

        public static string BuildEncodeQuery(IDictionary<string, string> dictionary)
        {
            var list = dictionary.Select(item => $"{item.Key}={EncodeParam(item.Value)}");
            return String.Join("&", list);
        }
    }
}
