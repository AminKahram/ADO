using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ADOSamples.ConsoleUI
{
    public class SqlSample
    {
        private SqlConnection sqlConnection;
        static string cnnstr { set; get; } = "Server=.; Initial catalog=StoreDB; User Id=sa; Password=123; Encrypt=False ";
        static SqlConnection cnn = new(cnnstr);

        public SqlSample()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = "StoreDB";
            builder.DataSource = ".";
            builder.Password = "123";
            builder.UserID = "sa";
            builder.Encrypt = false;
            builder.ConnectTimeout = 1000;
            builder.CommandTimeout = 1000;
            sqlConnection = new(builder.ConnectionString);

        }
        public static void FirstSample()
        {

            cnn.Open();
            //Console.WriteLine(cnn.State);
            //Console.ReadKey();

            //cnn.Close();
            //Console.WriteLine(cnn.State);

            SqlCommand sqlCommand = cnn.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.CommandText = "Select * from Categories";
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader["Id"]}\t\t Name: {reader["Name"]}");
            }

            cnn.Close();
        }

        public static void WorkingWithConnection() 
        {
            cnn.Open();
            Console.WriteLine(cnn.Database);
            Console.WriteLine(cnn.DataSource);
            Console.WriteLine(cnn.CommandTimeout);
            Console.WriteLine(cnn.ConnectionTimeout);

            cnn.Close();

        }

        public static void ConnectionBuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = "StoreDB";
            builder.DataSource = ".";
            builder.Password = "123";
            builder.UserID = "sa";
            builder.Encrypt = false;
            builder.ConnectTimeout = 1000;
            builder.CommandTimeout = 1000;
            SqlConnection sqlConnection = new(builder.ConnectionString);

            sqlConnection.Open();

            Console.WriteLine(sqlConnection.Database);
            Console.WriteLine(sqlConnection.DataSource);
            Console.WriteLine(sqlConnection.CommandTimeout);
            Console.WriteLine(sqlConnection.ConnectionTimeout);

            sqlConnection.Close();

        }

        public void TestCommand()
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.Text,
                CommandText = "Select * from Categories"
            };
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader["Id"]}\t\t Name: {reader["Name"]}");
            }

            sqlConnection.Close();
        }

        public void TestReader()
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.Text,
                CommandText = "Select * from Categories"
            };
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader.GetName(i));
                    Console.Write(":");

                    Console.Write(reader.GetValue(i));
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
            
            sqlConnection.Close();
        }

        public void TestReaderMultiple()
        {
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.Text,
                //CommandText = "Select * from Categories;Select * from Products"
                CommandText = "MultioleResult"
            };
            var reader = sqlCommand.ExecuteReader();

            do
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetName(i));
                        Console.Write(":");

                        Console.Write(reader.GetValue(i));
                        Console.Write("\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("".PadLeft(100,'-'));
            }while(reader.NextResult());
            
            sqlConnection.Close();
        }

        public void AddProduct(int categoryId, string productName, string description, int price)
        {
            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.Text,
                CommandText = $"Insert into Products(Name,Description,CategoryId,Price) values ('{productName}','{description}',{categoryId},{price})"
            };
            sqlConnection.Open();
            int result  = sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"Affacted row is {result}");
            sqlConnection.Close();

        }

        public void AddProductParameter(int categoryId, string productName, string description, int price)
        {
            SqlParameter categoryIdParam = new()
            {
                ParameterName = "@CategoryId",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = categoryId
            };

            SqlParameter productNameParam = new()
            {
                ParameterName = "@Name",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = productName
            };

            SqlParameter descriptionParam = new()
            {
                ParameterName = "@Description",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = description
            };

            SqlParameter priceParam = new()
            {
                ParameterName = "@Price",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = price
            };

            SqlCommand sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.Text,
                CommandText = $"Insert into Products(Name,Description,CategoryId,Price) values (@Name,@Description,@CategoryId,@Price)"
            };
            sqlConnection.Open();
            
            sqlCommand.Parameters.Add(categoryIdParam);
            sqlCommand.Parameters.Add(productNameParam);
            sqlCommand.Parameters.Add(descriptionParam);
            sqlCommand.Parameters.Add(priceParam);


            int result = sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"Affacted row is {result}");

            sqlConnection.Close();

        }

        public void AddTransactional(string categoryName, int categoryId, string productName, string description, int price)
        {
            SqlParameter categoryNameParam = new()
            {
                ParameterName = "@Name",
                DbType = DbType.String,
                SqlValue = categoryName
            };
            SqlParameter categoryIdParam = new()
            {
                ParameterName = "@CategoryId",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = categoryId
            };
            SqlParameter productNameParam = new()
            {
                ParameterName = "@NameProduct",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = productName
            };
            SqlParameter descriptionParam = new()
            {
                ParameterName = "@Description",
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Value = description
            };
            SqlParameter priceParam = new()
            {
                ParameterName = "@Price",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Value = price
            };

            SqlTransaction transaction = null;

            SqlCommand addProduct = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.Text,
                CommandText = $"Insert into Products(Name,Description,CategoryId,Price) values (@NameProduct,@Description,@CategoryId,@Price)"
            };
            sqlConnection.Open();

            addProduct.Parameters.Add(categoryIdParam);
            addProduct.Parameters.Add(productNameParam);
            addProduct.Parameters.Add(descriptionParam);
            addProduct.Parameters.Add(priceParam);

            SqlCommand addCategory = new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.Text,
                CommandText = $"Insert into Categories(Name) values (@Name)"
            };
            addCategory.Parameters.Add(categoryNameParam);
            try
            {
                transaction = sqlConnection.BeginTransaction();
                int resultprod = addProduct.ExecuteNonQuery();
                int resultcat = addCategory.ExecuteNonQuery();
                transaction.Commit();
                Console.WriteLine($"Affacted row in category is {resultcat}");
                Console.WriteLine($"Affacted row in category is {resultprod}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }


            sqlConnection.Close();
        }

        public void SimpleBulkInsert()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            SqlCommand command = new SqlCommand
            {
                CommandType = CommandType.Text,
                Connection = sqlConnection,
            };
            sqlConnection.Open( );
            for (int i = 0; i < 1000; i++)
            {
                command.CommandText = $"Insert into BulkTable(Name,Description) values ('Name {i}','Description {i}' )";
                command.ExecuteNonQuery();
            }
            stopwatch.Stop();
            sqlConnection.Close();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        public void SimpleBulkCopyInsert()
        {
            

            SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlConnection);
            sqlBulk.DestinationTableName = "BulkTable";
            sqlConnection.Open();

            DataTable dt = new();
            dt.Columns.Add(new DataColumn("Name"));
            dt.Columns.Add(new DataColumn("Description"));
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0;i < 1000;i++) 
            {
                dt.Rows.Add(new object[] { $"Name {i}", $"Description {i}" });
            }

            sqlBulk.WriteToServer(dt);

            stopwatch.Stop();
            sqlConnection.Close();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
