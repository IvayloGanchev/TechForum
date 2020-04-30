using TechForum.Data.Models;
using TechForum.Services.Mapping;

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

    public class CommentsServiceTests
    {


        [Fact]
        public async void TestCreateAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var commentsService = new CommentsService(repository);
            var userId = Guid.NewGuid().ToString();
            await commentsService.Create(1, userId, "testContent", 0);

            AutoMapperConfig.RegisterMappings(typeof(MyTestComment).Assembly);

            var comment = commentsService.GetByUserId<MyTestComment>(userId).FirstOrDefault();

            Assert.Equal("testContent", comment.Content);
        }

        [Fact]
        public async void TestDelete()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var commentsService = new CommentsService(repository);
            var userId = Guid.NewGuid().ToString();
            await commentsService.Create(1, userId, "testContent", 0);
            await commentsService.Create(1, userId, "testContent2", 1);

            AutoMapperConfig.RegisterMappings(typeof(MyTestComment).Assembly);

            await commentsService.Delete(1, 1);
            var commentsCount = commentsService.GetByUserId<MyTestComment>(userId).Count();

            Assert.Equal(0, commentsCount);
        }


        [Fact]
        public async void TestEdit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var commentsService = new CommentsService(repository);
            var userId = Guid.NewGuid().ToString();
            await commentsService.Create(1, userId, "testContent", 0);

            AutoMapperConfig.RegisterMappings(typeof(MyTestComment).Assembly);

            await commentsService.Edit(1, "editedContent");

            var editedComment = commentsService.GetByUserId<MyTestComment>(userId).FirstOrDefault();

            Assert.Equal("editedContent", editedComment.Content);
        }

        [Fact]
        public async void TestIsInPostId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));
            var commentsService = new CommentsService(repository);
            var userId = Guid.NewGuid().ToString();
            await commentsService.Create(1, userId, "testContent", 0);

            AutoMapperConfig.RegisterMappings(typeof(MyTestComment).Assembly);

            Assert.True(commentsService.IsInPostId(1, 1));
        }

        public class MyTestComment : IMapFrom<Comment>
        {
            public string Content { get; set; }

            public int ParentId { get; set; }

        }
    }
}

