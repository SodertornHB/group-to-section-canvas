using GroupToSection.Web.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Web.ViewModel
{
    public class GroupToSectionViewModel
    {
        private IEnumerable<GroupCategoryViewModel> groupCategories = new List<GroupCategoryViewModel>();

        public int CourseId { get; set; }

        public IEnumerable<GroupCategoryViewModel> GroupCategories
        {
            get
            {
                return groupCategories.Where(x => !x.IsEmpty());
            }
            set
            {
                groupCategories = value;
            }
        }

        public bool IsEmpty() => !GroupCategories.Any();

    }
}
