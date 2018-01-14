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
    public class UserRepsitory
    {

        private readonly SqlConnection _con;

        //ctor
        public UserRepsitory(SqlConnection con)
        {

            _con = con;
        }


        //Users collection
        public ObservableCollection<UserAdd> Users()
        {
            ObservableCollection<UserAdd> users = new ObservableCollection<UserAdd>();

            string query = "SELECT * FROM Users as u";
            try
            {
                using (SqlCommand command = new SqlCommand(query, _con))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new UserAdd
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                FirstName= reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            catch
            {

            }


            return users;

        }


        //Find a
        public int Find(UserAdd user)
        {
            string query = $"SELECT Id FROM dbo.Users WHERE [FirstName]='{user.FirstName}' AND [LastName]='{user.LastName}' AND [Email]='{user.Email.ToLower()}';";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                return command.ExecuteNonQuery();
            }

        }

        //Add User
        public int Add(UserAdd userAdd)
        {
            int id = 0;
            if (Find(userAdd) != 0)
            {
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
