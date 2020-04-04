namespace TechForum.Web.ViewModels.Categories
{
    using System;

    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class PostInCategoryViewModel : IMapFrom<Post>
    {
        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Preview => this.Content?.Length > 50 ? this.Content?.Substring(0, 50) + "..."
            : this.Content;

        public string AuthorUserName { get; set; }

        public int CommentsCount { get; set; }

    }
}