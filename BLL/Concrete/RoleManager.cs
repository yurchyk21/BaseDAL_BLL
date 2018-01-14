using DAL.Concrete;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class RoleManager
    {
        private readonly SqlConnection _con;
        private readonly RoleRepository _roleRepsitory;

        public RoleManager(SqlConnection con)
        {
            _con = con;
            _roleRepsitory = new RoleRepository(_con);
        }

        public int Add(string roleName)
        {

            return _roleRepsitory.Add(roleName);
        }
        public ObservableCollection <Role> Roles
        {
            get { return _roleRepsitory.Roles();}
        }

    }
}
