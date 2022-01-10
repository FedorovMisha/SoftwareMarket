using Dapper;
using Npgsql;
using SoftwareMarket.Core.Entity;

namespace SoftwareMarket.Core.Services;

public class BuyerTypeService
{
    private readonly ConnectionConfig _config;

    public BuyerTypeService(ConnectionConfig config)
    {
        _config = config;
    }

    public List<BuyerType> GetAll()
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection.Query<BuyerType>("SELECT * FROM BUYER_TYPE").ToList();
        }
    }

    public BuyerType? Get(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .Query<BuyerType>("SELECT * FROM BUYER_TYPE where ID = @id", new { id })
                .FirstOrDefault();
        }
    }

    public void Update(BuyerType buyerType)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .Execute("UPDATE BUYER_TYPE SET NAME = @name WHERE ID = @Id", new
                {
                    id = buyerType.Id,
                    name = buyerType.Name
                });
        }
    }

    public int Create(string name)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .ExecuteScalar<int>("INSERT into BUYER_TYPE (ID, NAME) values (nextval('buyertype_sequence'), @name) RETURNING Id", new
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
                .ExecuteScalar<int>("delete from BUYER_TYPE where id = @id", new
                {
                    id
                });
        }
    }
}