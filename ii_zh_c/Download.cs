using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_c
{
    class Download
    {
        public readonly int id;
        private ImageData target;

        public Download(int id, ImageData target)
        {
            this.id = id;
            this.target = target;
        }

        public string URL
        {
            get => target.URL;
        }

        public string FileName
        {
            get => target.FileName;
        }

        public void DoDownload()
        {
            new WebClient().DownloadFile(target.URL, target.FileName);
        }
    }
}
