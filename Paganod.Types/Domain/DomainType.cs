using Paganod.Types.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Types.Domain;

public abstract record DomainType : IDataRecord, INotifyPropertyChanged
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }


    public event PropertyChangedEventHandler PropertyChanged;
}
