
namespace WuKeSong.Interfaces
{
    public interface IDataService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">An integration engine message that locates a new patient encounter</param>
        /// <returns>An xml serialization of patient and encounter objects</returns>
        string GetPatientAndEncounterInfo(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">An integration engine message that locates a new event and its report</param>
        /// <returns>An xml serialization of event and report objects</returns>
        string GetEventAndReportInfo(string message);
    }
}