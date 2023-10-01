﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOSamples.ConsoleUI
{
    public class SqlSample
    {
        static string cnnstr { set; get; } = "Server=.; Initial catalog=StoreDB; User Id=sa; Password=123; Encrypt=False ";
        static SqlConnection cnn = new(cnnstr);
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
    }
}
