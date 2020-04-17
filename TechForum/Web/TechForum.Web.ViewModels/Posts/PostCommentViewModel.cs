namespace TechForum.Web.ViewModels.Posts
{
    using Ganss.XSS;
    using System;
    
    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class PostCommentViewModel : IMapFrom<Comment>
    {
        private readonly HtmlSanitizer sanitizer = new HtmlSanitizer();

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => this.Sanitize(this.Content);

        public string AuthorUserName { get; set; }

        private string Sanitize(string content)
        {
            this.sanitizer.AllowedTags.Add("iframe");

            return this.sanitizer.Sanitize(content);

        }
    }
}