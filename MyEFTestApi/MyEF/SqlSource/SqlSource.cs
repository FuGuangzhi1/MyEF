using MyEF.Enum;
using MyEF.Recard;
using MyEF.Txtensions;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MyEF.SqlSource
{
    public static class SqlSource
    {
        private static int _index = 0;
        public static Tuple<string, DbParameter[]> AddSql<T>(T t, MyDBType myDBType)
        {
            int index = _index;
            Type type = t.GetType();
            string valueSql = string.Join(',', type.GetProperties().Select(p => $"@{p.Name + index++}"));
            index = _index;
            DbParameter[] sqlParameters = type.GetProperties().Select(p =>
             {
                 return myDBType switch
                 {
                     MyDBType.SqlSever => new SqlParameter(p.Name + index++, p.GetValue(t)),
                     MyDBType.MySql => throw new NotImplementedException(),
                     _ => throw new NotImplementedException(),
                 };
             }).ToArray();
            _index = index;
            return new Tuple<string, DbParameter[]>($"insert into [{GetSqlConfig<T>().TableName}]({GetSqlConfig<T>().ColumnSql}) values({valueSql});", sqlParameters);
        }
        public static Tuple<string, DbParameter> DeleteSql<T>(object id, MyDBType myDBType)
        {
            int index = _index;
            DbParameter dbParameter = myDBType switch
            {
                MyDBType.SqlSever => new SqlParameter(GetSqlConfig<T>().Id + index++, id),
                MyDBType.MySql => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
            index = _index;
            return new Tuple<string, DbParameter>
                ($@"delete from [{GetSqlConfig<T>().TableName}] where [{GetSqlConfig<T>().Id}]= @{GetSqlConfig<T>().Id + index};", dbParameter);
        }
        public static Tuple<string, DbParameter[]> UpdateSql<T>(T t, MyDBType myDBType)
        {
            int index = _index;
            int idIndex = 0;
            Type type = t!.GetType();
            object? id = string.Empty;
            string valueSql = string.Join(',', type.GetProperties().Select(p =>
            {
                if (p.Name.Equals(type.GetIdName()))
                {
                    id = p?.GetValue(t);
                    idIndex = index++;
                    return "";
                }
                return $"{p!.Name}=@{p.Name + index++}";
            }).Where(x => x != ""));
            index = _index;
            DbParameter[] sqlParameters = type.GetProperties().Select(p =>
            {
                return myDBType switch
                {
                    MyDBType.SqlSever => new SqlParameter(p.Name + index++, p.GetValue(t)),
                    MyDBType.MySql => throw new NotImplementedException(),
                    _ => throw new NotImplementedException(),
                };
            }).ToArray();
            return new Tuple<string, DbParameter[]>
               ($@" update  [{type.GetTableName()}]  set {valueSql}  where [{type.GetIdName()}]= @{type.GetIdName() + idIndex} ;", sqlParameters);
        }
        public static string SelectSql<T>()
        {
            return $"Select {GetSqlConfig<T>().ColumnSql} from {GetSqlConfig<T>().TableName}";
        }
        private static SqlInfo GetSqlConfig<T>()
        {
            Type type = typeof(T);
            return new SqlInfo(string.Join(',', type.GetProperties().Select(p => $"[{p.Name}]").ToList()), type.GetTableName(), type.GetIdName());
        }
    }
}
