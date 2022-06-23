using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Data.Shared;

internal class DbTypeResolver
{
    private static Dictionary<Type, DbType> _TypeMap = new Dictionary<Type, DbType>()
    {
        { typeof(byte), DbType.Byte },
        { typeof(sbyte), DbType.SByte },
        { typeof(short), DbType.Int16 },
        { typeof(ushort), DbType.UInt16 },
        { typeof(int), DbType.Int32 },
        { typeof(uint), DbType.UInt32 },
        { typeof(long), DbType.Int64 },
        { typeof(ulong), DbType.UInt64 },
        { typeof(float), DbType.Single },
        { typeof(double), DbType.Double },
        { typeof(decimal), DbType.Decimal },
        { typeof(bool), DbType.Boolean },
        { typeof(string), DbType.String },
        { typeof(char), DbType.StringFixedLength },
        { typeof(Guid), DbType.Guid },
        { typeof(DateTime), DbType.DateTime },
        { typeof(DateTimeOffset), DbType.DateTimeOffset },
        { typeof(byte[]), DbType.Binary },
        { typeof(byte?), DbType.Byte },
        { typeof(sbyte?), DbType.SByte },
        { typeof(short?), DbType.Int16 },
        { typeof(ushort?), DbType.UInt16 },
        { typeof(int?), DbType.Int32 },
        { typeof(uint?), DbType.UInt32 },
        { typeof(long?), DbType.Int64 },
        { typeof(ulong?), DbType.UInt64 },
        { typeof(float?), DbType.Single },
        { typeof(double?), DbType.Double },
        { typeof(decimal?), DbType.Decimal },
        { typeof(bool?), DbType.Boolean },
        { typeof(char?), DbType.StringFixedLength },
        { typeof(Guid?), DbType.Guid },
        { typeof(DateTime?), DbType.DateTime },
        { typeof(DateTimeOffset?), DbType.DateTimeOffset },
        //{ typeof(Binary), DbType.Binary },
    };

    public static DbType GetDbTypeFromClrType(Type clrType)
    {
        if (_TypeMap.TryGetValue(clrType, out var type))
            return type;

        throw new Exception("DbType could not be resolved.");
    }
}
