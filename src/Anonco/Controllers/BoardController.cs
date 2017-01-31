using System.Web.Mvc;

namespace Anonco.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using Database.Entities;
    using Logic.Repositories.Abstract;
    using Microsoft.AspNet.Identity;
    using PagedList;
    using Shared;

    [Authorize]
    public class BoardController : Controller
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private const int _pageSize = 10;

        public BoardController(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        [AllowAnonymous]
        public ActionResult Index(int? page, string sortOrder)
        {
            int currentPage = page ?? 1;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSort = string.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.UserSort = sortOrder == "User" ? "UserDesc" : "User";
            ViewBag.TitleSort = sortOrder == "Title" ? "TitleDesc" : "Title";

            var announcements = _announcementRepository.GetAll();

            announcements = ReorderAnnouncements(announcements, sortOrder);

            return View(announcements.ToPagedList(currentPage, _pageSize));
        }

        private IQueryable<Announcement> ReorderAnnouncements(IQueryable<Announcement> announcements, string sortOrder)
        {
            switch (sortOrder)
            {
                case "User":
                    announcements = announcements.OrderBy(a => a.User.UserName);
                    break;
                case "UserDesc":
                    announcements = announcements.OrderByDescending(a => a.User.UserName);
                    break;
                case "Title":
                    announcements = announcements.OrderBy(a => a.Title);
                    break;
                case "TitleDesc":
                    announcements = announcements.OrderByDescending(a => a.Title);
                    break;
                case "Date":
                    announcements = announcements.OrderBy(a => a.AdditionDate);
                    break;
                default:
                    announcements = announcements.OrderByDescending(a => a.AdditionDate);
                    break;
            }

            return announcements;
        }

        [AllowAnonymous]
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
            if (announcement.UserId != User.Identity.GetUserId()
                && !User.IsInRole(AppStrings.AdminRoleName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                    return RedirectToAction("My");
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

            if (announcement.UserId != User.Identity.GetUserId()
                && !(User.IsInRole(AppStrings.AdminRoleName)
                    || User.IsInRole(AppStrings.ModRoleName)))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

        public ActionResult My(int? page)
        {
            int currentPage = page ?? 1;

            string userId = User.Identity.GetUserId();
            var announcements = _announcementRepository.GetAll();
            announcements = announcements.OrderByDescending(a => a.AdditionDate)
                                         .Where(a => a.UserId == userId);

            return View(announcements.ToPagedList(currentPage, _pageSize));
        }
    }
}