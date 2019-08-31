namespace luval.data
{
    public interface ISqlDialectProvider
    {
        object Entity { get; }
        SqlTableSchema Schema { get; }
        string GetCreateCommand();
        string GetReadCommand();
        string GetUpdateCommand();
        string GetDeleteCommand();
    }
}