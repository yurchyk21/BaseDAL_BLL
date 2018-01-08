using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DAL.Entities;

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

        //Add one worker
        public int Add(DateTime hiredDate)
        {
            int id = 0;
            string query = $"INSERT INTO dbo.ShopWorkers " +
            $"(HiredDate, IsLocked) VALUES  ('{hiredDate}', 0); ";
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
        public int LockUnlock(UserAdd user)
        {
            int id = 0;
            string query = $"UPDATE dbo.ShopWorkers SET IsLocked = 1^IsLocked WHERE Id = { user.Id };";
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

        //Delete worker
        public int Delete(UserAdd userAdd)
        {
            string query = $"DELETE FROM dbo.ShopWorkers WHERE Id = '{userAdd.Id}';";
            using (SqlCommand command = new SqlCommand(query, _con))
            {
                return command.ExecuteNonQuery();
            }
        }

    }
}
