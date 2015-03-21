using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wbs.Utilities
{
    public static class Win32
    {
        private static string GetDateTime()
        {
            return System.DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ");
        }

        /// <summary>
        /// 输出debug信息
        /// </summary>
        /// <param name="str"></param>
        public static void Debug(String str)
        {
            Console.WriteLine(GetDateTime() + str);
        }
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowMessage(string msg)
        {
            ShowMessage(msg, "提示信息！");
        }
        /// <summary>
        /// 显示信息并指定窗口标题内容
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        public static void ShowMessage(string msg, string title)
        {
            ShowMessage(msg, title, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 显示信息并指定窗口标题内容和提示小图片
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="mbi"></param>
        public static void ShowMessage(string msg, string title, MessageBoxIcon mbi)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, mbi);
        }
        /// <summary>
        /// 获取当前系统的时间戳。
        /// </summary>
        /// <returns>返回当前系统的时间戳。</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int GetTickCount();
        /// <summary>
        /// 暂停主程序的运行，但此期间主程序可以响应和处理其他消息（不是直接暂停主程序）。
        /// </summary>
        /// <param name="delay">暂停的时间，单位毫秒。</param>
        public static void TimeDelay(int delay)
        {
            int StartTime;
            StartTime = GetTickCount();
            do
            {
                System.Windows.Forms.Application.DoEvents();

            } while ((GetTickCount() - StartTime < delay));
        }
        /// <summary>
        /// 获取客户端操作系统类型。
        /// </summary>
        /// <returns>返回客户端所在计算机的操作系统类型。</returns>
        public static string GetOperatingSystemName()
        {
            string OSName = null;
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            switch (osInfo.Platform)
            {
                case PlatformID.Unix:
                    OSName = "Unix";
                    break;
                case PlatformID.Win32NT:
                    switch (osInfo.Version.Major)
                    {
                        case 3:
                            OSName = "Windows NT 3.51";
                            break;
                        case 4:
                            OSName = "Windows NT 4.0";
                            break;
                        case 5:
                            switch (osInfo.Version.Minor)
                            {
                                case 0:
                                    OSName = "Windows 2000";
                                    break;
                                case 1:
                                    OSName = "Windows XP";
                                    break;
                                case 2:
                                    OSName = "Windows 2003";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 6:
                            switch (osInfo.Version.Minor)
                            {
                                case 0:
                                    OSName = "Windows Vista";
                                    break;
                                case 1:
                                    OSName = "Windows 7";
                                    break;
                            }
                            break;
                        //case 7:
                        //    OSName = "Windows 7";
                        //    break;
                        default:
                            OSName = "Unknown Win32 NT Windows";
                            break;
                    }
                    break;
                case PlatformID.Win32S:
                    break;
                case PlatformID.Win32Windows:
                    switch (osInfo.Version.Major)
                    {
                        case 0:
                            OSName = "Windows 95";
                            break;
                        case 10:
                            if (osInfo.Version.Revision.ToString() == "2222A")
                                OSName = "Windows 98 Second Edition";
                            else
                                OSName = "Windows 98";
                            break;
                        case 90:
                            OSName = "Windows ME";
                            break;
                        default:
                            OSName = "Unknown Win32 Windows";
                            break;
                    }
                    break;
                case PlatformID.WinCE:
                    OSName = "Windows CE";
                    break;
                case PlatformID.MacOSX:
                    OSName = "Mac OS X";
                    break;
                case PlatformID.Xbox:
                    OSName = "Xbox";
                    break;
                default:
                    break;
            }
            if (osInfo.ServicePack != null)
                OSName += " " + osInfo.ServicePack;

            return OSName;// + string.Format(" ({0})", osInfo.VersionString);
        }
    }
}
