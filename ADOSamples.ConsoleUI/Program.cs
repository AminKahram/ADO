using Microsoft.Data.SqlClient;
using System.Data;

string cnnstr = "Server=.; Initial catalog=StoreDB; User Id=sa; Password=123; Encrypt=False ";
SqlConnection cnn = new(cnnstr);
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
Console.ReadKey();