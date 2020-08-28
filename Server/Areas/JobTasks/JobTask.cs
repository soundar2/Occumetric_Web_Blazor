using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Jobs
{
    [Table("tasks")]
    public class JobTask
    {
        public JobTask()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("job_id")]
        public int JobId { get; set; }

        [Column("Guid")]
        public string Guid { get; set; }

        [Column("task_name")]
        public string Name { get; set; }

        [Column("effort_type")]
        public string EffortType { get; set; }

        [Column("weight_lb")]
        public double WeightLb { get; set; }

        [Column("from_height")]
        public string FromHeight { get; set; }

        [Column("to_height")]
        public string ToHeight { get; set; }

        [Column("int_from_height")]
        public int IntFromHeight { get; set; }

        [Column("int_to_height")]
        public int IntToHeight { get; set; }

        [Column("carry_distance")]
        public string CarryDistance { get; set; }

        [Column("short_description")]
        public string ShortDescription { get; set; }

        [Column("lifting_index")]
        public double LiftingIndex { get; set; }

        [Column("snooks_male")]
        public string SnooksMale { get; set; }

        [Column("snooks_female")]
        public string SnooksFemale { get; set; }

        [Column("original_task_id")]
        public int? OriginalTaskId { get; set; }

        [Column("lift_duration_type")]
        public string LiftDurationType { get; set; }

        [Column("lift_frequency_type")]
        public string LiftFrequencyType { get; set; }

        public virtual Job Job { get; set; }
    }
}
