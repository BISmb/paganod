using System;

namespace Paganod.Web.Client.Store.ConnectorUseCase.Actions;

public class FetchConnectorsActionResults
{
    public readonly ConnectorDto[] Connectors;

    public FetchConnectorsActionResults(ConnectorDto[] prmConnectors)
    {
        Connectors = prmConnectors;
    }
}

