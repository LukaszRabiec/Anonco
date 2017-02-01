using System.Web.Mvc;

namespace Anonco.Controllers
{
    using System.Linq;
    using System.Net;
    using Database.Entities;
    using Logic.Repositories.Abstract;
    using PagedList;
    using Shared;

    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult Index(int? page, string sortOrder)
        {
            int currentPage = page ?? 1;
            int pageSize = 10;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.LastSort = string.IsNullOrEmpty(sortOrder) ? "Last" : "";
            ViewBag.FirstSort = sortOrder == "First" ? "FirstDesc" : "First";
            ViewBag.UserSort = sortOrder == "User" ? "UserDesc" : "User";

            var users = _userRepository.GetAll();

            users = ReorderUsers(users, sortOrder);

            return View(users.ToPagedList(currentPage, pageSize));
        }

        private IQueryable<User> ReorderUsers(IQueryable<User> users, string sortOrder)
        {
            switch (sortOrder)
            {
                case "User":
                    users = users.OrderBy(u => u.UserName);
                    break;
                case "UserDesc":
                    users = users.OrderByDescending(u => u.UserName);
                    break;
                case "First":
                    users = users.OrderBy(u => u.FirstName);
                    break;
                case "FirstDesc":
                    users = users.OrderByDescending(u => u.FirstName);
                    break;
                case "Last":
                    users = users.OrderBy(u => u.LastName);
                    break;
                default:
                    users = users.OrderByDescending(u => u.LastName);
                    break;
            }

            return users;
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View("Error", HttpStatusCode.BadRequest);
            }

            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return View("Error", HttpStatusCode.NotFound);
            }

            if (user.UserName == AppStrings.AdminUserName)
            {
                TempData[AppStrings.TempMessage] = AppStrings.ErrorAdminDelete;
                return RedirectToAction("Index");
            }

            try
            {
                _userRepository.Delete(user);
                _userRepository.SaveChanges();
            }
            catch
            {
                TempData[AppStrings.TempMessage] = AppStrings.ErrorOperation;
                return RedirectToAction("Index");
            }

            TempData[AppStrings.TempMessage] = AppStrings.SuccessDelete;
            return RedirectToAction("Index");
        }
    }
}