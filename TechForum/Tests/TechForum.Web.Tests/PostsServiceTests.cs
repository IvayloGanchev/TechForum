namespace TechForum.Web.Tests
{
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

    using static TechForum.Web.Tests.VotesServiceTests;

    public class PostsServiceTests
    {
        [Fact]
        public async void TestCreateAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var postService = new PostsService(repository);

            await postService.CreateAsync("test", "test", 1, Guid.NewGuid().ToString());

            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.GetById<MyTestPost>(1);

            Assert.Equal("test", post.Title);
        }

        [Fact]
        public async void TestGetCountByCategoryId()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var postService = new PostsService(repository);

            await postService.CreateAsync("test", "test", 2, Guid.NewGuid().ToString());

            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.GetById<MyTestPost>(1);

            Assert.Equal(1, postService.GetCountByCategoryId(2));
        }

        [Fact]
        public async void TestGetByUserId()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var postService = new PostsService(repository);
            var userId = Guid.NewGuid().ToString();
            await postService.CreateAsync("test", "test", 2, userId);
            await postService.CreateAsync("test1", "test1", 2, userId);

            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.GetById<MyTestPost>(1);

            Assert.Equal(2, postService.GetByUserId<MyTestPost>(userId).Count());

        }

        [Fact]
        public async void TestSearch()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var postService = new PostsService(repository);
            var userId = Guid.NewGuid().ToString();
            await postService.CreateAsync("searchString", "test", 2, userId);
            await postService.CreateAsync("searchString2", "test", 2, userId);

            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            Assert.Equal(2, postService.Search<MyTestPost>("search").Count());

        }

        [Fact]
        public async void TestGetByCategoryId()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            var postService = new PostsService(repository);
            var userId = Guid.NewGuid().ToString();
            await postService.CreateAsync("test", "test", 2, userId);
            await postService.CreateAsync("test1", "test1", 2, userId);

            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.GetById<MyTestPost>(1);

            Assert.Equal(2, postService.GetByCategoryId<MyTestPost>(2,2).Count());

        }
    }
}
