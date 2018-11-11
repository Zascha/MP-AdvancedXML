using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Xunit;

namespace MP.AdvancedXML.Rss
{
    public class RssTest
    {
        private const string XmlFile = @"..\..\..\MP.AdvancedXML.Data\books-valid.xml";
        private const string XsltFile = @"..\..\Xslt\BooksRss.xslt";
        private const string OutputRssFile = @"..\..\rss-books.xml";

        private const string RssTitle = "Rss title value";
        private const string RssLink = "http://library.by/catalog/rss";
        private const string RssDescription = "Rss description value";

        private readonly XslCompiledTransform _xslCompiledTransform;

        public RssTest()
        {
            _xslCompiledTransform = new XslCompiledTransform();
            _xslCompiledTransform.Load(XsltFile);
        }

        [Fact]
        public void GenerateRssFromXmlFile()
        {
            CreateOutputFileIfNorExists();

            var xsltParams = GetXsltFormedParams();

            using (var xmlWriter = XmlWriter.Create(OutputRssFile))
            {
                _xslCompiledTransform.Transform(XmlFile, xsltParams, xmlWriter);
            }
        }

        #region Private methods

        private void CreateOutputFileIfNorExists()
        {
            if (File.Exists(OutputRssFile))
            {
                var createdFile = File.Create(OutputRssFile);
                createdFile.Close();
            }
        }

        private XsltArgumentList GetXsltFormedParams()
        {
            var xsltParams = new XsltArgumentList();
            xsltParams.AddParam("Title", string.Empty, RssTitle);
            xsltParams.AddParam("Link", string.Empty, RssLink);
            xsltParams.AddParam("Description", string.Empty, RssDescription);

            return xsltParams;
        }

        #endregion
    }
}
