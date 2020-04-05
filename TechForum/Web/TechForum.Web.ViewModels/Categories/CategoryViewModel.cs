namespace TechForum.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<PostInCategoryViewModel> Posts { get; set; }

    }
}
