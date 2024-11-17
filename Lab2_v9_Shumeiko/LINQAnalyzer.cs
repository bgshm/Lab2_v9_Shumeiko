using System.Xml.Linq;

namespace Lab2_v9_Shumeiko
{
    public class LINQAnalyzer : IAnalyzer
    {
        public List<string> Search(string filePath, List<(string Attribute, string Keyword)> searchCriteria)
        {
            var results = new List<string>();
            var document = XDocument.Load(filePath);

            var elements = document.Descendants();
            foreach (var element in elements)
            {
                bool allAttributesMatch = true;

                // Перевіряємо, чи всі атрибути збігаються
                foreach (var (attribute, keyword) in searchCriteria)
                {
                    var attr = element.Attribute(attribute);
                    if (attr == null || !attr.Value.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        allAttributesMatch = false;
                        break;
                    }
                }

                // Якщо всі атрибути збігаються, додаємо вузол як XML-рядок
                if (allAttributesMatch)
                {
                    results.Add(element.ToString());
                }
            }
            return results;
        }
    }


}
