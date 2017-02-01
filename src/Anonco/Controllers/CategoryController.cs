using System.Web.Mvc;

namespace Anonco.Controllers
{
    using Logic.Repositories.Abstract;

    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
            return View(categories);
        }
    }
}