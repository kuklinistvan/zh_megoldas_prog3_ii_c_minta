using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_c
{
    public struct ImageData
    {
        public readonly string FileName;
        public readonly string URL;
        
        public ImageData(string fullUrlToTheImage)
        {
            // pl. https://www.images.com/path/to/the/image.png
            this.URL = fullUrlToTheImage;
            this.FileName = fullUrlToTheImage.Split('/').Last();
        }

        public override bool Equals(object obj)
        {
            if(!(obj is ImageData))
            {
                return false;
            }

            ImageData other = (ImageData)obj;

            return other.FileName == this.FileName
                && other.URL == this.URL;
        }

        public override string ToString()
        {
            return FileName + " (" + URL + ")";
        }
    }
}
