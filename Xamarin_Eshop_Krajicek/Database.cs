using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin_Eshop_Krajicek.Model;

namespace Xamarin_Eshop_Krajicek
{
    class Database
    {
        public readonly string databaseFileName;
        private readonly string connectionString;
        private static Database newDB = null;

        //C:\Users\Jiří Krajíček\AppData\Local\Packages\d7de7e96-c7d0-4231-be55-1649784a6c9d_cjvz65v3wgvtw\LocalState     

        private Database(string databaseFileName)
        {
            this.databaseFileName = databaseFileName;
            this.connectionString = $"Data Source={databaseFileName}";
        }

        public static Database Instance()
        {
            if (newDB == null)
            {
                newDB = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databaseEshop.sqlite3"));
             
            }
            return newDB;
        }

        public void CreateEshopDatabase()
        {
            if (!File.Exists(databaseFileName))
            {
                using (FileStream fs = File.Create(databaseFileName))
                {
                    fs.Close();
                }

                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string cmdTxt = "CREATE TABLE produkt (produktID INTEGER PRIMARY KEY AUTOINCREMENT, cena REAL, sleva REAL, nazev VARCHAR(256), popis VARCHAR(1024), kategorie VARCHAR(256))";
                    SqliteCommand command = new SqliteCommand(cmdTxt, connection);
                    command.ExecuteNonQuery();

                    cmdTxt = "CREATE TABLE zakaznik (zakaznikID INTEGER PRIMARY KEY AUTOINCREMENT, jmeno VARCHAR(256), adresa VARCHAR(1024))";
                    command = new SqliteCommand(cmdTxt, connection);
                    command.ExecuteNonQuery();

                    cmdTxt = "CREATE TABLE objednavka (objednavkaID INTEGER PRIMARY KEY AUTOINCREMENT, zakaznikID INTEGER REFERENCES zakaznik(zakaznikID), cena REAL, stav VARCHAR(256), datum DATETIME)";
                    command = new SqliteCommand(cmdTxt, connection);
                    command.ExecuteNonQuery();

                    cmdTxt = "CREATE TABLE polozka (polozkaID INTEGER PRIMARY KEY AUTOINCREMENT, objednavkaID INTEGER REFERENCES objednavka(objednavkaID), produktID INTEGER REFERENCES produkt(produktID), nazev VARCHAR(256), mnozstvi INTEGER, cena REAL)";
                    command = new SqliteCommand(cmdTxt, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
        /**************************************************************************************************************************************************************************/


        public List<Product> GetAllProducts()
        {
            List<Product> listP = new List<Product>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string cmdTxt = "SELECT * FROM produkt";
                SqliteCommand command = new SqliteCommand(cmdTxt, connection);
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    int productID = Convert.ToInt32(reader["produktID"]);
                    string nazev = Convert.ToString(reader["nazev"]);
                    string kategorie = Convert.ToString(reader["kategorie"]);
                    double cena = Convert.ToDouble(reader["cena"]);
                    string popis = Convert.ToString(reader["popis"]);
                    double sleva = Convert.ToDouble(reader["sleva"]);

                    Product produkt = new Product(productID, cena, sleva, nazev, popis, kategorie);

                    listP.Add(produkt);
                }

                connection.Close();
            }

            return listP;
        }

        public void CreateNewProduct(double cena, double sleva, string nazev, string popis, string kategorie)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO produkt (cena, sleva, nazev, popis, kategorie) values (@cena, @sleva, @nazev, @popis, @kategorie)";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    //command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@cena", cena);
                    command.Parameters.AddWithValue("@sleva", sleva);
                    command.Parameters.AddWithValue("@nazev", nazev);
                    command.Parameters.AddWithValue("@popis", popis);
                    command.Parameters.AddWithValue("@kategorie", kategorie);

                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }

        public void UpdateProduct(int productID, string nazev, string kategorie, double cena, double sleva, string popis)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE produkt SET nazev = @nazev, cena = @cena, kategorie = @kategorie, popis = @popis, sleva = @sleva WHERE produktID = @produktID";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@produktID", productID);
                    command.Parameters.AddWithValue("@cena", cena);
                    command.Parameters.AddWithValue("@sleva", sleva);
                    command.Parameters.AddWithValue("@nazev", nazev);
                    command.Parameters.AddWithValue("@popis", popis);
                    command.Parameters.AddWithValue("@kategorie", kategorie);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void DeleteProduct(int index)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM produkt WHERE produktID = @index";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@index", index);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public double GetProductSale(int productID)
        {
            double sale;

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT sleva FROM sleva WHERE produktID = @produktID";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@produktID", productID);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                        }
                        sale = reader.GetDouble(0);
                    }
                    command.ExecuteScalar();
                }

                connection.Close();
            }
            return sale;
        }

        /**************************************************************************************************************************************************************************/

        public void CreateNewCustomer(string jmeno, string adresa)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO zakaznik (jmeno, adresa) values (@jmeno, @adresa)";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@jmeno", jmeno);
                    command.Parameters.AddWithValue("@adresa", adresa);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> zakaznici = new List<Customer>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM zakaznik";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            int id = Convert.ToInt32(reader["zakaznikID"]);
                            string jmeno = Convert.ToString(reader["jmeno"]);
                            string adresa = Convert.ToString(reader["adresa"]);

                            Customer zakaznik = new Customer(id, jmeno, adresa);

                            zakaznici.Add(zakaznik);
                        }
                    }
                }
                connection.Close();
            }
            return zakaznici;
        }

        /**************************************************************************************************************************************************************************/

        public void CreateNewOrder(int zakaznikID, double cena, string stav, DateTime datumCas)
        {
            //string databazeFileName = "mydb.sqlite";
            //string connectionString = $"Data Source={databazeFileName};Version=3;";


            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO objednavka (zakaznikID, cena, stav, datum) values (@zakaznikID, @cena, @stav, @datum)";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@zakaznikID", zakaznikID);
                    command.Parameters.AddWithValue("@cena", cena);
                    command.Parameters.AddWithValue("@stav", stav);
                    command.Parameters.AddWithValue("@datum", datumCas);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Order> GetAllOrders()
        {
            List<Order> objednavky = new List<Order>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM objednavka";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int objednavkaID = Convert.ToInt32(reader["objednavkaID"]);
                            int zakaznikID = Convert.ToInt32(reader["zakaznikID"]);
                            double cena = Convert.ToInt32(reader["cena"]);
                            string stav = Convert.ToString(reader["stav"]);
                            DateTime datum = Convert.ToDateTime(reader["datum"]);

                            Order objednavka = new Order(objednavkaID, zakaznikID, cena, stav, datum);
                            objednavky.Add(objednavka);
                        }

                    }
                }
                connection.Close();
            }
            return objednavky;
        }


        public List<Order> GetCustomersOrders(int customerID)
        {
            List<Order> objednavky = new List<Order>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM objednavka WHERE zakaznikID = @customerID";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@customerID", customerID);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            int objednavkaID = Convert.ToInt32(reader["objednavkaID"]);
                            int zakaznikID = Convert.ToInt32(reader["zakaznikID"]);
                            double cena = Convert.ToInt32(reader["cena"]);
                            string stav = Convert.ToString(reader["stav"]);
                            DateTime datum = Convert.ToDateTime(reader["datum"]);

                            Order objednavka = new Order(objednavkaID, zakaznikID, cena, stav, datum);
                            objednavky.Add(objednavka);
                        }
                    }
                }
                connection.Close();
            }
            return objednavky;
        }

        public int GetOrderID(int customerID, DateTime date)
        {
            int ID;

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT objednavkaID FROM objednavka WHERE zakaznikID = @zakaznikID and datum = @datum";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@zakaznikID", customerID);
                    command.Parameters.AddWithValue("@datum", date);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();
                            //ID = reader.GetInt32(0);
                        }
                        ID = reader.GetInt32(0);
                    }
                    command.ExecuteScalar();
                }

                connection.Close();
            }
            return ID;
        }

        public async Task UpdateOrderPrice(int orderID, double price)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE objednavka SET cena = @cena WHERE objednavkaID = @objednavkaID";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@objednavkaID", orderID);
                    command.Parameters.AddWithValue("@cena", price);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public async Task UpdateOrderState(int orderID, string state)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE objednavka SET stav = @stav WHERE objednavkaID = @objednavkaID";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@objednavkaID", orderID);
                    command.Parameters.AddWithValue("@stav", state);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }


        /**************************************************************************************************************************************************************************/

        public void CreateNewItem(int objednavkaID, string nazev, int produktID, double cena, int mnozstvi)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO polozka (objednavkaID, nazev, produktID, cena, mnozstvi) values (@objednavkaID,@nazev, @produktID, @cena, @mnozstvi)";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@objednavkaID", objednavkaID);
                    command.Parameters.AddWithValue("@produktID", produktID);
                    command.Parameters.AddWithValue("@mnozstvi", mnozstvi);
                    command.Parameters.AddWithValue("@cena", cena);
                    command.Parameters.AddWithValue("@nazev", nazev);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Item> GetOrderItems(int objednavkaID)
        {
            List<Item> polozky = new List<Item>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT produktID, nazev, cena, mnozstvi FROM polozka WHERE objednavkaID = @objednavkaID";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@objednavkaID", objednavkaID);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int produktID = Convert.ToInt32(reader["produktID"]);
                            string nazev = Convert.ToString(reader["nazev"]);
                            double cena = Convert.ToDouble(reader["cena"]);
                            int mnozstvi = Convert.ToInt32(reader["mnozstvi"]);

                            Item polozka = new Item(produktID, nazev, mnozstvi, cena);
                            polozky.Add(polozka);
                        }
                    }
                }
                connection.Close();
            }
            return polozky;
        }
    }
}
