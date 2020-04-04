namespace TechForum.Web.ViewModels.Posts
{
    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class CategoryDropdownViewModel :IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}