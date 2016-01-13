using System;
using System.Web;

using System.IO;
using System.Configuration;
using System.Drawing;
using System.Net;

namespace Wbs.Everdigm.Web.mobile
{
    /// <summary>
    /// 调用Google静态地图的类
    /// </summary>
    public class StaticGoogleMap
    {
        /// <summary>
        /// 生成图片的宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 生成图片的高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 下载Google Map的静态图片并写入response
        /// </summary>
        /// <param name="target"></param>
        public void Chart(Stream target)
        {
            var url = ConfigurationManager.AppSettings["GoogleMapsStatic"]
                .Replace('#', '&').Replace("_center_", string.Format("{0},{1}", Latitude, Longitude))
                .Replace("_size_", string.Format("{0}x{1}", Width, Height));

            HttpWebRequest hwr;
            WebResponse resp;
            Bitmap map;
            try
            {
                hwr = (HttpWebRequest)WebRequest.Create(url);
                hwr.Timeout = 20000;
                resp = hwr.GetResponse();
                Stream stream = resp.GetResponseStream();
                map = new Bitmap(stream);
            }
            catch(Exception e)
            {
                map = new Bitmap(Width, Height);
                Graphics g = Graphics.FromImage(map);
                g.Clear(Color.White);
                string error = "Can not access Google map service.\r" + e.Message;
                //Font f = new Font("Arial", 8);
                //SolidBrush b = new SolidBrush(Color.Black);
                Image image = Image.FromFile(HttpContext.Current.Server.MapPath("/images/icon_warning.png"));
                float x = (Width - 100) / 2;
                float y = (Height - 100) / 2;
                g.DrawImage(image, x, y, 100, 100);
            }
            map.Save(target, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}