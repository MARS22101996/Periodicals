using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Periodicals.DAL.Context;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Repository.Abstract;
using Periodicals.ViewModels;
using NLog;
using Periodicals.BLL.Services;

namespace Periodicals.Controllers
{
    public class TopicsController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IRepositoryFactory _factory;

        public TopicsController(IRepositoryFactory factory)
        {
            _factory = factory;
        }

        public ActionResult Index1()
        {
            try
            {
                return View(_factory.TopicRepository.Get());
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Topics", "Index1"));
            }
        }
       
        // GET: Topics/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            try
            {
                var topicViewModel = new TopicViewModel();
                return View(topicViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Topics", "Create"));
            }
        }

        // POST: Topics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(TopicViewModel topicViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Name = "";
                    if (TopicService.ExistNameOfTopic(_factory,topicViewModel.NameOfTopic))
                    {
                        ViewBag.Name = "Тема с таким названием уже существует";
                        return View(topicViewModel);
                    }
                    var topicObj = new Topic
                    {
                        NameOfTopic = topicViewModel.NameOfTopic,
                        Description = topicViewModel.Description
                    };
                    _factory.TopicRepository.Add(topicObj);
                    logger.Info("Тема изданий {0} добавлена", topicObj.NameOfTopic);
                    TempData["SuccessMessage"] = "Тема изданий добавлена.";
                    return RedirectToAction("Index1");
                }

                return View(topicViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Topics", "Create"));
            }

        }

        // GET: Topics/Edit/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var topic = _factory.TopicRepository.FindById(id);
                if (topic == null)
                {
                    return View("ResourceNotFound");
                }
                var topicViewModelObj = new TopicViewModel
                {
                    TopicId = topic.TopicId,
                    NameOfTopic = topic.NameOfTopic,
                    Description = topic.Description
                };
                return View(topicViewModelObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Topics", "Edit"));
            }
        }
        


        // POST: Topics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(TopicViewModel topicViewModel)
        {
            try
            {
                var topicObj = new Topic
                {
                    TopicId = topicViewModel.TopicId,
                    NameOfTopic = topicViewModel.NameOfTopic,
                    Description = topicViewModel.Description
                };
                if (ModelState.IsValid)
                {
                    
                    _factory.TopicRepository.Edit(topicObj);
                    logger.Info("Тема изданий {0} изменена", topicObj.NameOfTopic);
                    TempData["SuccessMessage"] = "Тема изданий изменена.";
                    return RedirectToAction("Index1");
                }
                return View(topicViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Topics", "Edit"));
            }

        }
        [HttpGet]
        public ActionResult Edit1(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var topic = _factory.TopicRepository.FindById(id);
                if (topic == null)
                {
                    return View("ResourceNotFound");
                }
                var topicViewModelObj = new TopicViewModel
                {
                    TopicId = topic.TopicId,
                    NameOfTopic = topic.NameOfTopic,
                    Description = topic.Description
                };
                return View(topicViewModelObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Topics", "Edit1"));
            }
        }



        // POST: Topics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit1(TopicViewModel topicViewModel)
        {
            try
            {
                var topicObj = new Topic
                {
                    TopicId = topicViewModel.TopicId,
                    NameOfTopic = topicViewModel.NameOfTopic,
                    Description = topicViewModel.Description
                };
                if (ModelState.IsValid)
                {
                    _factory.TopicRepository.Edit(topicObj);
                    logger.Info("Тема изданий {0} изменена", topicObj.NameOfTopic);
                    TempData["SuccessMessage"] = "Тема изданий изменена.";
                    return RedirectToAction("Index1","Publications", new { id= topicObj.TopicId });
                }
                return View(topicViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Topics", "Edit1"));
            }

        }

        // GET: Topics/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var topic = _factory.TopicRepository.FindById(id);
                if (topic == null)
                {
                    return View("ResourceNotFound");
                }
                return View(topic);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Topics", "Delete"));
            }
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var name = _factory.TopicRepository.FindById(id).NameOfTopic;    
                _factory.TopicRepository.Delete(id);
                logger.Info("Тема изданий {0} была удалена", name);
                TempData["SuccessMessage"] = "Тема изданий удалена.";
                return RedirectToAction("Index1");
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Topics", "DeleteConfirmed"));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _factory.TopicRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
