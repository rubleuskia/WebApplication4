namespace DatabaseAccess.Entities.Common
{
    public interface IHaveVersion
    {
        byte[] RowVersion { get; set; }
    }
}