using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Repository.Abstract;
using NLog;

namespace Periodicals.BLL.Services
{
    public static class ReplenishmentService
    {
        /// <summary>
        /// Method for execution of replenishment
        /// </summary>
        /// <param name="_factory"></param>
        /// <param name="userId"></param>
        /// <param name="replenishment"></param>
        /// <param name="logger"></param>
        public static void ExecutionOfReplenishment(IRepositoryFactory _factory, string userId, Replenishment replenishment, Logger logger)
        {
            replenishment.OperationDate = DateTime.Now;
            var account = UserService.GetAccountOfUser(_factory,userId);
            replenishment.PersonalAccountId = account.PersonalAccountId;
            account.Balance += replenishment.Amount;
            _factory.ReplenishmentRepository.Add(replenishment);
            logger.Info("Пользователь {0} пополнил свой счет на {1} грн. Теперь сумма на его счету составляет {2} грн.", account.ApplicationUser.Email, replenishment.Amount, account.Balance);

        }
    }
}
