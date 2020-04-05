using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TechForum.Services.Data
{
    public interface IVoteService
    {
        Task VoteAsync(int postId, string userId, bool isUpVote);

        int GetVotes(int postId);
    }
}
