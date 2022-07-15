using System;
using System.Text.Json.Serialization;
using Fluxor;
using Paganod.Web.Client.Types;

namespace Paganod.Web.Client.Store.ConnectorUseCase;

[FeatureState]
public record ConnectorState : StateWithBackgroundProcessing
{
    public ICollection<ConnectorDto> Connectors { get; init; }

    public ConnectorState()
    {
        Connectors = new List<ConnectorDto>();
    }
}

public class ConnectorDto
{
    public Guid ConnectorId { get; init; }
    public string Type { get; init; }
    public string ConnectorJson { get; init; }
}