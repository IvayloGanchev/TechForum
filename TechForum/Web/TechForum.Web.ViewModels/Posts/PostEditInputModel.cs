using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TechForum.Data.Models;
using TechForum.Services.Mapping;

namespace TechForum.Web.ViewModels.Posts
{
   public class PostEditInputModel : IMapTo<Post>, IMapFrom<Post>
    {
        public int Id { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public Category Category { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public string AuthorUserName { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }
    }
}
