using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection <Role> Roles ()
        {
            ObservableCollection<Role> roles = new ObservableCollection<Role>();

            string query = "SELECT r.Id, r.Name FROM Roles as r";
            try
            {
                using (SqlCommand command = new SqlCommand(query, _con))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var role = new Role
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString()
                            };
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

        //Find a
        public int Find(string role)
        {
            string query = $"SELECT Id FROM dbo.Roles WHERE [Name]='{role.ToLower()}';";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                return command.ExecuteNonQuery();
            }

        }

            //Add one Role in the Base
            public int Add(string role)
        {
            int id = 0;
            if (/*Find(role) != 0*/true)
            {
                string query = $"INSERT INTO Roles (Name) VALUES ('{role.ToLower()}');";
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
            }
            return id;
        }


    }
}
