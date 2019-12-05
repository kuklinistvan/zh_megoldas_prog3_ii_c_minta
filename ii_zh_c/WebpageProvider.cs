using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ii_zh_c
{
    class WebpageProvider : IWebpageProvider
    {
        public XDocument Download(string url)
        {
            return XDocument.Load(url);
        }
    }
}
