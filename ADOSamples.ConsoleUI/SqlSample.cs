﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
    }
}
