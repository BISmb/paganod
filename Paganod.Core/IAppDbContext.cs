using System.Data;
using System.Data.Common;

namespace Paganod.Core;

public interface IAppContext
{
    DbConnection GetDatabaseConnector();
}

public class DataConnection : IDataConnector
{

}

public interface IDataConnector
{

}