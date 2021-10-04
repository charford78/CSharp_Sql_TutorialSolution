using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdDbLib
{
    public class MajorsController
    {
        public List<Major> GetAll()
        {
            var connStr = "server=localhost\\sqlexpress;database=EdDb;trusted_connection=true;";
            var sqlConn = new SqlConnection(connStr);
            sqlConn.Open();
            if (sqlConn.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Connection failed to open!");
            }
            // connection opened fine!
            var majors = new List<Major>();
            var sql = "Select * from Major;";
            var cmd = new SqlCommand(sql, sqlConn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var major = new Major()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Code = reader["Code"].ToString(),
                    Description = reader["Description"].ToString(),
                    MinSAT = Convert.ToInt32(reader["MinSAT"])
                };
                majors.Add(major);
            }
            reader.Close();
            sqlConn.Close();
            return majors;
        }        

        public Major? GetByPk(int id)
        {
            var connStr = "server=localhost\\sqlexpress;database=EdDb;trusted_connection=true;";
            var sqlConn = new SqlConnection(connStr);
            sqlConn.Open();
            if(sqlConn.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Connection failed to open!");
            }

            var sql = $"Select * from Major where Id = {id};";
            var cmd = new SqlCommand(sql, sqlConn);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                reader.Close();
                sqlConn.Close();
                return null;
            }
            reader.Read();
            var major = new Major()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Code = reader["Code"].ToString(),
                Description = reader["Description"].ToString(),
                MinSAT = Convert.ToInt32(reader["MinSAT"])
            };
            
            reader.Close();
            sqlConn.Close();
            return major;
        }
    }
}
