using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ii_zh_c
{
    public class ImageCollector
    {
        private IWebpageProvider webpageProvider;

        public ImageCollector(IWebpageProvider webpageProvider)
        {
            this.webpageProvider = webpageProvider;
        }

        public IList<ImageData> GetImages(string url)
        {
            XDocument xDocument = this.webpageProvider.Download(url);
            IEnumerable<XElement> imageElements = xDocument.Root.Descendants().Where(d => d.Name == "img");
            return imageElements.Select(ie => new ImageData(BaseUrlOf(url) + ie.Attribute("src").Value)).ToList();
        }

        private string BaseUrlOf(string url)
        {
            StringBuilder sb = new StringBuilder();

            string[] splitted = WithEndingSlash(url).Split('/');

            for (int i = 0; i < splitted.Length - 2; i++)
            {
                sb.Append(splitted[i] + "/");
            }

            return sb.ToString();
        }

        private string WithEndingSlash(string url)
        {
            if(url.Last() == '/')
            {
                return url;
            }
            else
            {
                return url + '/';
            }
        }
    }
}
