namespace Paganod.Web.Client.Types;

public abstract record StateWithBackgroundProcessing
{
    public string StatusMessage { get; set; }
    public bool IsLoading { get; set; }

    internal StateWithBackgroundProcessing(string message = "", bool isLoading = false)
    {
        StatusMessage = message;
        IsLoading = isLoading;
    }
}