namespace TechForum.Web.ViewModels.Posts
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Ganss.XSS;
    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class PostViewModel : IMapFrom<Post>, IMapTo<Post>, IHaveCustomMappings
    {
        private readonly HtmlSanitizer sanitizer = new HtmlSanitizer();

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => this.Sanitize(this.Content);

        public string AuthorUserName { get; set; }

        public int VotesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>().ForMember(x => x.VotesCount, options =>
              {
                  options.MapFrom(p => p.Votes.Sum(v => (int)v.VoteType));
              });
        }

        private string Sanitize(string content)
        {
            this.sanitizer.AllowedTags.Add("iframe");

            return this.sanitizer.Sanitize(content);

        }
    }
}
