
namespace Lab2_v9_Shumeiko
{
    public class XMLProcessor
    {
        private IAnalyzer _strategy;

        public XMLProcessor(IAnalyzer strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IAnalyzer strategy)
        {
            _strategy = strategy;
        }

        public List<string> ExecuteSearch(string filePath, List<(string Attribute, string Keyword)> searchCriteria)
        {
            return _strategy.Search(filePath, searchCriteria);
        }

    }
}
