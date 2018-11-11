using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using Xunit;

namespace MP.AdvancedXML.Validator
{
    public class XmlValidatorTest
    {
        private const string XmlSchemeNamespace= "http://library.by/catalog";
        private const string XmlSchemeFile = @"..\..\Xsd\BooksScheme.xsd";
        private const string XmlFileValid = @"..\..\..\MP.AdvancedXML.Data\books-valid.xml";
        private const string XmlFileInvalid = @"..\..\..\MP.AdvancedXML.Data\books-invalid.xml";

        private List<string> _errorMessages;

        private readonly XmlReaderSettings _xmlReaderSettings;

        public XmlValidatorTest()
        {
            _xmlReaderSettings = new XmlReaderSettings();

            _xmlReaderSettings.Schemas.Add(XmlSchemeNamespace, XmlSchemeFile);
            _xmlReaderSettings.ValidationType = ValidationType.Schema;
            _xmlReaderSettings.ValidationEventHandler += HandleXmlVerificationErrors;
            _xmlReaderSettings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;

            _errorMessages = new List<string>();
        }

        [Fact]
        public void VerifyXmlDocument_ValidDocument()
        {
            var xmlReader = XmlReader.Create(XmlFileValid, _xmlReaderSettings);
            while (xmlReader.Read());

            Assert.True(!_errorMessages.Any());
        }

        [Fact]
        public void VerifyXmlDocument_InvalidDocument()
        {
            var xmlReader = XmlReader.Create(XmlFileInvalid, _xmlReaderSettings);
            while (xmlReader.Read()) ;

            _errorMessages.ForEach(DisplayErrorMessage);

            Assert.True(_errorMessages.Any());
        }

        #region Private methods

        private void HandleXmlVerificationErrors(object sender, ValidationEventArgs args)
        {
            _errorMessages.Add($"Invalid value: {(sender as XmlReader).Name} (L{args.Exception.LineNumber} P{args.Exception.LinePosition})");
        }

        private void DisplayErrorMessage(string message)
        {
            Console.WriteLine(message);
        }

        #endregion

    }
}
