using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class CreateTaskCategoryDto
    {
        [Required]
        [StringLength(40, MinimumLength = 4)]
        public string Name { get; set; }

        private List<int> _industryIds;

        public List<int> IndustryIds
        {
            get => _industryIds ?? (_industryIds = new List<int>());
            set => _industryIds = value;
        }
    }
}
