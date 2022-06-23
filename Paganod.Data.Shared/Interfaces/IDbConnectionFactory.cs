using System.Data.Common;

namespace Paganod.Data.Shared.Interfaces;

public interface IDbConnectionFactory
{
    DbConnection NewConnection();
}