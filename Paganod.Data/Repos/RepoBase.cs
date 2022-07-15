
using AutoMapper;

using LinqToDB.Extensions;

using Paganod.Data.App;
using Paganod.Data.App.Internal;
using Paganod.Data.Contexts.App;
using Paganod.Data.Repos;
using Paganod.Data.Schema;
using Paganod.Data.Shared.Interfaces.Repos;
using Paganod.Sql.DML;
using Paganod.Types.Base;
using Paganod.Types.Base.Paganod.Schema;
using Paganod.Types.Domain;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Paganod.Data.Repos;

/// <summary>
/// Responsible for persisting and returning domains objects from the under-lying database connection
/// </summary>
/// <typeparam name="TDomainType"></typeparam>
/// <typeparam name="TRecordType"></typeparam>
internal abstract class RepoBase<TDomainType, TRecordType> : RepoBase<TDomainType, TRecordType, Guid>
    where TDomainType : DomainType //, IDataRecord, INotifyPropertyChanged, new()
    where TRecordType : DataRecord //, IDataRecord, new()
{
    internal RepoBase(AppDbContext prmDbContext)
        : base(prmDbContext)
    {

    }
}

public interface IRepoBase<TDomainType, TRecordType> : IExternalRepoBase<TDomainType>
{
        
}

internal abstract class RepoBase<TDomainType, TRecordType, TIdType> : IExternalRepoBase<TDomainType>, IRepoBase<TDomainType, TRecordType>
    where TDomainType : DomainType //, IDataRecord<TIdType>, INotifyPropertyChanged, new()
    where TRecordType : DataRecord //, IDataRecord<TIdType>, new()
    where TIdType : struct
{
    protected AppDbContext Db;
    internal EfDataAccess DbEf;

    protected AutoMapper.Mapper Mapper;

    public RepoBase(AppDbContext prmDbContext)
    {
        Db = prmDbContext;
        DbEf = prmDbContext.DataAccessLayer;

        if (Mapper is null)
        {
            var config = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<TDomainType, TRecordType>();
                cfg.CreateMap<TRecordType, TDomainType>();
            });
            Mapper = new AutoMapper.Mapper(config);
        }

    }

    public virtual void Add(TDomainType domainObject)
    {
        //var record = domainObject.As<TRecordType>();
        var record = GetRecordType(domainObject);
        domainObject.PropertyChanged += DomainObject_PropertyChanged;
        DbEf.Add(record);
    }

    public virtual void AddRange(IEnumerable<TDomainType> domainObjects)
    {
        List<TRecordType> records = new List<TRecordType>();

        foreach (var domainObject in domainObjects)
            records.Add(GetRecordType(domainObject));
        //records.Add(domainObject.As<TRecordType>());

        //var records = domainObjects.AsMany<TRecordType>();
        //db.AddRange(records);
    }

    // This method could technically be an extension method
    public virtual void Remove(Guid id)
    {
        var dObject = Get(id);
        Remove(dObject);
    }

    public virtual void Remove(TDomainType domainObject)
    {
        //var record = domainObject.As<TRecordType>();
        var record = GetRecordType(domainObject);

        var trackedEntity = DbEf.Entry<TRecordType>(record);
        if (trackedEntity is not null)
            trackedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        else
            DbEf.Remove(record);
    }

    public virtual void RemoveRange(IEnumerable<TDomainType> domainObjects)
    {
        var records = GetRecordTypes(domainObjects);
        DbEf.RemoveRange(records);
    }

    public virtual TDomainType Get(Guid id)
    {
        var record = DbEf.Find<TRecordType>(id);

        if (record is null)
            throw new Exception("Record was not found");

        //var domainObject = record.As<TDomainType>();
        var domainObject = GetDomainType(record);
        domainObject.PropertyChanged += DomainObject_PropertyChanged;
        return domainObject;
    }

    //public virtual bool Any(Expression<Predicate<TRecordType>>)
    //{

    //}

    public virtual TDomainType GetFull(Guid id)
    {
        return Get(id);
    }
    public virtual IEnumerable<TDomainType> GetAllFull()
    {
        throw new NotImplementedException();
    }

    // this method is internal to prevent misuse
    //protected internal IEnumerable<TDomainType> GetAll()
    //{
    //    return db.Set<TRecordType>().ToArray().AsMany<TDomainType>();
    //}

    public virtual bool Exists(Guid id)
    {
        return DbEf.Find(typeof(TRecordType), id) is not null;
    }

    protected void DomainObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var domainType = typeof(TDomainType);
        var recordType = typeof(TRecordType);

        if (typeof(TRecordType).GetProperty(e.PropertyName) is null)
            return;

        //// bug??
        ////if (e.PropertyName == nameof(ISchemaMigration.AppliedOn))
        ////    return;

        //var propertyType = sender.GetType().GetProperty(e.PropertyName).PropertyType;

        //if (propertyType.GetInterface(nameof(IEnumerable)) is not null)
        //    if (propertyType != typeof(string))
        //        return;

        TDomainType domainObject = sender as TDomainType;
        TRecordType recordObject = DbEf.Find(recordType, domainObject.Id) as TRecordType;
        //recordObject = GetRecordType(domainObject);

        var efEntry = DbEf.Entry(recordObject);
        efEntry.CurrentValues[e.PropertyName] = domainType.GetProperty(e.PropertyName).GetValue(sender);


        ////recordObject = domainObject.As<TRecordType>(recordObject);

        ////var mappers = ApplicationMap.GetAllRegisterdMappers();

        ////recordObject = domainObject.As(recordObject);\

        //DbEf.ChangeTracker.DetectChanges();

                




        ////ApplicationMap.Map(recordObject, domainObject);
    }

    protected void AddTracking(TDomainType domainObject)
    {
        domainObject.PropertyChanged += DomainObject_PropertyChanged;
    }

    public virtual IEnumerable<TDomainType> GetAll()
    {
        foreach(var record in DbEf.Set<TRecordType>())
            yield return GetDomainType(record);
    }

    public int Count()
    {
        return DbEf.Set<TRecordType>().Count();
    }

    private TRecordType GetRecordType(TDomainType domainObject)
    {
        return Mapper.Map<TRecordType>(domainObject);
    }

    private IEnumerable<TRecordType> GetRecordTypes(IEnumerable<TDomainType> domainObjects)
    {
        foreach (var domainObject in domainObjects)
            yield return GetRecordType(domainObject);
    }

    private TDomainType GetDomainType(TRecordType recordObject)
    {
        return Mapper.Map<TDomainType>(recordObject);
    }

    private IEnumerable<TDomainType> GetDomainTypes(IEnumerable<TRecordType> recordObjects)
    {
        foreach (var recordObject in recordObjects)
            yield return GetDomainType(recordObject);
    }
}
