using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL.API
{
     interface IBaidu
    {
    }
     interface IMap
    {
          ILocation Location_IP(String ip, String ak);
          ILocation Location_IP(String ip, String ak, String sn, String coor);
    }
     interface ILocation
    {
         String address { get; set; }
         IContent content { get; set; }
         Int32 status { get; set; }
         List<String> lAddress { get; }
    }
     interface IContent
    {
         String address { get; set; }
         IAddressDetail address_detail { get; set; }
         IPoint point { get; set; }
    }
     interface IAddressDetail
    {
        /// <summary>
        /// 市
        /// </summary>
         String city { get; set; }
        /// <summary>
        /// 百度城市代码
        /// </summary>
         Int32 city_code { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
         String district { get; set; }
        /// <summary>
        /// 省
        /// </summary>
         String province { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
         String street { get; set; }
        /// <summary>
        /// 门址
        /// </summary>
         String street_number { get; set; }
    }
     interface IPoint
    {
         String x { get; set; }
         String y { get; set; }
    }
}
