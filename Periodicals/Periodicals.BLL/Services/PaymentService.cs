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
    public static class PaymentService
    {
        /// <summary>
        /// Method for get all unpaid subscription of user
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<UserPublication> GetAllUnpaidUserPublications(IRepositoryFactory factory, string userId)
        {
            return factory.UserPublicationRepository.Get().AsEnumerable().Where(o => o.UserId == userId && o.PaymentState == false).ToList();
        }
        /// <summary>
        ///  Method for calculate unpaid amount for the user
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="userId"></param>
        /// <returns></returns>

        public static double CalculateUnpaidAmountForTheUser(IRepositoryFactory factory, string userId)
        {
            var userPublications = GetAllUnpaidUserPublications(factory, userId);
            return userPublications.Sum(p => p.Period*p.Publication.PricePerMonth);
        }
        /// <summary>
        /// Method for the execution of payment
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="userId"></param>
        /// <param name="payment"></param>
        /// <param name="account"></param>
        /// <param name="sum"></param>
        /// <param name="logger"></param>
        public static void ExecutionOfPayment(IRepositoryFactory factory, string userId, Payment payment, PersonalAccount account, double sum, Logger logger)
        {
            var userPublications = GetAllUnpaidUserPublications(factory, userId);
            payment.OperationDate = DateTime.Now;
            payment.PersonalAccountId = account.PersonalAccountId;
            payment.Amount = sum;
            account.Balance -= payment.Amount;
            foreach (var item in userPublications)
            {
                item.StartDate = DateTime.Now;
                item.EndDate = item.StartDate.AddMonths(item.Period);
                item.PaymentState = true;
                logger.Info("Пользователь {0} оплатил подписку на {1} на период {2} месяц(ев) в сумме {3} грн с {4} по {5} ", item.User.Email, item.Publication.NameOfPublication, item.Period, item.Publication.PricePerMonth* item.Period,item.StartDate, item.EndDate);
            }
            factory.PaymentRepository.Add(payment);

        }
    }
}
