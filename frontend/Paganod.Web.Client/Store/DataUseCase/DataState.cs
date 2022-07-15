using Fluxor;

using Paganod.Web.Client.Types;

using Paganod.Api.Shared.Feature.Config.Schema.Responses;
using Paganod.Shared.Types;

namespace Paganod.Web.Store.DataUseCase;

[FeatureState]
public record DataState : StateWithBackgroundProcessing
{
    //public SchemaModelDto? CurrentViewedSchema { get; init; } // todo: replace with a FormModel dto or ViewModel Dto
    public ICollection<SchemaModelDto>? ViewingSchemaModels { get; init; }
    public ICollection<Record> CurrentRecords { get; init; }
    public Record CurrentRecord { get; init; }

    private DataState() 
    {
        ViewingSchemaModels = new List<SchemaModelDto>();
        CurrentRecords = new List<Record>();
    }
}