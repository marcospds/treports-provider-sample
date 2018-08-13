using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TReportsProviderSample.Classes;
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
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    protected async Task<IActionResult> TestConnection()
    {
      return Ok(new TReportsTestSuccessResponse());
    }

    /// <summary>
    /// Faz o teste de uma query
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected async Task<IActionResult> TestQuery([FromBody] TReportsTestQueryRequest request)
    {
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
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    protected async Task<IActionResult> SchemaTable([FromBody] TReportsSchemaTableRequest request)
    {
      try
      {

        TReportsSchemaTableResponse response = new TReportsSchemaTableResponse();
        Schema.GetSchemaTable(request, response);
        if (request.TablesSourceGetRelations != null && request.TablesSourceGetRelations.Length > 0)
          Schema.GetRelationshipTableInfo(request, response);
        return Ok(response);
      }
      catch (Exception ex)
      {
        Response.StatusCode = 500;
        return Accepted(new TReportsCustomError() { code = "500", detailedMessage = ex.StackTrace, message = ex.Message });
      }
    }

    protected async Task<IActionResult> SchemaSql([FromBody] TReportsSchemaSqlRequest request)
    {
      return BadRequest(new TReportsCustomError() { code = "400", detailedMessage = "Esse provedor não suporta sql, pois é um exemolo que nao utilizabase de dados.", message = "Não suportado" });
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
              response.Data = reader.ReadToEnd();
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
