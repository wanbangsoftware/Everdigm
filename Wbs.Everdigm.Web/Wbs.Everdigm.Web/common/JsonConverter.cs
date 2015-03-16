using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
//using System.Web.Script.Serialization;
//using System.Runtime.Serialization.Json;

using Newtonsoft.Json;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// Json 转换工具
    /// </summary>
    public static class JsonConverter
    {
        /// <summary>
        /// 将对象转换成 Json 数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //return jss.Serialize(obj);
            //DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            //StringBuilder sb = new StringBuilder();
            //sb.Clear();
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    json.WriteObject(ms, obj);
            //    sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
            //}
            //return sb.ToString();
        }
        /// <summary>
        /// 将 Json 数据转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
            //DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(T));
            //T jsonObj;
            //using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            //{
            //    jsonObj = (T)dcjs.ReadObject(ms);
            //}
            //return jsonObj;
        }
    }
}