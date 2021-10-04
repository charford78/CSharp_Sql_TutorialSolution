using Microsoft.Data.SqlClient;
using System;

namespace CSharp_Sql_Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = "server=localhost\\sqlexpress;database=EdDb;" +
                "trusted_connection=true;";
            var sqlConn = new SqlConnection(connStr);
            sqlConn.Open();
            if(sqlConn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open");
                return;
            }
            Console.WriteLine("Connection opened.");

            // continue sql here
            var sql = "Select * from Student;";
            var cmd = new SqlCommand(sql, sqlConn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var Id = Convert.ToInt32(reader["Id"]);
                var FirstName = reader["Firstname"].ToString();
                var LastName = Convert.ToString(reader["Lastname"]);
                var StateCode = reader["StateCode"].ToString();
                var SAT = reader["SAT"].Equals(DBNull.Value) //ternary statement to check for nulls since SAT column in SQL allows nulls
                    ? (int?)null//if the statment above is true do this.
                    : Convert.ToInt32(reader["SAT"]);//if false do this.
                var GPA = Convert.ToDecimal(reader["GPA"]);
                //using the ternary statement again for the SAT variable.
                var message = $"{Id} | {FirstName} {LastName} | {StateCode} | {(SAT != null ? SAT : "NULL")}" +
                    $" | {GPA}";
                Console.WriteLine($"{ message}");
            }

            //best practice is to enter this reader close statment before you start the body of the while loop.
            reader.Close();

            sqlConn.Close();
        }
    }
}
