using K.Y.DLL.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL.API
{
    public class Baidu 
    {
        public class Map 
        {
            public class Location 
            {
                public String address { get; set; }
                //{  
                //address: "CN|北京|北京|None|CHINANET|1|None",   #地址  
                //content:       #详细内容  
                //{  
                //address: "北京市",   #简要地址  
                //address_detail:      #详细地址信息  
                //{  
                //city: "北京市",        #城市  
                //city_code: 131,       #百度城市代码  
                //district: "",           #区县  
                //province: "北京市",   #省份  
                //street: "",            #街道  
                //street_number: ""    #门址  
                //},  
                //point:               #百度经纬度坐标值  
                //{  
                //x: "116.39564504",  
                //y: "39.92998578"  
                //}  
                //},  
                //status: 0     #返回状态码  
                //}
                public Content content { get; set; }
                public Int32 status { get; set; }
                public List<String> lAddress
                {
                    get
                    {
                        return (address ?? "").Ex_ToList('|');
                    }
                }
                public class Content 
                {
                    public String address { get; set; }
                    public AddressDetail address_detail { get; set; }
                    public Point point { get; set; }
                }
                public class AddressDetail 
                {
                    /// <summary>
                    /// 市
                    /// </summary>
                    public String city { get; set; }
                    /// <summary>
                    /// 百度城市代码
                    /// </summary>
                    public Int32 city_code { get; set; }
                    /// <summary>
                    /// 区县
                    /// </summary>
                    public String district { get; set; }
                    /// <summary>
                    /// 省
                    /// </summary>
                    public String province { get; set; }
                    /// <summary>
                    /// 街道
                    /// </summary>
                    public String street { get; set; }
                    /// <summary>
                    /// 门址
                    /// </summary>
                    public String street_number { get; set; }
                }
                public class Point 
                {
                    public String x { get; set; }
                    public String y { get; set; }
                }
            }
            public static Location Location_IP(String ip, String ak)
            {
                var res = T_Web.HttpGet("http://api.map.baidu.com/location/ip", "ip=" + ip + "&ak=" + ak);
                var L = res.Ex_ToEntity<Location>();
                return L;
            }
            public static Location Location_IP(String ip, String ak, String sn, String coor)
            {
                var res = T_Web.HttpGet("http://api.map.baidu.com/location/ip", "ip=" + ip + "&ak=" + ak + "&sn=" + sn + "&coor=" + coor);
                var L = res.Ex_ToEntity<Location>();
                return L;
            }
        }
    }
}
