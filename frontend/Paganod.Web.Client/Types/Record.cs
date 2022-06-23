namespace Paganod.Web.Client.Types;

public record Record
{
    public string TableName { get; init; }
    // public Guid Id { get; init; }

    public IEnumerable<string> Fields => _InternalData.Keys;
    public IDictionary<string, object> Data => _InternalData;

    public object this[string key]
    {
        get => Get(key);
        set => Set(key, value);
    }

    private IDictionary<string, object> _InternalData;

    private Record()
    {
        _InternalData = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
    }

    public Record(string tableName)
        : this()
    {
        TableName = tableName.ToLower();
    }

    public Record(string tableName, IDictionary<string, object> data)
        : this(tableName)
    {
        if (data is not null)
            _InternalData = data.ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);
    }

    public T Get<T>(string colName)
    {
        if (_InternalData.ContainsKey(colName))
            return (T)Get(colName);

        return default(T);
    }

    public void Set(string colName, object colValue)
    {
        if (_InternalData.ContainsKey(colName))
            _InternalData[colName] = colValue;
        else
            _InternalData.Add(colName, colValue);
    }

    public object? Get(string colName)
    {
        if (_InternalData.ContainsKey(colName))
            return _InternalData[colName];

        return default;
    }
}