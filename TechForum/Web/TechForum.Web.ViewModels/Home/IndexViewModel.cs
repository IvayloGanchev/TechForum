using System;
using System.Collections.Generic;
using System.Text;

namespace TechForum.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<IndexCategoryViewModel> Categories { get; set; }
    }
}
