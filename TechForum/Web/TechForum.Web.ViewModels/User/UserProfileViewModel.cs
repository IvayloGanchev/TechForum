using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechForum.Data.Models;
using TechForum.Services.Mapping;
using TechForum.Web.ViewModels.Posts;

namespace TechForum.Web.ViewModels.User
{
   public class UserProfileViewModel : IMapFrom<ApplicationUser>
    {
        public UserProfileViewModel()
        {
            this.Posts = new List<PostViewModel>();
            this.Comments = new List<PostCommentViewModel>();
        }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public int Age { get; set; }

        public virtual IEnumerable<PostViewModel> Posts { get; set; }

        public int PostsCount => this.Posts.Count();

        public virtual IEnumerable<PostCommentViewModel> Comments { get; set; }

        public int CommentsCount => this.Comments.Count();
    }
}
