namespace Occumetric.Shared
{
    public class TaskCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IndustryId { get; set; }
        public IndustryViewModel IndustryViewModel { get; set; }
    }
}
