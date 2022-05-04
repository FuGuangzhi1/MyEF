using MyEFCore.Enum;
using System.Data;
using System.Data.SqlClient;

namespace MyEFCore
{
    public class DBContext : IDisposable, IAsyncDisposable
    {
        State state;
        IDbConnection dbConnection;
        protected virtual void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlSever("");
        }
        public IDbConnection GetIDbConnection()
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            OnConfiguring(dbContextOptionsBuilder);
            MyDBType dbType = dbContextOptionsBuilder.dBType;
            return dbType switch
            {
                MyDBType.SqlSever => new SqlConnection(dbContextOptionsBuilder.dbConnectionString),
                _ => throw new NotImplementedException(),
            };
        }
        private void OpenDb() 
        {
            dbConnection = GetIDbConnection();
            if (dbConnection.State == ConnectionState.Closed) dbConnection.Open();
        }
        protected virtual void Add<T>(T t)
        {
            OpenDb();

        }
        protected virtual void Update<T>(T t)
        {

        }
        protected virtual void Delete<T>(object id)
        {

        }
        protected virtual void Query<T>()
        {

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}