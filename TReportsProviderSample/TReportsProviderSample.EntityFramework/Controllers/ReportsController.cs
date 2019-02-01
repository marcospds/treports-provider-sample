using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Common;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TReportsProviderSample.Dto;
using System.Net;

namespace TReportsProviderSampleEntityFramework.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [ApiController]
  public class ReportsController : ControllerBase
  {
    private readonly ContextSample Context;
    public ReportsController(ContextSample _context)
    {
      Context = _context;
    }

    /// <summary>
    /// Retorna os parâmetros do provedor integrado
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("parameters")]
    [ProducesResponseType(typeof(TReportsParamsResponse), 200)]
    public IActionResult GetParameters()
    {
      Log.Information("***Executando método 'GetParameters'***");
      try
      {
        return Ok(new TReportsParamsResponse());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    /// <summary>
    /// Testa a disponibilidade do Provedor de Dados
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("testconnection")]
    [ProducesResponseType(typeof(TReportsTestSuccessResponse), 200)]
    public IActionResult TestConnection()
    {
      Log.Information("***Executando método 'TestConnection'***");
      return Ok(new TReportsTestSuccessResponse());
    }

    /// <summary>
    /// Valida um Query no banco
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("testquery")]
    [ProducesResponseType(typeof(TReportsTestSuccessResponse), 200)]
    public IActionResult TestQuery([FromBody] TReportsTestQueryRequest request)
    {
      Log.Information("***Executando método 'TestQuery'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);

      try
      {
        GetDataSql(GetSentenceSql(request.SqlText, request.SqlParameters), out IEnumerable<Column> columns);
        return Ok(new TReportsTestSuccessResponse());
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    /// <summary>
    /// Recupera o Schema de uma Tabela
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("schema/table")]
    [ProducesResponseType(typeof(TReportsSchemaTableResponse), 200)]
    public IActionResult SchemaTable([FromBody] TReportsSchemaTableRequest request)
    {
      Log.Information("***Executando método 'SchemaTable'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);

      try
      {
        TReportsSchemaTableResponse response = new TReportsSchemaTableResponse();
        response = GetSchemaTable(request);
        Log.Information("***Resposta do método 'SchemaTable'***");
        Log.Information(JsonConvert.SerializeObject(response));
        Log.Information(System.Environment.NewLine);
        Log.Information("-----Fim da resposta do método 'SchemaTable'-----" + System.Environment.NewLine);

        return Ok(response);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    private TReportsSchemaTableResponse GetSchemaTable(TReportsSchemaTableRequest request)
    {
      var entityType = GetEntityType(request.TableSourceName);

      var columns = new List<Column>();
      foreach (IProperty property in entityType.GetProperties())
      {
        columns.Add(new Column
        {
          ColumnName = property.Name,
          ColumnDescription = property.PropertyInfo.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>()?.Description,
          ColumnType = MapColumn(property.FieldInfo?.FieldType.ToString())
        });
      }

      return new TReportsSchemaTableResponse
      {
        SchemaTable = new SchemaTable
        {
          TableSourceName = request.TableSourceName,
          Columns = columns.Where(x => !string.IsNullOrEmpty(x.ColumnType)).ToArray()
        }
      };
    }

    /// <summary>
    /// Recupera o schema de uma sentença SQL
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("schema/sql")]
    [ProducesResponseType(typeof(TReportsSchemaSqlResponse), 200)]
    public IActionResult SchemaSql([FromBody] TReportsSchemaSqlRequest request)
    {
      Log.Information("***Executando método 'SchemaSql'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);

      try
      {
        var response = GetSchemaSql(request);

        Log.Information("***Resposta do método 'SchemaTable'***");
        Log.Information(JsonConvert.SerializeObject(response));
        Log.Information(System.Environment.NewLine);
        Log.Information("-----Fim da resposta do método 'SchemaTable'-----" + System.Environment.NewLine);

        return Ok(response);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    private TReportsSchemaSqlResponse GetSchemaSql(TReportsSchemaSqlRequest request)
    {
      GetDataSql(GetSentenceSql(request.SqlText, request.SqlParameters), out IEnumerable<Column> columns);

      return new TReportsSchemaSqlResponse
      {
        SchemaSql = new SchemaSql
        {
          Columns = columns.ToArray()
        }
      };
    }

    /// <summary>
    /// Recupera as Relations entre duas tabelas
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("relations")]
    public IActionResult GetRelations([FromBody] TReportsRelationRequest request)
    {
      Log.Information("***Executando método 'GetRelations'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);

      try
      {
        TReportsRelationResponse response = new TReportsRelationResponse();
        response = GetRelationsDto(request);
        return Ok(response);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    private TReportsRelationResponse GetRelationsDto(TReportsRelationRequest request)
    {
      var relations = new List<Relation>();
      IEntityType entityType = GetEntityType(request.ParentTableName);

      var z = entityType.GetDeclaredNavigations();
      foreach (INavigation navigation in z)
      {
        var relation = new Relation()
        {
          ParentTableName = GetEntityName(navigation.ForeignKey.PrincipalEntityType),
          ChildTableName = GetEntityName(navigation.ForeignKey.DeclaringEntityType),
        };

        List<ChildColumnElement> parentColumns = new List<ChildColumnElement>();
        foreach (IProperty parentKeyColumns in navigation.ForeignKey.PrincipalKey.Properties)
        {
          parentColumns.Add(new ChildColumnElement
          {
            ColumnName = parentKeyColumns.Name
          });
        }
        relation.ParentColumns = parentColumns;

        List<ChildColumnElement> childColumns = new List<ChildColumnElement>();
        foreach (IProperty childKeyColumns in navigation.ForeignKey.Properties)
        {
          childColumns.Add(new ChildColumnElement
          {
            ColumnName = childKeyColumns.Name
          });
        }
        relation.ChildColumns = childColumns;

        relation.RelationName = $"{relation.ParentTableName}_{relation.ChildTableName}";
        relations.Add(relation);


        Console.WriteLine(navigation.ToDebugString());
      }


      return new TReportsRelationResponse
      {
        Relations = relations.ToArray()
      };
    }
    
    /// <summary>
    /// Recupera os caminhos entre duas tabelas
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("paths")]
    public IActionResult GetPaths([FromBody] TReportsPathRequest request)
    {
      Log.Information("***Executando método 'GetPaths'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);
      
      try
      {
        TReportsPathResponse response = new TReportsPathResponse();
        response = GetPathsDto(request);
        return Ok(response);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    private TReportsPathResponse GetPathsDto(TReportsPathRequest request)
    {
      var paths = new List<Path>();
      IEntityType entityType = GetEntityType(request.TableName);

      var z = entityType.GetNavigations()
                        .Where(x => GetEntityName(x.ForeignKey.PrincipalEntityType) == request.TargetTableName 
                               || !x.ClrType.IsClass);

      foreach (var navigation in z)
      {
        bool hasChildPath = navigation
                                    .ForeignKey
                                    .DeclaringEntityType
                                    .GetDeclaredReferencingForeignKeys()
                                    .Any(x => GetEntityName(x.DeclaringEntityType)?.ToUpper() == request.TargetTableName?.ToUpper());

        if (GetEntityName(navigation.ForeignKey.DeclaringEntityType)?.ToUpper() == request.TargetTableName?.ToUpper() ||
          GetEntityName(navigation.ForeignKey.PrincipalEntityType)?.ToUpper() == request.TargetTableName?.ToUpper() ||
          hasChildPath)
        {
          var path = new Path()
          {
            ParentTableName = GetEntityName(navigation.ForeignKey.PrincipalEntityType),
            ChildTableName = GetEntityName(navigation.ForeignKey.DeclaringEntityType),
            ChildPaths =new List<Path>()
          };

          List<ChildColumnElement> parentColumns = new List<ChildColumnElement>();
          foreach (IProperty parentKeyColumns in navigation.ForeignKey.PrincipalKey.Properties)
          {
            parentColumns.Add(new ChildColumnElement
            {
              ColumnName = parentKeyColumns.Name
            });
          }

          path.ParentColumns = parentColumns;

          List<ChildColumnElement> childColumns = new List<ChildColumnElement>();
          foreach (IProperty childKeyColumns in navigation.ForeignKey.Properties)
          {
            childColumns.Add(new ChildColumnElement
            {
              ColumnName = childKeyColumns.Name
            });
          }

          path.ChildColumns = childColumns;
          path.PathName = $"{path.ParentTableName}_{path.ChildTableName}";

          if (hasChildPath)
          {
            var fk = navigation.ForeignKey
                                .DeclaringEntityType
                                .GetDeclaredReferencingForeignKeys()
                                .Where(x => GetEntityName(x.DeclaringEntityType)?.ToUpper() == request.TargetTableName?.ToUpper());


            foreach (var item in fk)
            {
              var childPath = new Path()
              {
                ParentTableName = GetEntityName(navigation.ForeignKey.DeclaringEntityType),
                ChildTableName = GetEntityName(item.DeclaringEntityType),
                ParentColumns = new List<ChildColumnElement>(),
                ChildColumns = new List<ChildColumnElement>()
              };

              foreach (IProperty parentKeyColumns in item.PrincipalKey.Properties)
              {
                childPath.ParentColumns.Add(new ChildColumnElement
                {
                  ColumnName = parentKeyColumns.Name
                });
              }

              foreach (IProperty childKeyColumns in item.Properties)
              {
                childPath.ChildColumns.Add(new ChildColumnElement
                {
                  ColumnName = childKeyColumns.Name
                });
              }
                            
              childPath.PathName = $"{childPath.ParentTableName}_{childPath.ChildTableName}";
                            
              path.ChildPaths.Add(childPath);
            }

          }

          paths.Add(path);

          Console.WriteLine(navigation.ToDebugString());
        }
      }

      return new TReportsPathResponse { Paths = paths };
    }

    /// <summary>
    /// Retorna as tabelas do Provedor de Dados de acordo com o nome enviado
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("search/tables")]
    public IActionResult SearchTable([FromBody] TReportsSearchTableRequest request)
    {
      Log.Information("***Executando método 'SearchTable'***");
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim do método da leitura dos parâmetros -----" + System.Environment.NewLine);

      try
      {
        TReportsSearchTableResponse response = new TReportsSearchTableResponse();
        response.SearchTables = new System.Collections.Generic.List<SearchTable>();

        List<SearchTable> tables = new List<SearchTable>();

        foreach (IEntityType entityType in Context.Model.GetEntityTypes().Where(x => x.Name.Contains(request.FindTable ?? string.Empty)))
        {
          tables.Add(new SearchTable
          {
            TableSourceName = GetEntityName(entityType)
          });
        }

        response.SearchTables = tables;

        return Ok(response);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    /// <summary>
    /// Recupera os Dados de acordo com a sentença
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("data")]
    [ProducesResponseType(typeof(TReportsDataReponse), 200)]
    public IActionResult GetData([FromBody] TReportsDataRequest request)
    {
      Log.Information("***Executando método 'GetData'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);

      try
      {
        TReportsDataReponse response = new TReportsDataReponse();
        response.Data = GetDataSql(GetSentenceSql(request.SentenceMember.SqlText, request.SentenceMember.SqlParameters), out IEnumerable<Column> columns);
        return Ok(response);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    private string GetDataSql(string sqlText, out IEnumerable<Column> columns)
    {
      using (var command = Context.Database.GetDbConnection().CreateCommand())
      {
        command.CommandText = sqlText;
        Context.Database.OpenConnection();
        using (var result = command.ExecuteReader())
        {
          return JsonConvert.SerializeObject(Serialize(result, out columns));
        }
      }
    }

    private IEnumerable<Dictionary<string, object>> Serialize(DbDataReader reader, out IEnumerable<Column> columns)
    {
      var results = new List<Dictionary<string, object>>();
      columns = GetColumns(reader);

      while (reader.Read())
        results.Add(SerializeRow(columns, reader));

      return results;
    }

    private List<Column> GetColumns(DbDataReader reader)
    {
      var cols = new List<Column>();
      for (var i = 0; i < reader.FieldCount; i++)
        cols.Add(new Column
        {
          ColumnName = reader.GetName(i),
          ColumnType = MapColumn(reader.GetFieldType(i).ToString())
        });
      return cols;
    }

    private Dictionary<string, object> SerializeRow(IEnumerable<Column> cols, DbDataReader reader)
    {
      var result = new Dictionary<string, object>();
      foreach (var col in cols)
        result.Add(col.ColumnName, reader[col.ColumnName]);
      return result;
    }

    private string GetEntityName(IEntityType entityType)
    {
      return entityType.Name.Split('.').Last();
    }

    private IEntityType GetEntityType(string entityName)
    {
      return Context.Model.GetEntityTypes().First(x => x.ClrType.Name?.ToUpper() == entityName?.ToUpper());
    }

    private string MapColumn(string column)
    {
      switch (column?.ToUpper())
      {
        case "SYSTEM.STRING": return "String";
        case "SYSTEM.DATETIME": return "DateTime";
        case "SYSTEM.DOUBLE": return "Double";
        case "SYSTEM.SINGLE": return "Float";
        case "SYSTEM.DECIMAL": return "Decimal";
        case "SYSTEM.INT32": return "Integer32";
        case "SYSTEM.INT16": return "Integer16";
        case "SYSTEM.INT64": return "Integer64";
        case "SYSTEM.BOOLEAN": return "Boolean";
        case "SYSTEM.BYTE[]": return "Bytes";
        default: return "SYSTEM.STRING";
      };
    }

    /// <summary>
    /// Replace all parameters on Sentence
    /// </summary>
    /// <param name="sentenceMember"></param>
    /// <returns></returns>
    private string GetSentenceSql(string sql, SqlParameter[] sqlParameters)
    {
      if (sqlParameters != null)
        foreach (SqlParameter param in sqlParameters)
          sql = sql.Replace($":{param.ParamName}", $"'{param.ParamValue}'");

      return sql;
    }
  }
}