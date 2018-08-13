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
  [Route("api/[controller]")]
  public class TReportsProviderAnonymousController : TReportsProviderControllerBase
  {

    /// <summary>
    /// Retorna os parâmetros do provedor integrado
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("parameters")]
    [ProducesResponseType(typeof(TReportsParamsResponse), 200)]
    public async Task<IActionResult> GetParameters()
    {
      return await base.GetParameters();
    }

    [HttpPost]
    [Route("testconnection")]
    [ProducesResponseType(typeof(TReportsTestSuccessResponse), 200)]
    public async Task<IActionResult> TestConnection()
    {
      return await base.TestConnection();
    }

    /// <summary>
    /// Faz o teste de uma query
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("testquery")]
    [ProducesResponseType(typeof(TReportsTestSuccessResponse), 200)]
    public async Task<IActionResult> TestQuery([FromBody] TReportsTestQueryRequest request)
    {
      return await base.TestQuery(request);
    }

    [HttpPost]
    [Route("schema/table")]
    [ProducesResponseType(typeof(TReportsSchemaTableResponse), 200)]
    public async Task<IActionResult> SchemaTable([FromBody] TReportsSchemaTableRequest request)
    {
      return await base.SchemaTable(request);
    }

    [HttpPost]
    [Route("schema/sql")]
    [ProducesResponseType(400)]
    public async Task<IActionResult> SchemaSql([FromBody] TReportsSchemaSqlRequest request)
    {
      return await base.SchemaSql(request);
    }

    /// <summary>
    /// Relatório de recorrencias
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// Relatório de recorrencias
    /// <returns></returns>
    /// <response code="200">Relatório</response>
    [HttpPost]
    [Route("data")]
    [ProducesResponseType(typeof(TReportsDataReponse), 200)]
    public async Task<IActionResult> GetData([FromBody] TReportsDataRequest request)
    {
      return await base.GetData(request);
    }
  }
}
