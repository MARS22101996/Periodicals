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
    public static class UserPublicationService
    {
        /// <summary>
        /// Method for get all unpaid publications
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static List<UserPublication> GetAllUnpaidPublications(IRepositoryFactory factory)
        {
            return factory.UserPublicationRepository.Get().Where(o => o.PaymentState == false).OrderBy(o => o.Publication.NameOfPublication).ToList();
        }
        /// <summary>
        /// Method for get all paid publications
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static List<UserPublication> GetAllPaidPublications(IRepositoryFactory factory)
        {
            return factory.UserPublicationRepository.Get().Where(o => o.PaymentState == true).OrderBy(o => o.Publication.NameOfPublication).ToList();

        }
        /// <summary>
        /// Method for get all subscriptions for the user
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<UserPublication> GetUserPublicationsForConcreteUser(IRepositoryFactory factory, string userId)
        {
            return factory.UserPublicationRepository.Get()
               .AsEnumerable()
               .Where(o => o.UserId == userId)
               .OrderBy(o => o.Publication.NameOfPublication)
               .ToList();
        }
        /// <summary>
        /// Method for get unpaid subscriptions for the user
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<UserPublication> GetUnpaidPublicationsForUser(IRepositoryFactory factory, string userId)
        {
            return factory.UserPublicationRepository.Get()
                .AsEnumerable()
                .Where(o => o.UserId == userId && o.PaymentState == false)
                .OrderBy(o => o.Publication.NameOfPublication)
                .ToList();
        }
        /// <summary>
        ///  Method for get paid subscriptions for the user
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<UserPublication> GetPaidPublicationsForUser(IRepositoryFactory factory, string userId)
        {
            return factory.UserPublicationRepository.Get()
               .AsEnumerable()
               .Where(o => o.UserId == userId && o.PaymentState == true)
               .OrderBy(o => o.Publication.NameOfPublication)
               .ToList();
        }
        /// <summary>
        /// Method for get paid subscriptions for special name and date
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<UserPublication> UserPublicationsWithName(IRepositoryFactory factory, int id, DateTime date)
        {
            return factory.UserPublicationRepository.Get().Where(o => o.PaymentState == true && o.PublicationId == id && o.StartDate <= date && date <= o.EndDate).OrderBy(o => o.Publication.NameOfPublication).ToList();
        }

        /// <summary>
        /// Method for get total paid sum for the subscription
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double GetTotalPaidSumForPublication(IRepositoryFactory factory, int name, DateTime date)
        {
            var userPublications = UserPublicationsWithName(factory,name, date);
            return userPublications.Sum(p => p.Period * p.Publication.PricePerMonth);
        }
        /// <summary>
        /// Method for get total unpaid sum for all subscriptions
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static double GetTotalUnpaidSum(IRepositoryFactory factory)
        {
            var allUnpaidPublications = GetAllUnpaidPublications(factory);
            return allUnpaidPublications.Sum(p => p.Period*p.Publication.PricePerMonth);
        }
        /// <summary>
        /// Method for get total paid sum for all subscriptions
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static double GetTotalPaidSum(IRepositoryFactory factory)
        {
            var allUnpaidPublications = GetAllPaidPublications(factory);
            return allUnpaidPublications.Sum(p => p.Period * p.Publication.PricePerMonth);
        }
        /// <summary>
        /// Method for get total unpaid sum for subscriptions of the user
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static double GetUnpaidSumForUser(IRepositoryFactory factory, string userId)
        {
            var allUnpaidPublications = GetUnpaidPublicationsForUser(factory, userId);
            return allUnpaidPublications.Sum(p => p.Period * p.Publication.PricePerMonth);
        }
        /// <summary>
        /// Method for get total paid sum for subscriptions of the user
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static double GetPaidSumForUser(IRepositoryFactory factory, string userId)
        {
            var allPaidPublications = GetPaidPublicationsForUser(factory,userId);
            return allPaidPublications.Sum(p => p.Period * p.Publication.PricePerMonth);
        }
        /// <summary>
        /// Method for creating the subscription
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="logger"></param>
        public static void CreateSubscription(IRepositoryFactory factory, string name, int id, string userId, Logger logger)
        {
            UserPublication userPublication = new UserPublication();
            userPublication.StartDate = DateTime.Now;
            userPublication.EndDate = userPublication.StartDate.AddMonths(Int16.Parse(name));
            userPublication.UserId = userId;
            userPublication.PublicationId = id;
            userPublication.Period = Int32.Parse(name);
            userPublication.PaymentState = false;
            factory.UserPublicationRepository.Add(userPublication);
         
            
        }
    }
}
