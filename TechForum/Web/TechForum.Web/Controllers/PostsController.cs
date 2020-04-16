namespace TechForum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TechForum.Data.Common.Repositories;
    using TechForum.Data.Models;
    using TechForum.Services.Data;
    using TechForum.Web.ViewModels.Posts;

    public class PostsController : Controller
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostsService postService;
        private readonly ICategoriesService categoriesService;

        public PostsController(UserManager<ApplicationUser> userManager, IPostsService postsService, ICategoriesService categoriesService,IDeletableEntityRepository<Post> postsRepository)
        {
            this.userManager = userManager;
            this.postService = postsService;
            this.categoriesService = categoriesService;
            this.postsRepository = postsRepository;
        }

        public IActionResult ById(int id)
        {
            var postViewModel = this.postService.GetById<PostViewModel>(id);

            if (postViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(postViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
            var viewModel = new PostCreateInputModel
            {
                Categories = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var postId = await this.postService.CreateAsync(input.Title, input.Content, input.CategoryId, user.Id);

            return this.RedirectToAction(nameof(this.ById), new { id = postId });
        }

        //TODO CHECK IF CURRENT USER IS AUTHOR OF POST
        [Authorize]
        public IActionResult Edit(PostCreateInputModel input)
        {
            
            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
            var viewModel = new PostEditViewModel
            {
                Categories = categories,
                Title = input.Title,
                Content = input.Content,
                CategoryId = input.CategoryId,
                Id = input.Id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitEdit(PostEditInputModel input)
        {
            var post = this.postsRepository.All().Where(x => x.Id == input.Id).FirstOrDefault();

            post.CategoryId = input.CategoryId;
            post.Content = input.Content;
            post.Title = input.Title;


            this.postsRepository.Update(post);
            await this.postsRepository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.ById), new { id = post.Id });

        }

    }
}
