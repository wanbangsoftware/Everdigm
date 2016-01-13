using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Wbs.Label.Printer
{
    public class TscLib
    {
        /// <summary>
        /// DLL 保存的文件名。
        /// </summary>
        private static string DLL_NAME = "TSCLib.dll";
        /// <summary>
        /// x86版本
        /// </summary>
        private static string x86dll = "TSCLIBx86.dll";
        /// <summary>
        /// x64版本
        /// </summary>
        private static string x64dll = "TSCLIBx64.dll";
        /// <summary>
        /// x64版本驱动文件长度
        /// </summary>
        private static long x64size = 81408;
        /// <summary>
        /// x86版本驱动文件长度
        /// </summary>
        private static long x86size = 74240;

        static TscLib() { SaveDll(); }
        ~TscLib() { }
        /// <summary>
        /// 辨别操作系统是否为x64
        /// </summary>
        private static bool x64 { get { return Environment.Is64BitOperatingSystem; } }
        /// <summary>
        /// 获取dll的应有长度
        /// </summary>
        private static long size { get { return x64 ? x64size : x86size; } }
        /// <summary>
        /// 将资源文件中的 DLL 保存到本地目录中备用。
        /// </summary>
        private static void SaveDll()
        {
            // 设置默认保存 DLL 文件的路径
            string savePath = Environment.SystemDirectory + "\\" + DLL_NAME;
            if (File.Exists(savePath))
            {
                FileStream f = new FileStream(savePath, FileMode.Open);
                long len = f.Length;
                f.Close();
                // 如果已存在的文件长度跟最新版本的不一样则删除该文件
                if (len != size)
                {
                    File.Delete(savePath);
                }
            }
            // 重新保存文件
            if (!File.Exists(savePath))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                // 读取资源中的数据
                Stream s = assembly.GetManifestResourceStream(string.Format("Wbs.Label.Printer.{0}", (x64 ? x64dll : x86dll)));
                // 将资源中的数据保存到数组中
                byte[] b = new byte[s.Length];
                try
                {
                    s.Read(b, 0, (int)s.Length);
                    // 创建一个新的文件，如果存在则重写该文件
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    // 将资源中的 excel 数据写入到流中并保存
                    fs.Write(b, 0, b.Length);
                    fs.Flush();
                    fs.Close();
                }
                finally
                {
                    b = null;
                    s.Close();
                }
            }
        }
        /// <summary>
        /// 显示DLL版本号码
        /// </summary>
        /// <returns></returns>
        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern int about();

        /// <summary>
        /// 打开打印机端口。
        /// </summary>
        /// <param name="printername">打印机名称。可以使用“\\computer(or ip)\printername”的方式访问网络打印机。</param>
        /// <returns></returns>
        [DllImport("TSCLib.dll", EntryPoint = "openport")]
        public static extern int openport(string printername);

        /// <summary>
        /// 关闭已经打开的打印机端口。
        /// </summary>
        /// <returns></returns>
        [DllImport("TSCLib.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        /// <summary>
        /// 设置要打印的标签基本参数。
        /// </summary>
        /// <param name="width">标签宽度，单位mm。</param>
        /// <param name="height">标签高度，单位mm。</param>
        /// <param name="speed">打印速度。</param>
        /// <param name="density">设定打印浓度。0~15，数字愈大打印结果愈黑</param>
        /// <param name="sensor">设定使用感应器类别</param>
        /// <param name="vertical">打印间距，单位mm。</param>
        /// <param name="offset">打印偏移量，单位mm。</param>
        /// <returns></returns>
        [DllImport("TSCLib.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height, string speed, string density, string sensor, string vertical, string offset);

        /// <summary>
        /// 在指定区域打印一个条形码。
        /// </summary>
        /// <param name="x">条形码X方向起始点，以点(point)表示。(200 DPI，1点=1/8 mm, 300 DPI，1点=1/12 mm)</param>
        /// <param name="y">条形码Y方向起始点，以点(point)表示。(200 DPI，1点=1/8 mm, 300 DPI，1点=1/12 mm)</param>
        /// <param name="type">条形码类型。</param>
        /// <param name="height">设定条形码高度，高度以点来表示</param>
        /// <param name="readable">设定是否打印条形码码文。0: 不打印码文，1: 打印码文</param>
        /// <param name="rotation">设定条形码旋转角度0: 旋转0度；90: 旋转90度；180: 旋转180度；270: 旋转270度</param>
        /// <param name="narrow">宽窄比例。设定条形码窄bar 比例因子，请参考TSPL使用手册</param>
        /// <param name="wide">窄宽比例。设定条形码窄bar 比例因子，请参考TSPL使用手册</param>
        /// <param name="code">条形码内容。</param>
        /// <returns></returns>
        [DllImport("TSCLib.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type, string height, string readable, string rotation, string narrow, string wide, string code);
        
        /// <summary>
        /// 清除当前打印机缓冲区。
        /// </summary>
        /// <returns></returns>
        [DllImport("TSCLib.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        /// <summary>
        /// 打印条形码。
        /// </summary>
        /// <param name="set">设定打印卷标式数</param>
        /// <param name="copy">设定打印卷标份数</param>
        /// <returns></returns>
        [DllImport("TSCLib.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        /// <summary>
        /// 在指定区域打印字符。
        /// </summary>
        /// <param name="x">横向坐标。文字X方向起始点，以点(point)表示。(200 DPI，1点=1/8 mm, 300 DPI，1点=1/12 mm)</param>
        /// <param name="y">纵向坐标。文字Y方向起始点，以点(point)表示。(200 DPI，1点=1/8 mm, 300 DPI，1点=1/12 mm)</param>
        /// <param name="fontheight">字符高度。字体高度，以点(point)表示</param>
        /// <param name="rotation">字符角度。旋转角度，逆时钟方向旋转(0/90/180/270)</param>
        /// <param name="fontstyle">字体格式。字体外形：0->标准(Nomal)；1->斜体(Italic)；2->粗体(Bold)；3->粗斜体(Bold and Italic)</param>
        /// <param name="fontunderline">下划线。0-> 无底线 1-> 加底线</param>
        /// <param name="szFaceName">字体名称。字体名称。如: Arial, Times new Roman, 细名体, 标楷体</param>
        /// <param name="content">打印文字内容。</param>
        /// <returns></returns>
        [DllImport("TSCLib.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight, int rotation, int fontstyle, int fontunderline, string szFaceName, string content);
    }
}
