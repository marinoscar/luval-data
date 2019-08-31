namespace luval.data
{
    public interface ISqlDialectProvider<T>
    {
        T Entity { get; }
        SqlTableSchema Schema { get; }
        string GetCreateCommand();
        string GetReadCommand();
        string GetUpdateCommand();
        string GetDeleteCommand();
        string GetReadAllCommand();
    }
}