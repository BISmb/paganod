using Fluxor;

namespace Paganod.Web.Store.AppUseCase;

[FeatureState]
public record LoadingState
{
    public bool IsLoading { get; init; }
    public bool DisableUserInput { get; init; } // If true will show a full page loader that covers the user's screen
    public string StatusMessage { get; init; }

    public LoadingState() 
    {
        IsLoading = false;
        DisableUserInput = false;
    }
}