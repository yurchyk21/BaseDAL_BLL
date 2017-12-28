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

        public RoleRepository(SqlConnection con)
        {
            _con = con;
        }


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



    }
}
