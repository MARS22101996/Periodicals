using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Periodicals.DAL.Repository.Abstract;
using Periodicals.DAL.Repository.Concrete;
using Moq;
using Periodicals.BLL.Services;
using System.Collections.Generic;
using Periodicals.DAL.Entities;
using Periodicals.Controllers;
using System.Web.Mvc;

namespace Periodicals.Tests
{
    [TestClass]
    public class UserPublicationTest
    {
        [TestMethod]
        public void GetTotalUnpaidSum()
        {
            var p4 = new Publication
            {
                PublicationId = 4,
                NameOfPublication = "БудМайстер",
                Description =
                  @"ЖУРНАЛ БУДМАЙСТЕР - специализированный журнал, который рассчитан на читателей, практикующих в области строительства, ремонта, дизайна и архитектуры. Журнал позиционируется как рекламно-информационное периодическое издание. Материалы, публикуемые в журнале, информируют о состоянии отечественного строительного рынка, перспективах развития строительного комплекса Украины, современных строительных материалах и технологиях.",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 100,
                PricePerMonth = 65,

            };
            var p2 = new Publication
            {
                PublicationId = 2,
                NameOfPublication = "Auto Bild Все ведущие. Всеукраинские издания",
                Description =
                   @"Журнал Auto Bild Все ведущие – это оперативные тесты авто, новости от производителей, история автомобилестроения, эксплуатация, ремонт и практические советы автомобилистам, авто-мото спорт, цены на новые авто. ",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 76,
                PricePerMonth = 60,

            };
            Mock<IRepositoryFactory> mock = new Mock<IRepositoryFactory>();
            mock.Setup(a => a.UserPublicationRepository.Get()).Returns(new List<UserPublication>
            {
                new UserPublication
                {
                UserPublicationId = 1,
                Publication = p4,
                UserId = "1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Period = 4,
                PaymentState = false
                }
            });
            double result = UserPublicationService.GetTotalUnpaidSum(mock.Object);
            Assert.AreEqual(260, result);

        }

        [TestMethod]
        public void GetTotalPaidSum()
        {
            var p4 = new Publication
            {
                PublicationId = 4,
                NameOfPublication = "БудМайстер",
                Description =
                  @"ЖУРНАЛ БУДМАЙСТЕР - специализированный журнал, который рассчитан на читателей, практикующих в области строительства, ремонта, дизайна и архитектуры. Журнал позиционируется как рекламно-информационное периодическое издание. Материалы, публикуемые в журнале, информируют о состоянии отечественного строительного рынка, перспективах развития строительного комплекса Украины, современных строительных материалах и технологиях.",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 100,
                PricePerMonth = 65,

            };
            var p2 = new Publication
            {
                PublicationId = 2,
                NameOfPublication = "Auto Bild Все ведущие. Всеукраинские издания",
                Description =
                   @"Журнал Auto Bild Все ведущие – это оперативные тесты авто, новости от производителей, история автомобилестроения, эксплуатация, ремонт и практические советы автомобилистам, авто-мото спорт, цены на новые авто. ",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 76,
                PricePerMonth = 60,

            };
            Mock<IRepositoryFactory> mock = new Mock<IRepositoryFactory>();
            
            mock.Setup(a => a.UserPublicationRepository.Get()).Returns(new List<UserPublication>
            {
                new UserPublication
                {
                UserPublicationId = 1,
                Publication = p4,
                UserId = "1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Period = 4,
                PaymentState = false
                },
                 new UserPublication
                {
                UserPublicationId = 1,
                Publication = p2,
                UserId = "1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Period = 1,
                PaymentState = true
                },
            });
            double result = UserPublicationService.GetTotalPaidSum(mock.Object);
            Assert.AreEqual(60, result);
        }

        [TestMethod]
        public void GetUnpaidSumForUser()
        {
            var user = new ApplicationUser
            {
                Id = "1",
                Email = "user1@periodicals.com",
                UserName = "user1@periodicals.com",
                FirstName = "User1",
                LastName = "User1"
            };
            var p4 = new Publication
            {
                PublicationId = 4,
                NameOfPublication = "БудМайстер",
                Description =
                  @"ЖУРНАЛ БУДМАЙСТЕР - специализированный журнал, который рассчитан на читателей, практикующих в области строительства, ремонта, дизайна и архитектуры. Журнал позиционируется как рекламно-информационное периодическое издание. Материалы, публикуемые в журнале, информируют о состоянии отечественного строительного рынка, перспективах развития строительного комплекса Украины, современных строительных материалах и технологиях.",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 100,
                PricePerMonth = 65,

            };
            var p2 = new Publication
            {
                PublicationId = 2,
                NameOfPublication = "Auto Bild Все ведущие. Всеукраинские издания",
                Description =
                   @"Журнал Auto Bild Все ведущие – это оперативные тесты авто, новости от производителей, история автомобилестроения, эксплуатация, ремонт и практические советы автомобилистам, авто-мото спорт, цены на новые авто. ",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 76,
                PricePerMonth = 60,

            };
            Mock<IRepositoryFactory> mock = new Mock<IRepositoryFactory>();
          
            mock.Setup(a => a.UserPublicationRepository.Get()).Returns(new List<UserPublication>
            {
                new UserPublication
                {
                UserPublicationId = 1,
                Publication = p4,
                UserId = user.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Period = 4,
                PaymentState = true
                },
                 new UserPublication
                {
                UserPublicationId = 1,
                Publication = p2,
                UserId = user.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Period = 1,
                PaymentState = false
                },
            });
            double result = UserPublicationService.GetUnpaidSumForUser(mock.Object, user.Id);
            Assert.AreEqual(60, result);
        }
        [TestMethod]
        public void GetPaidSumForUser()
        {
           var user = new ApplicationUser
            {
                Id ="1",
                Email = "user1@periodicals.com",
                UserName = "user1@periodicals.com",
                FirstName = "User1",
                LastName = "User1"
            };
            var p4 = new Publication
            {
                PublicationId = 4,
                NameOfPublication = "БудМайстер",
                Description =
                  @"ЖУРНАЛ БУДМАЙСТЕР - специализированный журнал, который рассчитан на читателей, практикующих в области строительства, ремонта, дизайна и архитектуры. Журнал позиционируется как рекламно-информационное периодическое издание. Материалы, публикуемые в журнале, информируют о состоянии отечественного строительного рынка, перспективах развития строительного комплекса Украины, современных строительных материалах и технологиях.",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 100,
                PricePerMonth = 65,

            };
            var p2 = new Publication
            {
                PublicationId = 2,
                NameOfPublication = "Auto Bild Все ведущие. Всеукраинские издания",
                Description =
                   @"Журнал Auto Bild Все ведущие – это оперативные тесты авто, новости от производителей, история автомобилестроения, эксплуатация, ремонт и практические советы автомобилистам, авто-мото спорт, цены на новые авто. ",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 76,
                PricePerMonth = 60,

            };
         
            Mock<IRepositoryFactory> mock = new Mock<IRepositoryFactory>();

            mock.Setup(a => a.UserPublicationRepository.Get()).Returns(new List<UserPublication>
            {
                new UserPublication
                {
                UserPublicationId = 1,
                Publication = p4,
                UserId = user.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Period = 4,
                PaymentState = true
                },

            });
            double result = UserPublicationService.GetPaidSumForUser(mock.Object, user.Id);
            Assert.AreEqual(260, result);
        }
        [TestMethod]
        public void ExistNameOfPublication()
        {
            var p1 = new Publication
            {
                PublicationId = 4,
                NameOfPublication = "БудМайстер",
                Description =
                 @"ЖУРНАЛ БУДМАЙСТЕР - специализированный журнал, который рассчитан на читателей, практикующих в области строительства, ремонта, дизайна и архитектуры. Журнал позиционируется как рекламно-информационное периодическое издание. Материалы, публикуемые в журнале, информируют о состоянии отечественного строительного рынка, перспективах развития строительного комплекса Украины, современных строительных материалах и технологиях.",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 100,
                PricePerMonth = 65,

            };
            var p2 = new Publication
            {
                PublicationId = 2,
                NameOfPublication = "Auto Bild Все ведущие. Всеукраинские издания",
                Description =
                   @"Журнал Auto Bild Все ведущие – это оперативные тесты авто, новости от производителей, история автомобилестроения, эксплуатация, ремонт и практические советы автомобилистам, авто-мото спорт, цены на новые авто. ",
                Periodicity = "ежемесячно",
                Format = "А4",
                Color = "полноцвет",
                Volume = 76,
                PricePerMonth = 60,

            };
            Mock<IRepositoryFactory> mock = new Mock<IRepositoryFactory>();

            mock.Setup(a => a.PublicationRepository.Get()).Returns(new List<Publication>
            {
                p1,
                p2
            });
            bool result = PublicationService.ExistNameOfPublication(mock.Object, "БудМайстер");
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void ExistNameOfTopic()
        {
            var t1 = new Topic
            {
                TopicId = 1,
                NameOfTopic = "Topic1",
                Description= "Topic1"

            };

            var t2 = new Topic
            {
                TopicId = 2,
                NameOfTopic = "Topic2",
                Description = "Topic2"

            };
         
            Mock<IRepositoryFactory> mock = new Mock<IRepositoryFactory>();

            mock.Setup(a => a.TopicRepository.Get()).Returns(new List<Topic>
            {
                t1,
                t2
            });
            bool result = TopicService.ExistNameOfTopic(mock.Object, "БудМайстер");
            Assert.AreEqual(false, result);
        }
        
    }
}
