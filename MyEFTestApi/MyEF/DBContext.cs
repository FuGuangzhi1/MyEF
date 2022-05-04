using MyEF.Config;
using MyEF.DataTableTxtensions;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static MyEF.SqlSource.SqlSource;

namespace MyEF
{
    public class DBContext : IDisposable, IAsyncDisposable
    {
        private DbContextOptions _dbContextOptions = null!;
        private IDbConnection dbConnection;
        private IDbTransaction dbTransaction;
        private IDbCommand dbCommand;

        //public DBContext()
        //{
        //}
        public DBContext(DbContextOptions dbContextOptions)
        {
            this._dbContextOptions = dbContextOptions;
        }
        protected virtual void OnConfiguring(DbContextOptions optionsBuilder)
        {
            //_dbContextOptions = optionsBuilder.UseSqlSever();
        }
        private IDbConnection GetDbConnection()
        {
            return _dbContextOptions.myDBType switch
            {
                Enum.MyDBType.SqlSever => new SqlConnection(_dbContextOptions.conn),
                Enum.MyDBType.MySql => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }
        private void OpenAndGetConnection()
        {
            dbConnection = GetDbConnection();
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();
        }
        private void GetDbCommand()
        {
            if (dbConnection is null)
                OpenAndGetConnection();
            dbCommand = dbConnection!.CreateCommand();
        }
        public virtual void Add<T>(T t) where T : class
        {
            var sqlInfo = AddSql(t, _dbContextOptions.myDBType);
            Execute(sqlInfo.Item1, sqlInfo.Item2);
        }
        public virtual void Update<T>(T t) where T : class
        {
            var sqlInfo = UpdateSql(t, _dbContextOptions.myDBType);
            Execute(sqlInfo.Item1, sqlInfo.Item2);
        }
        public virtual void Delete<T>(object id)
        {
            var sqlInfo = DeleteSql<T>(id, _dbContextOptions.myDBType);
            Execute(sqlInfo.Item1, sqlInfo.Item2);
        }
        public virtual IEnumerable<T> Query<T>() where T : class, new()
        {
            if (dbCommand is null) GetDbCommand();
            dbCommand!.CommandText = SelectSql<T>();
            return ToMyList<T>();
        }
        private IEnumerable<T> ToMyList<T>() where T : class, new()
        {
            SqlDataAdapter adapter = new SqlDataAdapter(dbCommand as SqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt.ToList<T>();
        }
        public IEnumerable<T> Where<T>
            (Expression<Func<T, bool>> funWhere) where T : class, new()
        {
            if (dbCommand is null) GetDbCommand();
            ExpressionAnalysis.ExpressionAnalysis expressionAnalysis = new ExpressionAnalysis.ExpressionAnalysis();
            expressionAnalysis.Visit(funWhere);
            string sql = expressionAnalysis.GetSql();
            dbCommand!.CommandText = SelectSql<T>() + " where " + sql;
            return ToMyList<T>();
        }
        private void Execute(string sql, params DbParameter[] Parameter)
        {
            if (dbCommand is null) GetDbCommand();
            dbCommand!.CommandText += sql;
            Parameter.ToList().ForEach(sqlParameter => dbCommand.Parameters.Add(sqlParameter));
        }
        public int SaveChange()
        {
            if (dbCommand is null) GetDbCommand();
            return dbCommand!.ExecuteNonQuery();
        }
        public void Dispose()
        {
            dbConnection.Dispose();
            dbTransaction.Dispose();
            dbCommand.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}