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
        private SqlConnection sqlConn { get; set; }
        
        public MajorsController(Connection connection)
        {
            sqlConn = connection.SqlConnection;
        }
        
        public int Create(Major major)
        {
            var sql = " INSERT Major (Code, Description, MinSAT)" +
                    $" VALUES ('{major.Code}', '{major.Description}', {major.MinSAT});";
            var cmd = new SqlCommand(sql, sqlConn);
            var rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        
        public List<Major> GetAll()
        {
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
            return majors;
        }        

        public Major? GetByPk(int id)
        {
            
            var sql = $"Select * from Major where Id = {id};";
            var cmd = new SqlCommand(sql, sqlConn);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                reader.Close();
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
            return major;
        }
    }
}
