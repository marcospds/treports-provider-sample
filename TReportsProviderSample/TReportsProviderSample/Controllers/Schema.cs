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
      DataTable table = new DataTable();
      string description = null;
      if (request.TableSourceName == "EMPRESA")
      {
        table.ReadXml("Data\\Empresa.xml");
        description = "Empresas do dataset";
      }
      else
      {
        table.ReadXml("Data\\Filial.xml");
        description = "Filiais do dataset";
      }

      if (table.Columns.Count > 0)
      {
        response.SchemaTable = new SchemaTable();
        response.SchemaTable.TableSourceName = table.TableName;
        response.SchemaTable.TableSourceDescription = description;
        List<Column> columns = new List<Column>();
        foreach (DataColumn columnSchema in table.Columns)
        {
          Column column = new Column();
          column.ColumnName = columnSchema.ColumnName;
          column.ColumnDescription = columnSchema.ColumnName;
          column.ColumnType = columnSchema.DataType.ToString();
          columns.Add(column);
        }
        response.SchemaTable.Columns = columns.ToArray();
      }
    }

     internal static void GetRelationshipTableInfo(TReportsSchemaTableRequest request, TReportsSchemaTableResponse response)
    {
      List<SchemaRelation> relations = new List<SchemaRelation>();
      string childTable = "FILIAL";
      string parentTable = "EMPRESA";
      SchemaRelation relation = new SchemaRelation();
      relation.ChildColumns = new List<ChildColumnElement>();
      relation.ParentColumns = new List<ChildColumnElement>();
      relation.RelationName = $"{parentTable}_{childTable}";
      relation.ParentSourceName = parentTable;
      relation.ChildSourceName = childTable;
      relations.Add(relation);
      relation.ParentColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO" });
      relation.ChildColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO_EMPRESA" });

      response.SchemaRelation = relations.ToArray();
    }
  }
}

