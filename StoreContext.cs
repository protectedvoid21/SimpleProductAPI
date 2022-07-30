using System.Data;
using System.Data.SqlClient;

namespace WebApiTest; 

public class StoreContext {
    private readonly string connectionString;

    public StoreContext(IConfiguration configuration) {
        connectionString = configuration.GetConnectionString("Store");
    }

    public IDbConnection CreateConnection() {
        return new SqlConnection(connectionString);
    }
}