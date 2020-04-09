using System.Threading.Tasks;

namespace TechForum.Services.Data
{
    public interface ICommentsService
    {
        Task Create(int postid, string userId, string content, int? parentId = null);

        bool IsInPostId(int commentId, int postId);
    }
}
