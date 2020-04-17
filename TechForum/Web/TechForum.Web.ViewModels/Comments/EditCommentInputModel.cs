using System;
using System.Collections.Generic;
using System.Text;
using TechForum.Data.Models;
using TechForum.Services.Mapping;

namespace TechForum.Web.ViewModels.Comments
{
    public class EditCommentInputModel
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; }

        public string AuthorUserName { get; set; }
    }
}
