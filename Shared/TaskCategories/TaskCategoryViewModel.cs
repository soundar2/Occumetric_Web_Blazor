using System;

namespace Occumetric.Shared
{
    public class TaskCategoryViewModel : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
