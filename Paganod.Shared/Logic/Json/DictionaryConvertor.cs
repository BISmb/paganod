using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Paganod.Shared.Logic.Json;

public class DictionaryConvertor : JsonConverter<Dictionary<string, object>>
{
    public override Dictionary<string, object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException($"JsonTokenType was of type {reader.TokenType}, only objects are supported");

        var dictionary = new Dictionary<string, object>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return dictionary;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException("JsonTokenType was not PropertyName");

            var propertyName = reader.GetString();
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new JsonException("Failed to get property name");

            ObjectToInferredTypesConverter objConverter = new ObjectToInferredTypesConverter();
            var val = objConverter.Read(ref reader, typeToConvert, options);
            dictionary.Add(propertyName, val);
        }

        return dictionary;
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<string, object> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var kvp in value)
        {
            writer.WritePropertyName(kvp.Key);
            writer.WriteStringValue(kvp.Value.ToString());
        }


        writer.WriteEndArray();

        //JsonSerializer.Serialize(writer, value, options);

    }
}