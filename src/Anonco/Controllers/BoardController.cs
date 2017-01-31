using System.Web.Mvc;

namespace Anonco.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using Database.Entities;
    using Logic.Repositories.Abstract;
    using Microsoft.AspNet.Identity;
    using Shared;

    public class BoardController : Controller
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public BoardController(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public ActionResult Index()
        {
            var announcements = _announcementRepository.GetAll();

            return View(announcements.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var announcement = _announcementRepository.GetById((int)id);

            if (announcement == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(announcement);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var announcement = _announcementRepository.GetById((int)id);

            if (announcement == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(announcement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _announcementRepository.Delete(id);
                _announcementRepository.SaveChanges();
            }
            catch
            {
                TempData[AppStrings.TempError] = AppStrings.ErrorOperation;
                return RedirectToAction("Delete", new { id });
            }

            TempData[AppStrings.TempMessage] = AppStrings.SuccessDelete;
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        //TODO: Dodać autoryzacje
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.UserId = User.Identity.GetUserId();
                announcement.AdditionDate = DateTimeOffset.Now;

                try
                {
                    _announcementRepository.Add(announcement);
                    _announcementRepository.SaveChanges();
                    TempData[AppStrings.TempMessage] = AppStrings.SuccessAdd;
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData[AppStrings.TempError] = AppStrings.ErrorOperation;
                    return View(announcement);
                }
            }

            TempData[AppStrings.TempError] = AppStrings.ErrorOperation;
            return View(announcement);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var announcement = _announcementRepository.GetById((int)id);

            if (announcement == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(announcement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _announcementRepository.Update(announcement);
                    _announcementRepository.SaveChanges();
                }
                catch
                {
                    TempData[AppStrings.TempError] = AppStrings.ErrorOperation;
                    return View(announcement);
                }
            }

            TempData[AppStrings.TempMessage] = AppStrings.SuccessEdit;
            return RedirectToAction("Index");
        }
    }
}