using Dapper;
using Npgsql;
using SoftwareMarket.Core.Entity;

namespace SoftwareMarket.Core.Services;

public class BuyerService
{
        private readonly ConnectionConfig _config;

    public BuyerService(ConnectionConfig config)
    {
        _config = config;
    }

    public List<Buyer> GetAll()
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .Query<Buyer, BuyerType, Buyer>("SELECT * FROM Buyer JOIN Buyer_Type as st on st.id = Buyer.Buyertype",
                    (buyer, type) =>
                    {
                        buyer.Type = type;
                        return buyer;
                    })
                .Distinct()
                .ToList();
        }
    }

    public Buyer? Get(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .Query<Buyer, BuyerType, Buyer>("SELECT * FROM Buyer JOIN Buyer_Type as st on st.id = Buyer.buyertype  where Buyer.Id = @id",
                    (buyer, type) =>
                    {
                        buyer.Type = type;
                        return buyer;
                    }, param: new
                    {
                        id
                    })
                .Distinct()
                .FirstOrDefault();
        }
    }

    public void Update(Buyer buyer)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .Execute("UPDATE Buyer SET buyertype = @buyertype, phone = @phone WHERE ID = @Id", new
                {
                    id = buyer.Id,
                    buyertype = buyer.BuyerType,
                    phone = buyer.Phone
                });
        }
    }

    public int Create(Buyer buyer)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            return connection
                .ExecuteScalar<int>("INSERT into Buyer (ID, buyertype, phone) values (nextval('buyer_sequence'), @buyertype, @phone) RETURNING Id", new
                {
                    buyertype = buyer.BuyerType,
                    phone = buyer.Phone
                });
        }
    }
    
    public void Remove(int id)
    {
        using (var connection =
               new NpgsqlConnection(_config.ConnectionString))
        {
            connection
                .ExecuteScalar<int>("delete from Buyer where id = @id", new
                {
                    id
                });
        }
    }
}