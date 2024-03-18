using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace BaiduMap.Util
{
    public static class AKSNCaculator
    {
        public static string CaculateAKSN(string ak, string sk, string url, IDictionary<string, string> dictionary)
        {
            // ak 必须是 dictionary中的最后一个参数
            if (dictionary.ContainsKey("ak"))
            {
                dictionary.Remove("ak");
            }
            dictionary.Add("ak", ak);

            // 计算sn时 dictionary不能有 sn
            if (dictionary.ContainsKey("sn"))
            {
                dictionary.Remove("sn");
            }

            var queryString = UrlUtil.BuildEncodeQuery(dictionary);
            var str = UrlUtil.Encode(url + "?" + queryString + sk);
            return EncryptUtil.MD5(str);
        }
    }
}
