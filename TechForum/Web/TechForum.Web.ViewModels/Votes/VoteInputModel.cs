using System;
using System.Collections.Generic;
using System.Text;

namespace TechForum.Web.ViewModels.Votes
{
    public class VoteInputModel
    {
        public int PostId { get; set; }

        public bool IsUpVote { get; set; }
    }
}
