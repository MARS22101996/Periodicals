using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Periodicals.DAL.Entities;

namespace Periodicals.DAL.Repository.Abstract
{
    public interface IPersonalAccountRepository : IRepository<PersonalAccount>
    {
       List<PersonalAccount> GetUserAccount(string userId);
    }
}
