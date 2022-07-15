using System;
namespace Paganod.Types.Domain;

public record Connector : DomainType
{
    


    public Connector()
    {
    }
}

public enum ConnectorType
{
    Data,
    Storage,
}

