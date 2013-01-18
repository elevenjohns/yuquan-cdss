
namespace WuKeSong.Interfaces
{
    public interface IFactService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns>An xml serialization of a list of Fact objects</returns>
        string GetFactsFromReport(string url);
    }
}