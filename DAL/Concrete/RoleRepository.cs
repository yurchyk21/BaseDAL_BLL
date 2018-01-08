using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
   public class RoleRepository
    {
        private readonly SqlConnection _con;

        //ctor
        public RoleRepository(SqlConnection con)
        {
            _con = con;
        }

        //Return all Roles
        public List<Role> Roles ()
        {

            List<Role> roles = new List<Role>();

            string query = "SELECT r.Id, r.Name FROM Roles as r";
            try
            {
                using (SqlCommand command = new SqlCommand(query, _con))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var role = new Role();
                            role.Id = int.Parse(reader["Id"].ToString());
                            role.Name = reader["Name"].ToString();
                            roles.Add(role);
                        }
                    }
                }
            }
            catch
            {

            }


            return roles;

        }

        //Add one Role in the Base
        public int Add(string role)
        {
            int id = 0;
            string query = $"INSERT INTO dbo.Roles ([Name]) VALUES ('role');";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                int res = command.ExecuteNonQuery();
                if (res == 1)
                {
                    query = $"SELECT SCOPE_IDENTITY() as Id";
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        id = int.Parse(reader["Id"].ToString());
                    }
                }
            }
            return id;
        }


    }
}
