namespace TechForum.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TechForum.Data.Common.Repositories;
    using TechForum.Data.Models;
    using TechForum.Services.Mapping;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int postId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment()
            {
                Content = content,
                ParentId = parentId,
                PostId = postId,
                AuthorId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task Delete(int postid, int commentId)
        {
            var comment = this.commentsRepository.All().Where(x => x.Id == commentId).FirstOrDefault();
            if (comment != null)
            {
                await this.RemoveChildren(commentId);
                this.commentsRepository.Delete(comment);
                await this.commentsRepository.SaveChangesAsync();
            }
        }

        public async Task Edit(int commentId, string content)
        {
            var comment = this.commentsRepository.All().Where(x => x.Id == commentId).FirstOrDefault();

            comment.Content = content;

            this.commentsRepository.Update(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetByUserId<T>(string userId)
        {
            var query = this.commentsRepository.All().Where(x => x.AuthorId == userId);

            return query.To<T>().ToList();
        }

        public bool IsInPostId(int commentId, int postId)
        {
            var commentPostId = this.commentsRepository.All().Where(x => x.Id == commentId).Select(x => x.PostId).FirstOrDefault();

            return commentPostId == postId;

        }

        public async Task RemoveChildren(int commentId)
        {
            var children = this.commentsRepository.All().Where(x => x.ParentId == commentId).ToList();
            foreach (var childComment in children)
            {
                await this.RemoveChildren(childComment.Id);
                this.commentsRepository.Delete(childComment);
                await this.commentsRepository.SaveChangesAsync();

            }

        }
    }
}
