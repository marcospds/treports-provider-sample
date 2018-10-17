using System.Collections.Generic;
using System.Data;
using TReportsProviderSample.Dto;

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
      else if (request.TableSourceName == "PAIS")
      {
        tableSource.ReadXml("Data\\Pais.xml");
        description = "Pais do dataset";
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
        MapColumns(response.SchemaTable.Columns);
      }
    }

    internal static void MapColumns(Column[] columns)
    {
      foreach(Column c in columns)
      {
        if (c.ColumnType.ToUpper() == "SYSTEM.STRING")
          c.ColumnType = "String";
        else if (c.ColumnType.ToUpper() == "SYSTEM.DATETIME")
          c.ColumnType = "DateTime";
        else if (c.ColumnType.ToUpper() == "SYSTEM.DOUBLE")
          c.ColumnType = "Double";
        else if (c.ColumnType.ToUpper() == "SYSTEM.SINGLE")
          c.ColumnType = "Float";
        else if (c.ColumnType.ToUpper() == "SYSTEM.DECIMAL")
          c.ColumnType = "Decimal";
        else if (c.ColumnType.ToUpper() == "SYSTEM.INT32")
          c.ColumnType = "Integer32";
        else if (c.ColumnType.ToUpper() == "SYSTEM.INT16")
          c.ColumnType = "Integer16";
        else if (c.ColumnType.ToUpper() == "SYSTEM.INT64")
          c.ColumnType = "Integer64";
        else if (c.ColumnType.ToUpper() == "SYSTEM.BOOLEAN")
          c.ColumnType = "Boolean";
        else if (c.ColumnType.ToUpper() == "SYSTEM.BYTE[]")
          c.ColumnType = "Bytes";

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
      relation.ParentColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO" });
      relation.ChildColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO_EMPRESA" });
      relations.Add(relation);

      relation = new Relation();
      relation.ChildColumns = new List<ChildColumnElement>();
      relation.ParentColumns = new List<ChildColumnElement>();
      relation.RelationName = $"{parentTable}_{childTable}_2";
      relation.ParentTableName = parentTable;
      relation.ChildTableName = childTable;
      relation.ParentColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO" });
      relation.ParentColumns.Add(new ChildColumnElement() { ColumnName = "PAIS" });
      relation.ChildColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO_EMPRESA" });
      relation.ChildColumns.Add(new ChildColumnElement() { ColumnName = "PAIS" });
      relations.Add(relation);

      Relation relation2 = new Relation();
      relation2.ChildColumns = new List<ChildColumnElement>();
      relation2.ParentColumns = new List<ChildColumnElement>();
      relation2.RelationName = $"{parentTable}_{childTable}_xxx";
      relation2.ParentTableName = parentTable;
      relation2.ChildTableName = childTable;
      relations.Add(relation2);
      relation2.ParentColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO" });
      relation2.ChildColumns.Add(new ChildColumnElement() { ColumnName = "CODIGO_EMPRESA" });


      response.Relations = relations.ToArray();
    }
  }
}

