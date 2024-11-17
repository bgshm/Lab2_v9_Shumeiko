
namespace Lab2_v9_Shumeiko
{
    public interface IAnalyzer
    {
        List<string> Search(string filePath, List<(string Attribute, string Keyword)> searchCriteria);
    }


}
