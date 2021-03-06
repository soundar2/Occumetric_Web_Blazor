﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Shared
{
    public class MasterTaskViewModel : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string EffortType { get; set; }

        public double WeightLb { get; set; }

        public string FromHeight { get; set; }

        public string ToHeight { get; set; }

        public int IntFromHeight { get; set; }

        public int IntToHeight { get; set; }

        public string CarryDistance { get; set; }

        public string ShortDescription { get; set; }

        public double LiftingIndex { get; set; }

        public string SnooksMale { get; set; }

        public string SnooksFemale { get; set; }

        public string LiftDurationType { get; set; }

        public string LiftFrequencyType { get; set; }
        public int IndustryId { get; set; }

        private List<TaskCategoryViewModel> _taskCategoryViewModels;

        [NotMapped]
        public bool IsChecked { get; set; }

        public virtual List<TaskCategoryViewModel> TaskCategoryViewModels
        {
            get => _taskCategoryViewModels ?? (_taskCategoryViewModels = new List<TaskCategoryViewModel>());
            set => _taskCategoryViewModels = value;
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
