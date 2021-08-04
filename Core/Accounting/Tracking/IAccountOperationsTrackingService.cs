namespace Core.Accounting.Tracking
{
    public interface IAccountOperationsTrackingService
    {
        AccountOperationInfoCollection GetOperations();
    }
}
