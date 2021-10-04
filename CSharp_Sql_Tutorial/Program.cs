using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using EdDbLib;

namespace CSharp_Sql_Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        static void X() { 
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
            var sql = "Select * from Student " +
                        " where gpa between 2.5 and 3.5 " +
                        " order by SAT desc;";
            var cmd = new SqlCommand(sql, sqlConn);
            var reader = cmd.ExecuteReader();
            var students = new List<Student>();
            while (reader.Read())
            {
                var student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.Firstname = reader["Firstname"].ToString();
                student.Lastname = Convert.ToString(reader["Lastname"]);
                student.StateCode = reader["StateCode"].ToString();
                student.SAT = reader["SAT"].Equals(DBNull.Value) //ternary statement to check for nulls since SAT column in SQL allows nulls
                    ? (int?)null//if the statment above is true do this.
                    : Convert.ToInt32(reader["SAT"]);//if false do this.
                student.GPA = Convert.ToDecimal(reader["GPA"]);
                //using the ternary statement again for the SAT variable.
                student.MajorId = reader["MajorId"].Equals(DBNull.Value)
                    ? (int?)null
                    : Convert.ToInt32(reader["MajorId"]);
                Console.WriteLine(student);
                students.Add(student);
            }

            //best practice is to enter this reader close statment before you start the body of the while loop.
            reader.Close();

            sqlConn.Close();
        }
    }
}
