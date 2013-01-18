using System.ServiceModel;

namespace WuKeSong.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITriggerRuleService" in both code and config file together.
    /// <summary>
    /// When defining Web Services, avoid using non-primitive data types. Because the client side will generate corresponding proxy classes.
    /// Use xml serialization string for complex objects.
    /// </summary>
    [ServiceContract]
    public interface IDecisionSupportService
    {
        [OperationContract]
        void Notify(string message);

        [OperationContract]
        void AddProblem(int encounter_id, int problem_definition_id);

        //[OperationContract]
        //string RetrieveProblemList(int id);

        [OperationContract]
        void UpdateProblemState(int id, string operation);

        [OperationContract]
        bool? ReEvaluate(int id);

        [OperationContract]
        void UpdateLabTestDict();

        [OperationContract]
        void UpdateProblemDict();

        [OperationContract]
        void UpdatePhysiologicalItemDict();

        [OperationContract]
        void SetCurrentUser(string user);

        // Provide as CDS control property
        [OperationContract]
        void SetCurrentEncounter(string id);
    }
}
