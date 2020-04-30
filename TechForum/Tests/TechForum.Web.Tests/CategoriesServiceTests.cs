using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using TechForum.Data;
using TechForum.Data.Models;
using TechForum.Data.Repositories;
using TechForum.Services.Data;
using TechForum.Services.Mapping;
using Xunit;

namespace TechForum.Web.Tests
{
    public class CategoriesServiceTests
    {
        [Fact]
        public void TestGetAll()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoriesService = new CategoriesService(repository);
            repository.AddAsync(new Category { Name = "Test" });
            repository.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);
            var categoriesCount = categoriesService.GetAll<MyTestCategory>().Count();

            Assert.Equal(1, categoriesCount);
        }

        [Fact]
        public void TestGetByName()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));
            var categoriesService = new CategoriesService(repository);
            repository.AddAsync(new Category { Name = "Test" });
            repository.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);
            var category = categoriesService.GetByName<MyTestCategory>("Test");

            Assert.Equal("Test", category.Name);

        }

        public class MyTestCategory : IMapFrom<Category>
        {
            public string Name { get; set; }
        }
    }
}
