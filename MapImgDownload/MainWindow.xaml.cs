using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace MapImgDownload
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (button1.Content.ToString() == "暂停")
            {
                textBox1.IsEnabled = true;
                textBox2.IsEnabled = true;
                return;
            }
            int i = 0;
            int end = 0;
            if(textBox1.Text.Trim()=="")
            {
                MessageBox.Show("请输入起始位置:");
            }
            try
            {
                i = Convert4to10(textBox1.Text.Trim());
            }
            catch
            {
                MessageBox.Show("起始位置错误");
                return;
            }
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入终止位置:");
            }
            try
            {
                end = Convert4to10(textBox2.Text.Trim());
            }
            catch
            {
                MessageBox.Show("起始终止错误");
                return;
            }
            if (end < i)
            {
                MessageBox.Show("终止位置必须大于起始位置");
                return;
            }
            textBox1.IsEnabled = false;
            textBox2.IsEnabled = false;
            button1.Content = "暂停";
            for (; i <= end; i++)
            {
                string r = Convert(i);
                //if (r.StartsWith("12") || r.StartsWith("13"))//中国范围的图片以12,13开头
                //if (r.StartsWith("1302") || r.StartsWith("1303") || r.StartsWith("1320") || r.StartsWith("1321"))
                //{
                    string f = Break(r);
                    if (!Directory.Exists(string.Format("E:/map/{0}", f.Substring(0, f.Length - 1))))
                    {
                        Directory.CreateDirectory(string.Format("E:/map/{0}", f.Substring(0, f.Length - 1)));
                    }
                    try
                    {
                        if (!File.Exists(string.Format("E:/map/{0}.png", f)))
                        {
                            Console.WriteLine(string.Format("下载{0}.png到E:/map/{1}.png {2}", r, f, i));
                            WebClient wc = new WebClient();
                            wc.DownloadFile(string.Format("http://r0.tiles.ditu.live.com/tiles/r{0}.png?g=69", r), string.Format("E:/map/{0}.png", f));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("E:/map/{0}.png file is exist {1}", f, i));
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
            Console.WriteLine("下载完毕");
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
