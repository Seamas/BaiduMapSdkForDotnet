using System;
using BaiduMap.Attributes;

namespace BaiduMap.Request.Models
{
    public class CoordTransfModel : BaiduModel
    {
        [Required]
        public string Coords { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
    }
}
