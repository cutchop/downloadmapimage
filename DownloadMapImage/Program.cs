/******
 * Description: 用于下载Bing地图图片
 * Creator:     伍凯 2011-3-4
 ******/
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace DownloadMapImage
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            int e = 0;
            while (true)
            {
                Console.Write("请输入起始位置:");
                try
                {
                    i = Convert4to10(Console.ReadLine());
                }
                catch { i = -1; }
                if (i >= 0) break;
                else Console.WriteLine("输入错误");
            }
            while (true)
            {
                Console.Write("请输入终止位置:");
                try
                {
                    e = Convert4to10(Console.ReadLine());
                }
                catch { e = -1; }
                if (e >= i) break;
                else Console.WriteLine("输入错误");
            }
            Console.WriteLine("开始下载从{0}到{1}的图片...", i, e);
            int t = e - i;
            DateTime startTime = DateTime.Now;
            for (; i <= e; i++)
            {
                string r = Convert(i);
                //if (r.StartsWith("12") || r.StartsWith("13"))//中国范围的图片以12,13开头
                //if (r.StartsWith("1302") || r.StartsWith("1303") || r.StartsWith("1320") || r.StartsWith("1321"))
                //{
                    string f = Break(r);
                    if (!Directory.Exists(string.Format("G:/map/{0}", f.Substring(0, f.Length - 1))))
                    {
                        Directory.CreateDirectory(string.Format("G:/map/{0}", f.Substring(0, f.Length - 1)));
                    }
                    try
                    {
                        if (!File.Exists(string.Format("G:/map/{0}.png", f)))
                        {
                            Console.WriteLine(string.Format("下载{0}.png到G:/map/{1}.png {2}%", r, f, Math.Round((double)(t - e + i) / t * 100), 2));
                            WebClient wc = new WebClient();
                            wc.DownloadFile(string.Format("http://r0.tiles.ditu.live.com/tiles/r{0}.png?g=69", r), string.Format("G:/map/{0}.png", f));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("G:/map/{0}.png file is exist {1}%", f, Math.Round((double)(t - e + i) / t * 100), 2));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                //}
                //else
                //{
                //    Console.WriteLine(r + ".png not download  " + i);
                //}
            }
            Console.WriteLine("下载完毕,总共下载{0}张图片,耗时{1}分钟", t, (DateTime.Now - startTime).TotalMinutes);
            while (true)
            {
                Console.ReadLine();
            }
        }

        //十进制转换成四进制字符串
        private static string Convert(int n)
        {
            string r = "";
            while (n >= 4)
            {
                r = (n % 4).ToString() + r;
                n = n / 4;
            }
            r = n + r;
            return r;
        }
        //四进制转换成十进制字符串
        private static int Convert4to10(string n)
        {
            int r = 0;
            for (int i = 0; i < n.Length; i++)
            {
                if (n.Length - 1 - i == 0)
                {
                    r += int.Parse(n.Substring(i, 1));
                }
                else
                {
                    r += int.Parse(n.Substring(i, 1)) * (2 << (((n.Length - 1 - i) * 2) - 1));
                }
            }
            return r;
        }

        //将字符串用‘/’分隔
        private static string Break(string s)
        {
            string r = "";
            for (int i = 0; i < s.Length; i++)
            {
                r = r + s.Substring(i, 1) + "/";
            }
            return r.Remove(r.Length - 1, 1);
        }
    }
}
