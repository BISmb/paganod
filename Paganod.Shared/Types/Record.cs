using Paganod.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Paganod.Shared.Types;

public record Record
{
    public string Type { get; init; }

    public IEnumerable<string> FieldNames()
    {
        return Data.Keys.ToArray();
    }

    public string[] Fields => Data.Keys.ToArray();
    //public Dictionary<string, object> Data => _InternalDictionary;
    public Guid RecordId
    {
        get => Get<Guid>(RecordIdColumnName);
        set => Set(RecordIdColumnName, value);
    }

    public string RecordIdColumnName => "Id"; //Common.CreatePrimaryKeyColumnNameByRecordTypeProperName(Type);
    public DateTime CreatedOn => Get<DateTime>("CreatedDate");
    public DateTime ModifiedOn => Get<DateTime>("ModifiedOn");

    private List<string> _ChangedProperties { get; set; }
    public Dictionary<string, object> Data { get; set; }

    // default constructor (json)
    private Record()
    {
        Type = "";
        Data = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        _ChangedProperties = new List<string>();
    }

    [JsonConstructor]
    public Record(string type)
        : this()
    {
        //_InternalDictionary.Add(Common.CreatePrimaryKeyColumnNameByRecordTypeProperName(type), Guid.NewGuid());
        Type = type.ToLower();
    }

    public Record(string prmType, IDictionary<string, object> prmData)
        : this(prmType)
    {
        Data.AddRange(prmData);
    }

    public Record(string prmType, IEnumerable<KeyValuePair<string, object>> prmData)
        : this(prmType)
    {
        Data.AddRange(prmData);
    }

    public object? this[string key]
    {
        get => Get(key);
        set => Set(key, value);
    }

    public bool ContainsKey(string keyName)
    {
        return Data.ContainsKey(keyName);
    }

    public object? Get(string key) => Get<object>(key);

    public T? Get<T>(string key)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

        if (!Data.ContainsKey(key) || Data[key] is null)
            return default;

        // this is because sometimes guid is stored as string
        if (Data[key] is string && typeof(T) == typeof(Guid) && (Data[key].ToString()).Length == 36)
            return (T)(object)Guid.Parse(Data[key] as string);
        else
            return (T)Data[key];

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public void Set(string key, object? value)
    {
        key = key.ToLower();

        // this is to prevent changes appearing for new instances
        if (!_ChangedProperties.Contains(key))
            _ChangedProperties.Add(key);

        if (value == null)
        {
            Data.Remove(key);
            return;
        }

        if (Data.ContainsKey(key))
            Data[key] = value;
        else
            Data.Add(key, value);
    }

    public void Add(string keyName, object value)
    {
        if (Data.ContainsKey(keyName))
            throw new Exception($"Record already contains {keyName}");

        Data.Add(keyName, value);
    }

    public void AddMany(IEnumerable<KeyValuePair<string, object>> data)
    {
        foreach (var c in data)
            Add(c.Key, c.Value);
    }

    public void AddMany(Dictionary<string, object> data)
    {
        AddMany(data.AsEnumerable());
    }

    public void Remove(string keyName)
    {
        if (!Data.ContainsKey(keyName))
            throw new Exception($"Record does not contains {keyName}");

        Data.Remove(keyName);

        if (_ChangedProperties.Contains(keyName))
            _ChangedProperties.Remove(keyName);
    }

    public Dictionary<string, object> AllData()
    {
        return Data;
    }

    public Dictionary<string, object> ChangedData()
    {
        return Data.Where(x => _ChangedProperties
                                    .Contains(x.Key, StringComparer.OrdinalIgnoreCase))
                                    .ToDictionary(x => x.Key, x => x.Value);
    }
}
