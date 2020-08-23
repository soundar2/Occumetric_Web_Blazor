using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class UpdateMasterTaskDto
    {
        public UpdateMasterTaskDto()
        {
            EffortType = "Lift";
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EffortType { get; set; }

        public string ShortDescription { get; set; }

        //[Required]
        public double WeightLb { get; set; }

        public string FromHeight { get; set; }

        public string ToHeight { get; set; }

        public string CarryDistance { get; set; }

        public List<TaskCategoryViewModel> _taskCategoryViewModels;

        public List<TaskCategoryViewModel> TaskCategoryViewModels
        {
            get
            {
                return _taskCategoryViewModels ?? new List<TaskCategoryViewModel>();
            }

            set
            {
                _taskCategoryViewModels = value;
            }
        }
    }
}
