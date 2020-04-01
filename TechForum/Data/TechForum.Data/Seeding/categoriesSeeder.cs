
namespace TechForum.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TechForum.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<string>()
            {
                "Gaming", "Cars", "Programming", "Industrial", "Coronavirus", "Techno", "Education"
            };

            foreach (var cat in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = cat,
                    Description = cat,
                    Title = cat,
                });
            }
        }
    }
}
