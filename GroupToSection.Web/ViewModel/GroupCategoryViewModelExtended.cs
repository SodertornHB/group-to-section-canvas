using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupToSection.Web.ViewModel
{
    public partial class GroupCategoryViewModel 
    {
        public IEnumerable<GroupViewModel> Groups { get; set; } = new List<GroupViewModel>();
        public bool IsEmpty() => Groups == null || !Groups.Any();
    }
} 