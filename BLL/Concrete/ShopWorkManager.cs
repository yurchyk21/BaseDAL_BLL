using BLL.ViewModels;
using DAL.Concrete;
using DAL.Entities;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class ShopWorkManager
    {
        private readonly SqlConnection _con;
        private readonly UserRepsitory _userRepsitory;

        public ShopWorkManager(SqlConnection con)
        {
            _con = con;
            _userRepsitory = new UserRepsitory(_con);
        }

        public int Add(ShopWorkerAddViewModel shopWorker)
        {

            UserAdd userAdd = new UserAdd
            {
                Name = shopWorker.Name,
                SurName = shopWorker.SurName,
                Email = shopWorker.Email


            };

            ICryptoService cryptoService = new PBKDF2();

            //New User
            

            //save this salt to the database
            userAdd.PasswordSalt= cryptoService.GenerateSalt();

            //save this hash to the database
            userAdd.Password = cryptoService.Compute(shopWorker.Password);

            _userRepsitory.Add(userAdd);

            return 0;
        }



    }
}
