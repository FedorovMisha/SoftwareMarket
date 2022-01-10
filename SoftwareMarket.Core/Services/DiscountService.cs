using Dapper;
using Npgsql;
using SoftwareMarket.Core.Entity;

namespace SoftwareMarket.Core.Services;

public class DiscountService
{
    private readonly ConnectionConfig _config;

    public DiscountService(ConnectionConfig config)
    {
        _config = config;
    }

    public List<Discount> GetAll()
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection.Query<Discount>("SELECT * FROM Discount").ToList();
        }
    }

    public Discount? Get(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .Query<Discount>("SELECT * FROM Discount where ID = @id", new { id })
                .FirstOrDefault();
        }
    }

    public void Update(Discount discount)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .Execute("UPDATE Discount SET value = @value WHERE ID = @Id", new
                {
                    id = discount.Id,
                    value = discount.Value
                });
        }
    }

    public int Create(decimal value)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .ExecuteScalar<int>("INSERT into discount (ID, value) values (nextval('discount_sequence'), @value) RETURNING Id", new
                {
                    value
                });
        }
    }
    
    public void Remove(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .ExecuteScalar<int>("delete from discount where id = @id", new
                {
                    id
                });
        }
    }
}