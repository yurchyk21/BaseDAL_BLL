using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{




  public  class UserRepsitory
    {

        private readonly SqlConnection _con;

        //ctor
        public UserRepsitory(SqlConnection con )
      {
            
        _con = con;
     }

        //Add one user
        public int Add (UserAdd userAdd)
        {
            int id=0;
            string query = $"INSERT INTO Users "+
            $"(LastName,FirstName, Email, PasswordSalt,[Password]) "+
            $" VALUES "+
            $" ('{userAdd.LastName}', '{userAdd.FirstName}', '{userAdd.Email}', '{userAdd.PasswordSalt}', '{userAdd.Password}'); ";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                int res = command.ExecuteNonQuery();
                if (res == 1)
                {
                    query = $"SELECT SCOPE_IDENTITY() as UserId";
                    command.CommandText = query;
                   var reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        id = int.Parse(reader["UserId"].ToString());
                    }
                }
            }
            return id;
        }
    }
}
