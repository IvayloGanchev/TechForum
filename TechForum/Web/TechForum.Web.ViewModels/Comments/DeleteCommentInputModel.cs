﻿namespace TechForum.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TechForum.Data.Models;

    public class DeleteCommentInputModel
    {

        public int Id { get; set; }

        public string PostAuthorUserName { get; set; }

        public int PostId { get; set; }

        public int ParentId { get; set; }

        public string AuthorUserName { get; set; }
    }
}
