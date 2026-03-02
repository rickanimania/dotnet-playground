namespace DeferredExecutionMaterialization.Core.Abstractions;

public interface IMaterializationDataSource
{
    int TotalRecords { get; }

    int BadMaterializedRecords { get; }
    int GoodMaterializedRecords { get; }

    void ExecuteBad();
    void ExecuteGood();
}