using AutoMapper;
using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechForum.Data.Models;
using TechForum.Services.Mapping;

namespace TechForum.Web.ViewModels.Posts
{
    public class PostViewModel : IMapFrom<Post>, IMapTo<Post>, IHaveCustomMappings
    {

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string AuthorUserName { get; set; }

        public int VotesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>().ForMember(x => x.VotesCount, options =>
              {
                  options.MapFrom(p => p.Votes.Sum(v => (int)v.VoteType));
              });
        }
    }
}
