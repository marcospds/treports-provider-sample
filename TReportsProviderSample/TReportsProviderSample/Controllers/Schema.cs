using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TReportsProviderSample.Classes;
using TReportsProviderSample.Classes.Commom;

namespace TReportsProviderSample.Controllers
{
  public class Schema
  {
    internal static void GetSchemaTable(TReportsSchemaTableRequest request, TReportsSchemaTableResponse response)
    {
      DataTable tableSource = new DataTable();
      DataTable tableDictionary = new DataTable();
      string description = null;

      tableDictionary.ReadXml("Data\\Dicionario.xml");

      if (request.TableSourceName == "EMPRESA")
      {
        tableSource.ReadXml("Data\\Empresa.xml");
        description = "Empresas do dataset";
      }
      else
      {
        tableSource.ReadXml("Data\\Filial.xml");
        description = "Filiais do dataset";
      }

      if (tableSource.Columns.Count > 0)
      {
        response.SchemaTable = new SchemaTable();
        response.SchemaTable.TableSourceName = tableSource.TableName;
        response.SchemaTable.TableSourceDescription = description;
        List<Column> columns = new List<Column>();
        foreach (DataColumn columnSchema in tableSource.Columns)
        {
          Column column = new Column();
          column.ColumnName = columnSchema.ColumnName;

          DataRow[] drDicionario = tableDictionary.Select(string.Format("TABELA = '{0}' AND COLUNA = '{1}'", tableSource.TableName, columnSchema.ColumnName));

          column.ColumnDescription = drDicionario[0]["DESCRICAO"].ToString();
          column.ColumnType = columnSchema.DataType.ToString();
          columns.Add(column);
        }
        response.SchemaTable.Columns = columns.ToArray();
      }
    }

    internal static void GetRelationshipTableInfo(TReportsRelationRequest request, TReportsRelationResponse response)
    {
      List<Relation> relations = new List<Relation>();
      string childTable = "FILIAL";
      string parentTable = "EMPRESA";
      Relation relation = new Relation();
      relation.ChildColumns = new List<ChildColumnElement>();
      relation.ParentColumns = new List<ChildColumnElement>();
      relation.RelationName = $"{parentTable}_{childTable}";
      relation.ParentTableName = parentTable;
      relation.ChildTableName = childTable;
      relations.Add(relation);
      relation.ParentColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO" });
      relation.ChildColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO_EMPRESA" });

      response.Relations = relations.ToArray();
    }
  }
}

