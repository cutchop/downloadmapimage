using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Downloadabc
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile("http://emap1.mapabc.com/mapabc/maptile?v=w2.61&z=12&x=3346&y=1681", "e:/map/mapabc.png");
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
