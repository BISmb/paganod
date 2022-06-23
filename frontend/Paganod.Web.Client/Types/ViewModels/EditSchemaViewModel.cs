using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Paganod.Types.Base.Paganod;
using PropertyChanged;

namespace Paganod.Web.Client.Types.ViewModels;

public record EditSchemaFormViewModel : EditSchemaViewModel, INotifyPropertyChanged
{
    public bool ChangesDetected { get; private set; }

    public EditSchemaFormViewModel()
    { }

    public void StartTracking()
    {
        Console.WriteLine("Start tracking changes");

        PropertyChanged += (_, _) => ChangesDetect();
        Columns.CollectionChanged += (_, _) => ChangesDetect();

        foreach(var col in Columns)
            col.PropertyChanged += (_, _) => ChangesDetect();
    }

    private void ChangesDetect()
    {
        Console.WriteLine("Changes were detected");
        ChangesDetected = true;
        // FormChangesDetected?.Invoke(this, null);
    }

}

public record EditSchemaViewModel : INotifyPropertyChanged
{
    public Guid SchemaModelId { get; set; }
    public Guid SolutionId { get; set; }
    public string TableName { get; set; }
    public string PrimaryKeyName { get; set; }
    public DbType PrimaryKeyType { get; set; }

    public ObservableCollection<EditSchemaColumnViewModel> Columns { get; set; }

    public EditSchemaViewModel()
    {
        Columns = new ObservableCollection<EditSchemaColumnViewModel>();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void AddNewColumn()
    {
        var newColumn = new EditSchemaColumnViewModel();
        Columns.Add(newColumn);
    }
}

public class EditSchemaColumnViewModel : INotifyPropertyChanged
{
    public Guid ColumnId { get; set; }
    //public string DisplayName { get; set; }
    public string Name { get; set; }
    public FormFieldType Type { get; set; }
    public IDictionary<string, string> Options { get; set; } // TODO: change this to string and interpret it where needed

    public bool ReadOnly { get; init; }
    public bool IsDeleted { get; set; }

    public EditSchemaColumnViewModel()
    {
        ColumnId = Guid.Empty;
        Options = new Dictionary<string, string>();
    }

    public EditSchemaColumnViewModel(string colName, FormFieldType type) // add design type as a parameter for this constructor
        : this()
    {
        Name = colName;
        Type = type;
    }

    public EditSchemaColumnViewModel(string colName, FormFieldType type, Guid columnId = default, params (string, string)[] options) // add design type as a parameter for this constructor
        : this(colName, type)
    {
        ColumnId = columnId;

        foreach (var option in options)
            Options.Add(option.Item1, option.Item2);
    }

    public EditSchemaColumnViewModel(string colName, FormFieldType type, Guid columnId, IDictionary<string, string> options) // add design type as a parameter for this constructor
        : this(colName, type, columnId, options.Select(x => (x.Key, x.Value)).ToArray())
    {
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}