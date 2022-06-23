using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Paganod.Shared.Logic.Json;

public static class Json
{
    public static JsonSerializerOptions GetSharedJsonOptions(bool prettify = false)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new DictionaryConvertor());
        options.Converters.Add(new ObjectToInferredTypesConverter());

        //options.Converters.Add(new DictionaryJsonConvertor());

        //options.Converters.Add(null);

        //if (IsDevelopment() | prettify)
        //    options.WriteIndented = true;

        return options;
    }

    public static string SerializeToJson<T>(T value, bool prettify = false)
    {
        var options = GetSharedJsonOptions(prettify);
        string json = JsonSerializer.Serialize<T>(value, options);
        return json;
    }
}