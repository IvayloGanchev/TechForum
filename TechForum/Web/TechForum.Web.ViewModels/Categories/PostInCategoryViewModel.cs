namespace TechForum.Web.ViewModels.Categories
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;
    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class PostInCategoryViewModel : IMapFrom<Post>
    {

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Preview
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));
                return content?.Length > 300
                    ? content?.Substring(0, 300) + "..."
                    : content;
            }
        }

        public string AuthorUserName { get; set; }

        public int CommentsCount { get; set; }

    }
}