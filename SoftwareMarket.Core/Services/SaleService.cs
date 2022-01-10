using Dapper;
using Npgsql;
using SoftwareMarket.Core.Entity;

namespace SoftwareMarket.Core.Services;

public class SaleService
{ 
    private readonly ConnectionConfig _config;

    public SaleService(ConnectionConfig config)
    {
        _config = config;
    }

    public List<Sale> GetAll()
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .Query<Sale>("SELECT * FROM Sale")
                .Distinct()
                .ToList();
        }
    }

    public Sale? Get(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .Query<Sale>("SELECT * FROM Sale Where Id = @id", 
                    new
                    {
                        id
                    })
                .Distinct()
                .FirstOrDefault();
        }
    }

    public void Update(Sale sale)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .Execute("UPDATE Sale SET softwareid = @softwareid, discountid = @discountid, buyerid = @buyerid, orderdate = @orderdate   WHERE ID = @Id", new
                {
                    id = sale.Id,
                    softwareid = sale.SoftwareId,
                    discountid = sale.DiscountId,
                    buyerid = sale.BuyerId,
                    orderdate = sale.OrderDate
                });
        }
    }

    public int Create(Sale sale)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .ExecuteScalar<int>("INSERT into Sale (ID, softwareid, discountid, buyerid, orderdate) values (nextval('sale_sequence'), @softwareid, @discountid, @buyerid, @orderdate) RETURNING Id", new
                {
                    softwareid = sale.SoftwareId,
                    discountid = sale.DiscountId,
                    buyerid = sale.BuyerId,
                    orderdate = sale.OrderDate
                });
        }
    }

    public void Remove(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .ExecuteScalar<int>("delete from sale where id = @id", new
                {
                    id
                });
        }
    }
}