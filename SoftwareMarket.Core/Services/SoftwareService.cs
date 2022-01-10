using Dapper;
using Npgsql;
using SoftwareMarket.Core.Entity;

namespace SoftwareMarket.Core.Services;

public class SoftwareService
{
    private readonly ConnectionConfig _config;

    public SoftwareService(ConnectionConfig config)
    {
        _config = config;
    }

    public List<Software> GetAll()
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .Query<Software, SoftwareType, Software>("SELECT * FROM Software JOIN Software_type as st on st.id = Software.typeid",
                    (software, type) =>
                    {
                        software.Type = type;
                        return software;
                    })
                .Distinct()
                .ToList();
        }
    }

    public Software? Get(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .Query<Software, SoftwareType, Software>("SELECT * FROM Software JOIN Software_type as st on st.id = Software.typeid  where Software.Id = @id",
                    (software, type) =>
                    {
                        software.Type = type;
                        return software;
                    }, param: new
                    {
                        id
                    })
                .Distinct()
                .FirstOrDefault();
        }
    }

    public void Update(Software software)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .Execute("UPDATE Software SET price = @price, typeid = @typeid, name = @name WHERE ID = @Id", new
                {
                    id = software.Id,
                    price = software.Price,
                    typeid = software.TypeId,
                    name = software.Name
                });
        }
    }

    public int Create(Software software)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .ExecuteScalar<int>("INSERT into Software (ID, typeid, name, price) values (nextval('software_sequence'), @tid, @name, @price) RETURNING Id", new
                {
                    tid = software.TypeId,
                    name = software.Name,
                    price = software.Price
                });
        }
    }
    
    public void Remove(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .ExecuteScalar<int>("delete from Software where id = @id", new
                {
                    id
                });
        }
    }
}