namespace TechForum.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TechForum.Services.Data;
    using TechForum.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

            return this.View(viewModel);
        }
    }
}
