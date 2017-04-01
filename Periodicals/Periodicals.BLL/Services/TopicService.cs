using Periodicals.DAL.Entities;
using Periodicals.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodicals.BLL.Services
{
    public static class TopicService
    {
        /// <summary>
        /// Method for get topics which contain selected topics
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="OwnTopics"></param>
        /// <returns></returns>
        public static List<Topic> GetTopicsWhichContainSelectTopics(IRepositoryFactory factory, string[] OwnTopics)
        {
            return factory.TopicRepository.Get().Where(co => OwnTopics.Contains(co.TopicId.ToString())).ToList();
        }
        /// <summary>
        /// Method for check for name of topic
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ExistNameOfTopic(IRepositoryFactory factory, string name)
        {
            return factory.TopicRepository.Get().Any(c => c.NameOfTopic == name);
        }
    }
}
