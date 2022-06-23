using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Sql.Utility
{
    public class SchemaMap : Dictionary<string, List<string>>
    {
        private Dictionary<string, string> RecordTypeTableMatches { get; set; }

        public SchemaMap() : base(StringComparer.OrdinalIgnoreCase)
        {
            RecordTypeTableMatches = new(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Key: RecordType, Vlue: TableName
        /// </summary>
        /// <param name="matches"></param>
        public void AddRecordTypeTableMatches(Dictionary<string, string> matches)
        {
            foreach (var kvp in matches)
                RecordTypeTableMatches.Add(kvp.Key, kvp.Value);
        }

        public bool ContainsRecordType(string recordType) => RecordTypeTableMatches.ContainsKey(recordType);

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetPrimaryKeyForTable(string tableName)
        {
            var recordType = GetRecordTypeFromTableName(tableName);

            return $"{recordType}_id";
        }

        public bool ContainsTable(string tableName) => this.ContainsKey(tableName);
        public string GetRecordTypeFromTableName(string tableName) => RecordTypeTableMatches.First(x => x.Value == tableName).Value;
        public string GetTableNameFromRecordType(string recordType) => RecordTypeTableMatches[recordType];
        public void AddTable(string TableName) => Add(TableName, new());
        public void AddColumn(string TableName, string ColumnName) => this[TableName].Add(ColumnName);
        public void RemoveTable(string TableName) => Remove(TableName);
        public void RemoveColumn(string TableName, string ColumnName) => this[TableName].Remove(ColumnName);
        public bool ContainsColumn(string tableName, string columnName) => this[tableName].Contains(columnName, StringComparer.OrdinalIgnoreCase);
        public IEnumerable<string> GetTableColumns(string tableName) => this[tableName];
    }
}
