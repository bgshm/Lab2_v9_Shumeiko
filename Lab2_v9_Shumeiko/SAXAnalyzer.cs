using System.Xml;

namespace Lab2_v9_Shumeiko
{
    public class SAXAnalyzer : IAnalyzer
    {
        public List<string> Search(string filePath, List<(string Attribute, string Keyword)> searchCriteria)
        {
            var results = new List<string>();
            using (var reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
                    {
                        bool allAttributesMatch = true;

                        // Перевіряємо, чи всі атрибути збігаються
                        foreach (var (attribute, keyword) in searchCriteria)
                        {
                            string? value = reader.GetAttribute(attribute);
                            if (string.IsNullOrEmpty(value) || !value.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                            {
                                allAttributesMatch = false;
                                break;
                            }
                        }

                        // Якщо всі атрибути збігаються, додаємо вузол як XML-рядок
                        if (allAttributesMatch)
                        {
                            var elementXml = reader.ReadOuterXml();
                            if (!string.IsNullOrEmpty(elementXml))
                            {
                                results.Add(elementXml);
                            }
                        }
                    }
                }
            }
            return results;
        }
    }


}
