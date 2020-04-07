namespace TechForum.Web.ViewModels.Home
{
    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Url => $"/c/{this.Name}";

        public int PostsCount { get; set; }
    }
}
