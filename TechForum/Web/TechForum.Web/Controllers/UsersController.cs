namespace TechForum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TechForum.Data.Models;
    using TechForum.Services.Data;
    using TechForum.Web.ViewModels.Posts;
    using TechForum.Web.ViewModels.User;

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostsService postsService;
        private readonly ICommentsService commentsService;

        public UsersController(UserManager<ApplicationUser> userManager, IPostsService postsService, ICommentsService commentsService)
        {
            this.userManager = userManager;
            this.postsService = postsService;
            this.commentsService = commentsService;
        }

        public async Task<IActionResult> Profile(string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            var viewModel = new UserProfileViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Posts = this.postsService.GetByUserId<PostViewModel>(user.Id),
                Comments = this.commentsService.GetByUserId<PostCommentViewModel>(user.Id),
                Age = DateTime.Now.Year - user.DateOfBirth.Year,
                Picture = user.Picture,
            };

            return this.View(viewModel);
        }
    }
}
