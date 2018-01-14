using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DAL.Entities;
using System.Collections.ObjectModel;

namespace DAL.Concrete
{
    public class ShopWorkerRepository
    {
        private readonly SqlConnection _con;

        //ctor
        public ShopWorkerRepository(SqlConnection con)
        {

            _con = con;
        }

        public int Find(int id)
        {
            string query = $"SELECT Id FROM dbo.ShopWorkers WHERE [Id]='{id}';";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                return command.ExecuteNonQuery();
            }

        }


        //Add one worker
        public void Add(UserAdd userAdd, DateTime hiredDate, bool isLocked)
        {
            //int id = 0;
            if (Find(userAdd.Id) != 0)
            {

                string query = $"INSERT INTO dbo.ShopWorkers " +
            $"(Id, HiredDate, IsLocked) VALUES  ('{userAdd.Id}','{hiredDate}', '{isLocked}'); ";
                using (SqlCommand command = new SqlCommand(query, _con))
                {
                    int res = command.ExecuteNonQuery();
                    //if (res == 1)
                    //{
                    //    query = $"SELECT SCOPE_IDENTITY() as Id";
                    //    command.CommandText = query;
                    //    var reader = command.ExecuteReader();
                    //    if (reader.Read())
                    //    {
                    //        id = int.Parse(reader["Id"].ToString());
                    //    }
                    //}
                }
            }
            //return id;
        }
        public void LockUnlock(ShopWorker worker)
        {
            //int id = 0;
            string query = $"UPDATE dbo.ShopWorkers SET IsLocked = 1^IsLocked WHERE Id = '{worker.Id }';";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                int res = command.ExecuteNonQuery();
                //if (res == 1)
                //{
                //    query = $"SELECT SCOPE_IDENTITY() as Id";
                //    command.CommandText = query;
                //    var reader = command.ExecuteReader();
                //    if (reader.Read())
                //    {
                //        id = int.Parse(reader["Id"].ToString());
                //    }
                //}
            }
            //return id;
        }

        public ObservableCollection<ShopWorker> Workers()
        {
            ObservableCollection <ShopWorker> workers = new ObservableCollection<ShopWorker>();

            string query = $"SELECT dbo.ShopWorkers.Id, dbo.Users.FirstName, dbo.Users.LastName, " +
                           $"dbo.Users.Email, dbo.ShopWorkers.HiredDate, dbo.ShopWorkers.IsLocked " +
                           $"FROM dbo.ShopWorkers INNER JOIN dbo.Users ON dbo.ShopWorkers.Id = dbo.Users.Id";
            try
            {
                using (SqlCommand command = new SqlCommand(query, _con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var worker = new ShopWorker
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                HiredDate = DateTime.Parse(reader["HiredDate"].ToString()),
                                IsLocked = bool.Parse(reader["IsLocked"].ToString())
                            };
                            workers.Add(worker);
                        }
                    }
                }
            }
            catch
            {

            }
            return workers;
        }


        //Delete worker
        public int Delete(ShopWorker worker)
        {
            string query = $"DELETE FROM dbo.ShopWorkers WHERE Id = '{worker.Id}';";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(int id)
        {
            string query = $"DELETE FROM dbo.ShopWorkers WHERE Id = '{id}';";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                return command.ExecuteNonQuery();
            }
        }

    }
}
