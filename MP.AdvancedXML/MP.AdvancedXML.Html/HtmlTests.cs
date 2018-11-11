
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Xunit;

namespace MP.AdvancedXML.Html
{
    public class HtmlTests
    {
        private const string XmlFile = @"..\..\..\MP.AdvancedXML.Data\books-valid.xml";
        private const string XsltFile = @"..\..\Xslt\BooksHtml.xslt";
        private const string OutputRssFile = @"..\..\books.html";

        private const string HtmlTitle = "Current funds by genre";

        private readonly XslCompiledTransform _xslCompiledTransform;

        public HtmlTests()
        {
            _xslCompiledTransform = new XslCompiledTransform();
            _xslCompiledTransform.Load(XsltFile);
        }

        [Fact]
        public void GenerateHtmlFromXmlFile()
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
            xsltParams.AddParam("Title", string.Empty, HtmlTitle);

            return xsltParams;
        }

        #endregion
    }
}
