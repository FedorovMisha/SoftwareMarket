using Dapper;
using Npgsql;
using SoftwareMarket.Core.Entity;

namespace SoftwareMarket.Core.Services;

public class SoftwareTypeService
{
    private readonly ConnectionConfig _config;

    public SoftwareTypeService(ConnectionConfig config)
    {
        _config = config;
    }

    public List<SoftwareType> GetAll()
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection.Query<SoftwareType>("SELECT * FROM SOFTWARE_TYPE").ToList();
        }
    }

    public SoftwareType? Get(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .Query<SoftwareType>("SELECT * FROM SOFTWARE_TYPE where ID = @id", new { id })
                .FirstOrDefault();
        }
    }

    public void Update(SoftwareType softwareType)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .Execute("UPDATE SOFTWARE_TYPE SET NAME = @name WHERE ID = @Id", new
                {
                    id = softwareType.Id,
                    name = softwareType.Name
                });
        }
    }

    public int Create(string name)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .ExecuteScalar<int>("INSERT into SOFTWARE_TYPE (ID, NAME) values (nextval('softwaretype_sequence'), @name) RETURNING Id", new
                {
                    name = name
                });
        }
    }
    
    public void Remove(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .ExecuteScalar<int>("delete from SOFTWARE_TYPE where id = @id", new
                {
                    id
                });
        }
    }
}