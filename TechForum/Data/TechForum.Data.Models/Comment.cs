﻿using System;
using System.Collections.Generic;
using System.Text;
using TechForum.Data.Common.Models;

namespace TechForum.Data.Models
{
    public class Comment : BaseDeletableModel<int>
    {

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}