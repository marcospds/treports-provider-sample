using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TReportsProviderSample.Dto;
using TReportsProviderSample.Extensions;

namespace TReportsProviderSample.Controllers
{
   public class TReportsProviderControllerBase : Controller
  {

    /// <summary>
    /// Retorna os parâmetros do provedor integrado
    /// </summary>
    /// <returns></returns>
    protected async Task<IActionResult> GetParameters()
    {
      Log.Information("***Executando método 'GetParameters'***");
      try
      {
        TReportsParamsResponse response = new TReportsParamsResponse();
        var parameterUser = new TReportsProviderParams();
        parameterUser.Name = "user";
        parameterUser.Value = "treports";
        parameterUser.IsPassword = false;
        parameterUser.Description = "Usuário para acesso";

        var parameterPassword = new TReportsProviderParams();
        parameterPassword.Name = "password";
        parameterPassword.Value = "treports";
        parameterPassword.IsPassword = true;
        parameterPassword.Description = "Senha para acesso";

        var parameterUpperCase = new TReportsProviderParams();
        parameterUpperCase.Name = "upper";
        parameterUpperCase.Value = "false";
        parameterUpperCase.IsPassword = false;
        parameterUpperCase.Description = "Retorno em Upper Case?";

        var parameterResponseFormat = new TReportsProviderParams();
        parameterResponseFormat.Name = "responseFormat";
        parameterResponseFormat.Value = "json";
        parameterResponseFormat.IsPassword = false;
        parameterResponseFormat.Description = "Formato resposta provider JSON ou XML";

        response.ProviderParams = new TReportsProviderParams[] { parameterUser, parameterPassword, parameterUpperCase, parameterResponseFormat };
        return Ok(response);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    protected async Task<IActionResult> TestConnection()
    {
      Log.Information("***Executando método 'TestConnection'***");
      return Ok(new TReportsTestSuccessResponse());
    }

    /// <summary>
    /// Faz o teste de uma query
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected async Task<IActionResult> TestQuery([FromBody] TReportsTestQueryRequest request)
    {
      Log.Information("***Executando método 'TestQuery'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);

      try
      {
        string sql = request.SqlText;

        if (sql.ToUpper().Contains("WHERE"))
          sql = sql.Substring(sql.IndexOf("WHERE", StringComparison.InvariantCultureIgnoreCase) + 5);
        else
          sql = "1=1";

        if (request.SqlParameters != null)
        {
          foreach (SqlParameter parameter in request.SqlParameters)
          {
            string parAux = parameter.ParamType == "System.String" || parameter.ParamType == "System.DateTime" ? "'{0}'" : "{0}";

            sql = sql.Replace($":{parameter.ParamName}", string.Format(parAux, parameter.ParamValue));
          }
        }

        DataTable tableEmpresa = new DataTable();

        tableEmpresa.ReadXml("Data\\Empresa.xml");

        try
        {
          tableEmpresa.Select(sql);
          return Ok(new TReportsTestSuccessResponse());

        }
        catch { }

        DataTable tableFilial = new DataTable();
        tableFilial.ReadXml("Data\\Filial.xml");
        try
        {
          tableEmpresa.Select(sql);
          return Ok(new TReportsTestSuccessResponse());
        }
        catch
        {
          throw;
        }
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    protected async Task<IActionResult> SchemaTable([FromBody] TReportsSchemaTableRequest request)
    {
      Log.Information("***Executando método 'SchemaTable'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);


      try
      {
        TReportsSchemaTableResponse response = new TReportsSchemaTableResponse();
        Schema.GetSchemaTable(request, response);
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

    protected async Task<IActionResult> SchemaSql([FromBody] TReportsSchemaSqlRequest request)
    {
      Log.Information("***Executando método 'SchemaSql'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);

      return BadRequest(new TReportsCustomError() { code = "400", detailedMessage = "Esse provedor não suporta sql, pois é um exemolo que nao utilizabase de dados.", message = "Não suportado" });
    }

    protected async Task<IActionResult> GetRelations([FromBody] TReportsRelationRequest request)
    {
      Log.Information("***Executando método 'GetRelations'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);

      try
      {
        TReportsRelationResponse response = new TReportsRelationResponse();
        Schema.GetRelationshipTableInfo(request, response);
        return Ok(response);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    protected async Task<IActionResult> SearchTable([FromBody] TReportsSearchTableRequest request)
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

        SearchTable pais= new SearchTable();
        pais.TableSourceName = "PAIS";
        pais.TableSourceDescription = "País";
        tables.Add(pais);

        SearchTable empresa = new SearchTable();
        empresa.TableSourceName = "EMPRESA";
        empresa.TableSourceDescription = "Empresa";
        tables.Add(empresa);

        SearchTable filial = new SearchTable();
        filial.TableSourceName = "FILIAL";
        filial.TableSourceDescription = "Filial";
        tables.Add(filial);
       
        response.SearchTables = tables.FindAll(x => x.TableSourceName.ToUpper().Contains(request.FindTable.ToUpper()));

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
    /// Relatório de recorrencias
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// Relatório de recorrencias
    /// <returns></returns>
    /// <response code="200">Relatório</response>
    protected async Task<IActionResult> GetData([FromBody] TReportsDataRequest request)
    {
      Log.Information("***Executando método 'GetData'***");
      Log.Information("-----Leitura dos parâmetros  -----" + System.Environment.NewLine);
      Log.Information(JsonConvert.SerializeObject(request));
      Log.Information(System.Environment.NewLine);
      Log.Information("-----Fim da leitura dos parâmetros -----" + System.Environment.NewLine);

      try
      {
        string sql = request.SentenceMember.SqlText;

        if (sql.ToUpper().Contains("WHERE"))
          sql = sql.Substring(sql.IndexOf("WHERE", StringComparison.InvariantCultureIgnoreCase) + 5);
        else
          sql = "1=1";

        if (request.SentenceMember.SqlParameters != null)
        {
          foreach (SqlParameter parameter in request.SentenceMember.SqlParameters)
          {
            string parAux = parameter.ParamType == "System.String" || parameter.ParamType == "System.DateTime" ? "'{0}'" : "{0}";

            sql = sql.Replace($":{parameter.ParamName}", string.Format(parAux, parameter.ParamValue));
          }
        }


        DataTable table = new DataTable();
        if (request.SentenceMember.SqlText.ToUpper().Contains("FILIAL"))
        {
          table.ReadXml("Data\\Filial.xml");
        }
        else
        {
          table.ReadXml("Data\\Empresa.xml");
        }
        table.TableName = request.SentenceMember.EntityName;

        //Aqui aplicariamos o filtro, mas como é um exemplo usando datatable não foi possível por causa dos nomes das tabelas e join
        //table.DefaultView.RowFilter = sql;
        //DataTable result = table.DefaultView.ToTable();

        if (request.ProviderParams?.FirstOrDefault(p => p.Name == "upper")?.Value == "true")
        {
          foreach (DataRow row in table.Rows)
          {
            foreach (DataColumn column in table.Columns)
              row[column.ColumnName] = row[column.ColumnName].ToString().ToUpper();
          }
        }

        TReportsDataReponse response = new TReportsDataReponse();
        bool isXmlFormat = request.ProviderParams?.FirstOrDefault(p => p.Name == "responseFormat")?.Value?.ToLower() == "xml";

        if (isXmlFormat)
        {
          using (MemoryStream ms = new MemoryStream())
          {
            table.WriteXml(ms);
            ms.Position = 0;
            using (StreamReader reader = new StreamReader(ms))
            {
              response.Data = reader.ReadToEnd();
              response.DataContentType = "application/xml";
            }
          }
        }
        else
        {
          response.Data = JsonConvert.SerializeObject(table.ToDictionaryList(), Formatting.Indented);
        }

        return Ok(response);

      }
      catch (Exception ex)
      {
        Log.Error(ex.Message);
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }
  }

  public class TReportsCustomError
  {
    public string code { get; set; }
    public string message { get; set; }
    public string detailedMessage { get; set; }
  }
}
