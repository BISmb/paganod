using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Paganod.Sql.Utility;
using Paganod.Types.Domain;

namespace Paganod.Sql.Interfaces
{
    /// <summary>
    /// Used to translate database specific schema into Paganod aware schema models
    /// </summary>
    public interface ISchemaTranslator : IDisposable
    {
        public SchemaModel GetSchemaModel(string tableName, bool runRecommendations = false)
            => runRecommendations ? GetSchemaModelWithRecommendations(GetSchemaModelWithoutRecommendations(tableName)) : GetSchemaModelWithoutRecommendations(tableName);
        public IEnumerable<SchemaModel> GetSchemaModels(bool runRecommendations = false)
            => runRecommendations ? GetSchemaModelsWithRecommendations() : GetSchemaModelsWithoutRecommendations();

        /// <summary>
        /// Get a schema model without recommendations for a given table
        /// </summary>
        /// <param name="tableName">The tablename in the database</param>
        /// <returns></returns>
        public SchemaModel GetSchemaModelWithoutRecommendations(string tableName);

        /// <summary>
        /// Get multiple schema models without recommendations for all the tables in the translator's current database connection.
        /// </summary>
        /// <param name="tableName">The tablename in the database</param>
        /// <returns></returns>
        public IEnumerable<SchemaModel> GetSchemaModelsWithoutRecommendations();

        public SchemaModel GetSchemaModelWithRecommendations(SchemaModel sm);
        public IEnumerable<SchemaModel> GetSchemaModelsWithRecommendations(IEnumerable<SchemaModel> lstSchemaModels = null);
        public SchemaMap GetSchemaMap();
    }
}
