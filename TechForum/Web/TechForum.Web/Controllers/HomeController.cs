namespace TechForum.Web.Controllers
{
    using System.Diagnostics;

    using TechForum.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using TechForum.Data;
    using TechForum.Web.ViewModels.Home;
    using System.Linq;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var categories = this.dbContext.Categories.Select(x => new IndexCategoryViewModel
            {
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Title = x.Title,
            }).ToList();

            viewModel.Categories = categories;
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
