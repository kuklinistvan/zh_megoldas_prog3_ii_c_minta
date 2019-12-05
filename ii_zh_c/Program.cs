using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_c
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XML letöltése...");
            IList<ImageData> data = new ImageCollector(new WebpageProvider()).GetImages("http://users.nik.uni-obuda.hu/cseri/zh2_gyakorlo/simplepage.html");
            
            ColorParallelDownloader downloader = new ColorParallelDownloader();
            data.ToList().ForEach(d => downloader.QueveDownload(d));
            downloader.StartAllThen(downloads =>
            {
                downloads.ToList().ForEach(d => Process.Start(d.FileName));
            });

            Console.ReadLine();
        }

        
    }
}
