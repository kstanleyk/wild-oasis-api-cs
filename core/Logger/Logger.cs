using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Core.Logger;

public static class Logger
{
    private static readonly ILogger PerfLogger;
    private static readonly ILogger UsageLogger;
    private static readonly ILogger ErrorLogger;
    private static readonly ILogger DiagnosticLogger;

    static Logger()
    {
        var appSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = appSettings["ConnectionStrings:VishiHoldingLogger"];

        PerfLogger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(connectionString,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "PerfLogs",
                    AutoCreateSqlTable = true,
                    BatchPostingLimit = 1
                },
                columnOptions: GetSqlColumnOptions())
            .CreateLogger();

        UsageLogger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(connectionString,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "UsageLogs",
                    AutoCreateSqlTable = true,
                    BatchPostingLimit = 1
                },
                columnOptions: GetSqlColumnOptions())
            .CreateLogger();

        ErrorLogger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(connectionString,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "ErrorLogs",
                    AutoCreateSqlTable = true,
                    BatchPostingLimit = 1
                },
                columnOptions: GetSqlColumnOptions())
            .CreateLogger();

        DiagnosticLogger = new LoggerConfiguration()
            .WriteTo.MSSqlServer(connectionString,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "DiagnosticLogs",
                    AutoCreateSqlTable = true,
                    BatchPostingLimit = 1
                },
                columnOptions: GetSqlColumnOptions())
            .CreateLogger();

        Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
    }

    public static ColumnOptions GetSqlColumnOptions()
    {
        var columnOptions = new ColumnOptions();
        columnOptions.Store.Remove(StandardColumn.Properties);
        columnOptions.Store.Remove(StandardColumn.MessageTemplate);
        columnOptions.Store.Remove(StandardColumn.Message);
        columnOptions.Store.Remove(StandardColumn.Exception);
        columnOptions.Store.Remove(StandardColumn.TimeStamp);
        columnOptions.Store.Remove(StandardColumn.Level);

        columnOptions.AdditionalColumns = new Collection<SqlColumn>
        {
            new SqlColumn {DataType = SqlDbType.DateTime2, ColumnName = "Timestamp"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Product"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Layer"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Location"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Message"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Hostname"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "UserId"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "UserName"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Exception"},
            new SqlColumn {DataType = SqlDbType.BigInt, ColumnName = "ElapsedMilliseconds"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "CorrelationId"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "CustomException"},
            new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "AdditionalInfo"},
            new SqlColumn {DataType = SqlDbType.VarChar,DataLength = 25, ColumnName = "Client"},
            new SqlColumn {DataType = SqlDbType.VarChar,DataLength = 10, ColumnName = "BranchCode"},
        };

        return columnOptions;
    }

    public static void WritePerf(LogDetail infoToLog)
    {
        try
        {
            PerfLogger.Write(LogEventLevel.Information,
                "{Timestamp}{Message}{Layer}{Location}{Product}" +
                "{CustomException}{ElapsedMilliseconds}{Exception}{Hostname}" +
                "{UserId}{UserName}{CorrelationId}{AdditionalInfo}{Client}{BranchCode}",
                infoToLog.Timestamp, infoToLog.Message,
                infoToLog.Layer, infoToLog.Location,
                infoToLog.Product, infoToLog.CustomException,
                infoToLog.ElapsedMilliseconds, infoToLog.Exception?.ToBetterString(),
                infoToLog.Hostname, infoToLog.UserId,
                infoToLog.UserName, infoToLog.CorrelationId,
                infoToLog.AdditionalInfo,
                infoToLog.Client,
                infoToLog.BranchCode
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public static void WriteUsage(LogDetail infoToLog)
    {
        //_usageLogger.Write(LogEventLevel.Information, "{@LogDetail}", infoToLog);

        UsageLogger.Write(LogEventLevel.Information,
            "{Timestamp}{Message}{Layer}{Location}{Product}" +
            "{CustomException}{ElapsedMilliseconds}{Exception}{Hostname}" +
            "{UserId}{UserName}{CorrelationId}{AdditionalInfo}{Client}{BranchCode}",
            infoToLog.Timestamp, infoToLog.Message,
            infoToLog.Layer, infoToLog.Location,
            infoToLog.Product, infoToLog.CustomException,
            infoToLog.ElapsedMilliseconds, infoToLog.Exception?.ToBetterString(),
            infoToLog.Hostname, infoToLog.UserId,
            infoToLog.UserName, infoToLog.CorrelationId,
            infoToLog.AdditionalInfo,
            infoToLog.Client,
            infoToLog.BranchCode
        );
    }
    public static void WriteError(LogDetail infoToLog)
    {
        if (infoToLog.Exception != null)
        {
            var procName = FindProcName(infoToLog.Exception);
            infoToLog.Location = string.IsNullOrEmpty(procName) ? infoToLog.Location : procName;
            infoToLog.Message = GetMessageFromException(infoToLog.Exception);
        }

        ErrorLogger.Write(LogEventLevel.Information,
            "{Timestamp}{Message}{Layer}{Location}{Product}" +
            "{CustomException}{ElapsedMilliseconds}{Exception}{Hostname}" +
            "{UserId}{UserName}{CorrelationId}{AdditionalInfo}{Client}{BranchCode}",
            infoToLog.Timestamp, infoToLog.Message,
            infoToLog.Layer, infoToLog.Location,
            infoToLog.Product, infoToLog.CustomException,
            infoToLog.ElapsedMilliseconds, infoToLog.Exception?.ToBetterString(),
            infoToLog.Hostname, infoToLog.UserId,
            infoToLog.UserName, infoToLog.CorrelationId,
            infoToLog.AdditionalInfo,
            infoToLog.Client,
            infoToLog.BranchCode
        );
    }
    public static void WriteDiagnostic(LogDetail infoToLog)
    {
        //var writeDiagnostics = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableDiagnostics"]);
        //if (!writeDiagnostics)
        //    return;

        DiagnosticLogger.Write(LogEventLevel.Information,
            "{Timestamp}{Message}{Layer}{Location}{Product}" +
            "{CustomException}{ElapsedMilliseconds}{Exception}{Hostname}" +
            "{UserId}{UserName}{CorrelationId}{AdditionalInfo}{Client}{BranchCode}",
            infoToLog.Timestamp, infoToLog.Message,
            infoToLog.Layer, infoToLog.Location,
            infoToLog.Product, infoToLog.CustomException,
            infoToLog.ElapsedMilliseconds, infoToLog.Exception?.ToBetterString(),
            infoToLog.Hostname, infoToLog.UserId,
            infoToLog.UserName, infoToLog.CorrelationId,
            infoToLog.AdditionalInfo,
            infoToLog.Client,
            infoToLog.BranchCode
        );
    }

    private static string GetMessageFromException(Exception ex)
    {
        if (ex.InnerException != null)
            return GetMessageFromException(ex.InnerException);

        return ex.Message;
    }

    private static string FindProcName(Exception ex)
    {
        var sqlEx = ex as SqlException;
        if (sqlEx != null)
        {
            var procName = sqlEx.Procedure;
            if (!string.IsNullOrEmpty(procName))
                return procName;
        }

        if (!string.IsNullOrEmpty((string)ex.Data["Procedure"]))
        {
            return (string)ex.Data["Procedure"];
        }

        if (ex.InnerException != null)
            return FindProcName(ex.InnerException);

        return null;
    }
}