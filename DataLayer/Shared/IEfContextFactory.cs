using DataLayer.DbContext;
using MySql.Data.MySqlClient;

namespace DataLayer.Shared
{
    public interface IEfContextFactory
    {
        MainDbContext Create();
        MySqlConnection CreateMySqlConnection();

    }
}
