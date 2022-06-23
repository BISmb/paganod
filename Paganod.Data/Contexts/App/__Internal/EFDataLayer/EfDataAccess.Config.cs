using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Paganod.Data.App.Internal;

public partial class EfDataAccess : DbContext
{
    internal EfDataAccess(DbContextOptions<EfDataAccess> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("paganod");

        foreach (var dbSetType in GetConfigurableTypes())
        {
            var oEntityBuilder = modelBuilder.Entity(dbSetType);
            ApplyBaseConfigurationPerEntity(ref oEntityBuilder);
        }

        ApplyEntitySpecificConfiguration(ref modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void ApplyEntitySpecificConfiguration(ref ModelBuilder oModelBuilder)
    {
        // ConnectorTemplateTask has many TemplateVariable
        // Matched On: ConnectorTemplateTask.Id = TemplateVariable.ConnectorTemplateTaskId 
        //oModelBuilder.Entity<ConnectorTaskDefinition>()
        //    .HasMany<VariableDefinition>(ctt => ctt.Variables)
        //    .WithOne(tv => tv.ConnectorTemplateTask)
        //    .HasForeignKey(tv => tv.ConnectorTemplateTaskId);

        //// TemplateVariable has many ProcessVariable
        //// Matched On: ProcessVariable.TemplateVariableId = TemplateVariable.Id
        //oModelBuilder.Entity<VariableDefinition>()
        //    .HasMany<ProcessVariable>(tv => tv.ProcessVariables)
        //    .WithOne(pv => pv.TemplateVariable)
        //    .HasForeignKey(pv => pv.TemplateVariableId);

        //oModelBuilder.Entity<ProcessTask>()
        //    .HasMany<ProcessVariable>(pt => pt.Variables)
        //    .WithOne(pv => pv.SourceTask)
        //    .HasForeignKey(pv => pv.ProcessTaskId);
    }

    //public static (string TableName, string PrimaryKeyColumnName) GetTableInfo(string recordName)
    //{
    //    //Pluralizer pluralizer = new Pluralizer();
    //    //string PrimaryKeyColumnName = $"{recordName}Id";
    //    //string TableName = pluralizer.Pluralize(recordName);
    //    //return (TableName, PrimaryKeyColumnName);
    //}


    protected virtual void ApplyBaseConfigurationPerEntity(ref EntityTypeBuilder oEntityBuilder)
    {
        //var oBaseType = oEntityBuilder.Metadata.ClrType;

        //Pluralizer pluralizer = new Pluralizer();
        //IEnumerable<PropertyInfo> lstProperties = oBaseType.GetProperties();


        //string typeName = oBaseType.Name.Remove("record", StringComparison.OrdinalIgnoreCase);
        //string recordName = pluralizer.Singularize(typeName);
        //var tblInfo = GetTableInfo(recordName);

        //// Calculate Database Defaults for Columns

        //// Declare Entity Key
        //if (lstProperties.Any(x => x.Name == tblInfo.PrimaryKeyColumnName))
        //    oEntityBuilder.HasKey(tblInfo.PrimaryKeyColumnName);

        //oEntityBuilder.Property(typeof(Guid), tblInfo.PrimaryKeyColumnName).ValueGeneratedNever(); // prevent primary key from being generated

        //oEntityBuilder.Property(nameof(DataRecord.Id))
        //    .IsRequired(true)
        //    .ValueGeneratedNever();

        //oEntityBuilder.Property(nameof(DataRecord.CreatedOn))
        //    .IsRequired(true);

        //oEntityBuilder.Property(nameof(DataRecord.ModifiedOn))
        //    .IsRequired(true);

        //oEntityBuilder.ToTable(tblInfo.TableName, "paganod");

        //// Bools stored as strings
        //foreach (var property in lstProperties.Where(prop => prop.PropertyType == typeof(bool)))
        //    oEntityBuilder.Property(property.Name).HasConversion(new BoolToStringConverter("FALSE", "TRUE"));

        //// Enums stored as strings
        //foreach (var property in lstProperties.Where(prop => prop.PropertyType.BaseType == typeof(Enum)))
        //    oEntityBuilder.Property(property.Name).HasConversion<string>();
    }

    internal IEnumerable<Type> GetConfigurableTypes()
    {
        List<Type> dbSetTypes = new();

        //var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);

        //foreach (var property in properties)
        //{
        //    var setType = property.PropertyType;

        //    var isDbSet = setType.IsGenericType && (typeof(DbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition()));

        //    if (isDbSet)
        //        dbSetTypes.Add(setType.GenericTypeArguments[0]);
        //}

        return dbSetTypes;
    }
}
