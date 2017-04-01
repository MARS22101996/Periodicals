using Periodicals.DAL.Entities;
using Periodicals.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.BLL.Services
{
    public static class PublicationService
    {
        /// <summary>
        /// Method for get all get all publications of the topic
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Publication> GetPublicationsOfTopic(IRepositoryFactory factory, int id)
        {
            return factory.PublicationRepository.Get().Where(a => a.Topics.Any(c => c.TopicId == id)).ToList();
        }
        /// <summary>
        ///  Method for get all publications which contain the name
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="name"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public static List<Publication> PublicationsWhichContainTheName(IRepositoryFactory factory, string name, int topicId)
        {
            return factory.PublicationRepository.Get().Where(a => a.NameOfPublication.Contains(name) && a.Topics.Any(c => c.TopicId == topicId)).ToList();
        }
        /// <summary>
        /// Method for get all publications ordered by name
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public static List<Publication> PublicationsOrderedByName(IRepositoryFactory factory, int topicId)
        {
            return factory.PublicationRepository.Get().Where(p => p.Topics.Any(c => c.TopicId == topicId)).OrderBy(p => p.NameOfPublication).ToList();

        }
        /// <summary>
        /// Method for get all publications ordered by price
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public static List<Publication> PublicationsOrderedByPrice(IRepositoryFactory factory, int topicId)
        {
            return factory.PublicationRepository.Get().Where(p => p.Topics.Any(c => c.TopicId == topicId)).OrderBy(p => p.PricePerMonth).ToList();
        }
        /// <summary>
        ///  Method for get all publications which contain the name without topic
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Publication> PublicationsWhichContainTheNameWithoutTopic(IRepositoryFactory factory, string name)
        {
           return factory.PublicationRepository.Get().Where(a => a.NameOfPublication.Contains(name)).ToList();
        }
        /// <summary>
        ///  Method for get all publications ordered by name without topic
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Publication> PublicationsOrderedByNameWithoutTopic(IRepositoryFactory factory)
        {
            return factory.PublicationRepository.Get().OrderBy(p => p.NameOfPublication).ToList();
        }
        /// <summary>
        /// Method for get all publications ordered by price without topic
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static List<Publication> PublicationsOrderedByPriceWithoutTopic(IRepositoryFactory factory)
        {
            return factory.PublicationRepository.Get().OrderBy(p => p.PricePerMonth).ToList();
        }
        /// <summary>
        /// Method for check for name of publication
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ExistNameOfPublication(IRepositoryFactory factory,string name)
        {
            return factory.PublicationRepository.Get().Any(c => c.NameOfPublication == name);
        }
    }
}
