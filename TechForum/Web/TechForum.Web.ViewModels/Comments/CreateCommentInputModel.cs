 namespace TechForum.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreateCommentInputModel
    {
        public string Content { get; set; }

        public int PostId { get; set; }

        public int ParentId { get; set; }
    }
}
