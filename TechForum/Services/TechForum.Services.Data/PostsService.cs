namespace TechForum.Services.Data
{
    using System.Threading.Tasks;

    using TechForum.Data.Common.Repositories;
    using TechForum.Data.Models;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CreateAsync(string title, string content, int categoryId, string userId)
        {
            var post = new Post
            {
                CategoryId = categoryId,
                Content = content,
                Title = title,
                AuthorId = userId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
            return post.Id;
        }
    }
}
