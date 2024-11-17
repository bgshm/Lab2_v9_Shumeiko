using System.Xml.Xsl;
using System.Xml;

namespace Lab2_v9_Shumeiko
{
    public static class HTMLTransformer
    {
        public static void Transform(string xmlFilePath, string xslFilePath, string outputHtmlPath)
        {
            try
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xslFilePath);

                using (XmlReader xmlReader = XmlReader.Create(xmlFilePath))
                using (XmlWriter writer = XmlWriter.Create(outputHtmlPath, xslt.OutputSettings))
                {
                    xslt.Transform(xmlReader, writer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during transformation: {ex.Message}");
                throw;
            }
        }
    }
}
