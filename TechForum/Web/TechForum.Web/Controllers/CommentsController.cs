﻿namespace TechForum.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TechForum.Data.Models;
    using TechForum.Services.Data;
    using TechForum.Web.ViewModels.Comments;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            var parentId = input.ParentId == 0 ? (int?)null : input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInPostId(parentId.Value, input.PostId))
                {
                    return this.BadRequest();
                }
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.commentsService.Create(input.PostId, userId, input.Content, parentId);

            return this.RedirectToAction("ById", "Posts", new { id = input.PostId });
        }

        [Authorize]
        public async Task<IActionResult> Delete(DeleteCommentInputModel input)
        {
            if
                (input.AuthorUserName != this.User.Identity.Name && input.PostAuthorUserName != this.User.Identity.Name)
            {
                return this.BadRequest();
            }

            await this.commentsService.Delete(input.PostId, input.Id);

            return this.RedirectToAction("ById", "Posts", new { id = input.PostId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentInputModel input)
        {
            if (input.AuthorUserName != this.User.Identity.Name)
            {
                return this.BadRequest();
            }

            await this.commentsService.Edit(input.Id, input.Content);

            return this.RedirectToAction("ById", "Posts", new { id = input.PostId });
        }
    }
}
