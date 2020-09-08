using System;
using System.ComponentModel.DataAnnotations;

namespace Occumetric.Shared
{
    public class UpdateApplicantDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Fname { get; set; }

        public string Mname { get; set; }

        [Required]
        public string Lname { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime? dob { get; set; }
        public string Sex { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhotoFileName { get; set; }

        public string IdCardType { get; set; }

        public string IdCardStateName { get; set; }

        public string IdCardNumber { get; set; }

        public bool IsEmployee { get; set; }

        public int Race { get; set; }

        public string DisabilityStatus { get; set; }
    }
}
