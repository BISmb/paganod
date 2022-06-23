using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Paganod.Web.Features.Config.Schema.Components;

public partial class ScheduleMigrationModal : FluxorComponent
{
    [Parameter]
    public Guid ForwardMigrationId { get; set; }

    [Parameter]
    public Guid SunsetMigrationId { get; set; }

    private ScheduleMigrationFormModel FormModel { get; set; }
    private class ScheduleMigrationFormModel : System.ComponentModel.INotifyPropertyChanged
    {
        public Guid ForwardMigrationId { get; set; }
        public Guid SunsetMigrationId { get; set; }

        public DateTime? ForwardMigrationScheduledTime { get; set; }
        public DateTime? SunsetMigrationScheduledTime { get; set; }

        public bool ForwardMigrationScheduleNow { get; set; }
        public bool SunsetMigrationScheduleNow { get; set; }

        public ScheduleMigrationFormModel(Guid forwardMigrationId, Guid sunsetMigrationId = default)
        {
            ForwardMigrationId = forwardMigrationId;
            SunsetMigrationId = sunsetMigrationId;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    protected override Task OnParametersSetAsync()
    {
        FormModel = new ScheduleMigrationFormModel(ForwardMigrationId, SunsetMigrationId);
        return base.OnParametersSetAsync();
    }

    private void ToggleScheduleNow(Guid migrationId)
    {
        if (migrationId == FormModel.ForwardMigrationId)
        {
            // FormModel.ForwardMigrationScheduledTime = null;
            FormModel.ForwardMigrationScheduleNow = !FormModel.ForwardMigrationScheduleNow;
        } 
        else if (migrationId == FormModel.SunsetMigrationId)
        {
            // FormModel.SunsetMigrationScheduledTime = null;
            FormModel.SunsetMigrationScheduleNow = !FormModel.SunsetMigrationScheduleNow;
            Console.WriteLine($"Sunset Migration Scheduled Now: {FormModel.SunsetMigrationScheduleNow}");
        }
        StateHasChanged();
    }
}