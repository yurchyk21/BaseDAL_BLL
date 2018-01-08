using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{




    public class UserRepsitory
    {

        private readonly SqlConnection _con;

        //ctor
        public UserRepsitory(SqlConnection con)
        {

            _con = con;
        }

        //Add User
        public int Add(UserAdd userAdd)
        {
            int id = 0;
            string query = $"INSERT INTO dbo.Users " +
            $"(LastName,FirstName, Email, PasswordSalt,[Password]) " +
            $" VALUES " +
            $" ('{userAdd.LastName}', '{userAdd.FirstName}', '{userAdd.Email}', '{userAdd.PasswordSalt}', '{userAdd.Password}'); ";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                int res = command.ExecuteNonQuery();
                if (res == 1)
                {
                    query = $"SELECT SCOPE_IDENTITY() as UserId";
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        id = int.Parse(reader["UserId"].ToString());
                    }
                }
            }
            return id;
        }

        //Delete User
        public int Delete(UserAdd userAdd)
        {
            string query = $"DELETE FROM dbo.Users WHERE Id = '{userAdd.Id}';";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                return command.ExecuteNonQuery();

            }
        }
    }
}
