using BLL.ViewModels;
using DAL.Concrete;
using DAL.Entities;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly ShopWorkerRepository _shopWorkerRepository;
        

        public ShopWorkManager(SqlConnection con)
        {
            _con = con;
            _userRepsitory = new UserRepsitory(_con);
            _shopWorkerRepository = new ShopWorkerRepository(_con);
        }

        public int Add(ShopWorkerAddViewModel shopWorker)
        {

            UserAdd userAdd = new UserAdd
            {
                LastName = shopWorker.LastName,
                FirstName = shopWorker.FirstName,
                Email = shopWorker.Email
            };

            ICryptoService cryptoService = new PBKDF2();
            userAdd.PasswordSalt= cryptoService.GenerateSalt();
            userAdd.Password = cryptoService.Compute(shopWorker.Password);


            return _userRepsitory.Add(userAdd);
        }
        public ObservableCollection<UserAdd> Users
        {
            get { return _userRepsitory.Users(); }
        }
        public void Delete(object userAdd)
        {
            _userRepsitory.Delete(userAdd as UserAdd);
            _shopWorkerRepository.Delete((userAdd as UserAdd).Id);
        }
        public void SetWorker(object userAdd, DateTime hiredDate, bool isLocked)
        {
            _shopWorkerRepository.Add(userAdd as UserAdd, hiredDate, isLocked);
        }
        public void LockChanger(object worker)
        {
            _shopWorkerRepository.LockUnlock(worker as ShopWorker);
        }
        public ObservableCollection<ShopWorker> Workers
        {
            get { return _shopWorkerRepository.Workers(); }
        }

    }
}
