
namespace TechForum.Web.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TechForum.Data;
    using TechForum.Data.Common.Models;
    using TechForum.Data.Common.Repositories;
    using TechForum.Data.Models;
    using TechForum.Data.Repositories;
    using TechForum.Services.Data;
    using TechForum.Services.Mapping;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public void TestGetPostById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Post { Title = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new PostsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestPost).Assembly);
            var post = postService.GetById<MyTestPost>(1);

            Assert.Equal("test", post.Title);
        }

        public class MyTestPost : IMapFrom<Post>
        {
            public string Title { get; set; }

            public int CategoryId { get; set; }
        }

        [Fact]
        public async Task TwoDownVotesShouldCountOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfRepository<Vote>(new ApplicationDbContext(options.Options));
            var service = new VoteService(repository);

            for (int i = 0; i < 100; i++)
            {
                await service.VoteAsync(1, "1", false);
            }

            for (int i = 0; i < 100; i++)
            {
                await service.VoteAsync(1, "2", false);
            }

            var votes = service.GetVotes(1);
            Assert.Equal(-2, votes);
        }
    } 
}
