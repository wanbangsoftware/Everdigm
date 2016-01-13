using System.Web;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 错误信息的图画
    /// </summary>
    public class ErrorChart
    {
        private string _msg;
        /// <summary>
        /// 生成一个 ErrorChart 实体并指定错误信息
        /// </summary>
        /// <param name="msg"></param>
        public ErrorChart(string msg)
        {
            _msg = msg;
        }
        private int _width = 230;
        /// <summary>
        /// 图像宽度
        /// </summary>
        public int Width
        {
            set
            {
                if (value > 0)
                    _width = value;
            }
        }
        private int _height = 36;
        /// <summary>
        /// 设置图像高度
        /// </summary>
        public int Height
        {
            set
            {
                if (value > 0)
                    _height = value;
            }
        }
        /// <summary>
        /// 生成图片并写入流
        /// </summary>
        /// <param name="target"></param>
        public void Chart(Stream target)
        {
            Bitmap b = new Bitmap(_width, _height);
            Graphics g = Graphics.FromImage(b);
            // 填充白色背景
            g.Clear(Color.White);
            string path = HttpContext.Current.Server.MapPath("../images/error.png");
            Image img = Image.FromFile(path);

            g.DrawImage(img, 10, (_height - img.Height) / 2);

            // 写字
            Font f = new Font("Arial", 9);
            Brush br = new SolidBrush(Color.FromArgb(0x00, 0x99, 0xCC));
            int fheight = (int)g.MeasureString(_msg, f).Height;
            g.DrawString(_msg, f, br, (10 + img.Width + 5), (_height - fheight) / 2);

            b.Save(target, ImageFormat.Png);
            img.Dispose();
            br.Dispose();
            f.Dispose();
            g.Dispose();
            b.Dispose();
        }
    }
}