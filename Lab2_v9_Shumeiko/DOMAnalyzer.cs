using System.Xml;

namespace Lab2_v9_Shumeiko
{
    public class DOMAnalyzer : IAnalyzer
    {
        public List<string> Search(string filePath, List<(string Attribute, string Keyword)> searchCriteria)
        {
            var results = new List<string>();
            var document = new XmlDocument();
            document.Load(filePath);

            var elements = document.GetElementsByTagName("*");
            foreach (XmlNode node in elements)
            {
                if (node.Attributes != null)
                {
                    bool allAttributesMatch = true;

                    // Перевіряємо, чи всі атрибути збігаються
                    foreach (var (attribute, keyword) in searchCriteria)
                    {
                        var attr = node.Attributes[attribute];
                        if (attr == null || !attr.Value.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        {
                            allAttributesMatch = false;
                            break;
                        }
                    }

                    // Якщо всі атрибути збігаються, додаємо вузол як XML-рядок
                    if (allAttributesMatch)
                    {
                        results.Add(node.OuterXml);
                    }
                }
            }
            return results;
        }
    }


}
