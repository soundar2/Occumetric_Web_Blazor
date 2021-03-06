﻿using System;

namespace Occumetric.Shared
{
    public class ApplicantViewModel
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string Guid { get; set; }

        public string Fname { get; set; }

        public string Mname { get; set; }

        public string Lname { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime? dob { get; set; }

        public string PhotoFileName { get; set; }

        public string IdCardType { get; set; }

        public string IdCardStateName { get; set; }

        public string IdCardNumber { get; set; }

        public string Sex { get; set; }

        public bool IsEmployee { get; set; }

        public int Race { get; set; }

        public string DisabilityStatus { get; set; }
    }
}
