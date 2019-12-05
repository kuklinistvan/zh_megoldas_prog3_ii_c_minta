using ii_zh_c;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using test.Properties;

namespace test
{
    [TestFixture]
    public class ImageCollectorTest
    {
        [Test]
        public void TestImageCollector()
        {
            Mock<IWebpageProvider> providerMock = new Mock<IWebpageProvider>();
            providerMock.Setup(prov => prov.Download(It.IsAny<string>())).Returns(XDocument.Parse(Resources.mock_result_data));

            ImageCollector imCollector = new ImageCollector(providerMock.Object);

            IList<ImageData> images = imCollector.GetImages("http://www.abc.hu/index.html");

            ImageData[] imagesAsArray = images.ToArray();

            Assert.AreEqual(new ImageData("http://www.abc.hu/pic1.bmp"), imagesAsArray[0]);
            Assert.AreEqual(new ImageData("http://www.abc.hu/pic2.bmp"), imagesAsArray[1]);
            Assert.AreEqual(new ImageData("http://www.abc.hu/pic3.bmp"), imagesAsArray[2]);
        }
    }
}
