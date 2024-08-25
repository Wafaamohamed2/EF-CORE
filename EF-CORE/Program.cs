using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

internal class Program
{

    private static void Main(string[] args)
    {
        var configration = new ConfigurationBuilder()
            .AddJsonFile("appstiing.json")
            .Build();

        SqlConnection connction = new SqlConnection(configration.GetSection("constr").Value);
        var sql = "SELECT * FROM WALLETS";

        SqlCommand cmd = new SqlCommand(sql, connction);

        cmd.CommandType = CommandType.Text;

        connction.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        Wallet wallet;

        while (reader.Read()) {

            wallet = new Wallet
            {

                Id = reader.GetInt32("Id"),
                Holder = reader.GetString("Holder"),
                Balance = reader.GetDecimal("Balance")
            };

            Console.WriteLine(wallet);


        }
        connction.Close();
    }
}