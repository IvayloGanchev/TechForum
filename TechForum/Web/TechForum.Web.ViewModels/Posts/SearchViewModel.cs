namespace TechForum.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class SearchViewModel
    {

        public string SearchString { get; set; }

        public IEnumerable<PostInSearchViewModel> FoundPosts { get; set; }
    }
}
