using Microsoft.EntityFrameworkCore;

using OfficeOpenXml;

using Paganod.Data.App.Internal;
using Paganod.Data.Contexts.App;
using Paganod.Shared.Types;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Paganod.Data.App;

internal class _ExcelDbContext : AppDbContext
{
    private readonly string _ExcelFilePath;

    public _ExcelDbContext(string excelFilePath)
        : base(_ExcelDbContext.GetInMemoryEntityFrameworkOptions())
    {
        _ExcelFilePath = excelFilePath;
    }

    private static DbContextOptions<EfDataAccess> GetInMemoryEntityFrameworkOptions()
    {
        var dbOptions = new DbContextOptionsBuilder<EfDataAccess>()
                                        .UseSqlite("Data Source=c:\\users\\marty\\desktop\\paganod.db;Mode=Memory;Cache=Shared")
                                        .Options;

        return dbOptions;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var numberOfRecords = await DataAccessLayer.SaveChangesAsync(cancellationToken);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var excelPackage = new ExcelPackage(new FileInfo(_ExcelFilePath)))
        {
            var workbook = excelPackage.Workbook;

            // persist paganod schema to excel file
            PersistPaganodModelsToExcel(ref workbook);

            // persist records to excel file
            PersistToExcelWorksheet(ref workbook);

            excelPackage.Save();
        }

        return numberOfRecords;
    }

    private void PersistPaganodModelsToExcel(ref ExcelWorkbook workbook)
    {
        SaveAsExcelTab(DataAccessLayer.SchemaModelRecords, ref workbook);
        SaveAsExcelTab(DataAccessLayer.SchemaColumnRecords, ref workbook);
        SaveAsExcelTab(DataAccessLayer.SchemaMigrationRecords, ref workbook);
        SaveAsExcelTab(DataAccessLayer.SchemaMigrationOperationRecords, ref workbook);
    }

    private void SaveAsExcelTab<TEntity>(DbSet<TEntity> dbSet, ref ExcelWorkbook workbook)
        where TEntity : class
    {
        string worksheetName = typeof(TEntity).Name;

        if (!workbook.Worksheets.Any(x => x.Name == worksheetName))
            workbook.Worksheets.Add(worksheetName);

        workbook.Worksheets[worksheetName].Cells[1, 1].LoadFromCollection(dbSet.AsEnumerable(), true);
    }

    //private void LoadFromExcelWorksheet<TEntity>(this DbSet<TEntity> dbset, ref ExcelWorkbook workbook, string worksheetName)
    //    where TEntity : class
    //{
    //    var worksheet = workbook.Worksheets.First(x => x.Name == worksheetName);

    //    Dictionary<int, string> colNumHeaders = new Dictionary<int, string>();

    //    // get headers
    //    int numProperties = typeof(TEntity).GetProperties().Length;
    //    for (int colNum = 1; colNum < numProperties + 1; colNum++)
    //        colNumHeaders.Add(colNum, worksheet.Cells[1, colNum].Value.ToString());

    //    // fil and typecast data
    //    //int row
    //    //do
    //    //{

    //    //}


    //    //for (int rowNum = 2; rowNum)

    //    //workbook.Worksheets[worksheetName].Cells[1, 1].LoadFromCollection
    //}

    private void PersistToExcelWorksheet(ref ExcelWorkbook workbook)
    {
        var tableNames = SchemaModels.GetAll().Select(x => x.TableName).ToArray();

        foreach (string tableName in tableNames)
        {
            string[] columnNames;

            var columnRange = new Dictionary<string, Dictionary<string, int>>();

            //create new worksheet tab (if needed)
            if (!workbook.Worksheets.Any(x => x.Name == tableName))
                workbook.Worksheets.Add(tableName);

            var worksheet = workbook.Worksheets[tableName];

            // write headers (setup map also)
            columnNames = SchemaColumns.GetSchemaColumnsForTableName(tableName).Select(x => x.Name).ToArray();
            for (int i = 0; i < columnNames.Length; i++)
            {
                if (!columnRange.ContainsKey(tableName))
                    columnRange.Add(tableName, new Dictionary<string, int>());

                columnRange[tableName].Add(columnNames[i], i + 1);
                worksheet.Cells[0 + 1, i + 1].Value = columnNames[i];
            }

            // write data
            //allRecords = this[tableName].AllAs().ToArray();

            //for (int i = 0; i < allRecords.Length; i++)
            //{
            //    record = allRecords[i];

            //    foreach (var columnName in columnRange[tableName].Keys)
            //    {
            //        if (record.ContainsKey(columnName))
            //        {
            //            object excelValue = record[columnName];

            //            string excelStrValue = excelValue switch
            //            {
            //                DateTime => ((DateTime)excelValue).ToShortDateString(),

            //                _ => excelValue.ToString(),
            //            };

            //            worksheet.Cells[i + 2, columnRange[tableName][columnName]].Value = excelStrValue;
            //        }
            //    }

            //    //foreach (var column in record.AllData())
            //    //    worksheet.Cells[i + 2, columnRange[tableName][column.Key]].Value = column.Value;
            //}
        }
    }
}
