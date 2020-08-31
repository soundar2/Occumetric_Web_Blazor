using Occumetric.Server.Areas.Shared;
using Occumetric.Server.Areas.Tenants;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Occumetric.Server.Areas.Applicants
{
    [Table("applicants")]
    public class Applicant : AuditableEntity
    {
        public Applicant()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("tenantId")]
        public int TenantId { get; set; }

        [Column("guid")]
        public string Guid { get; set; }

        [Column("fname")]
        public string Fname { get; set; }

        [Column("mname")]
        public string Mname { get; set; }

        [Column("lname")]
        public string Lname { get; set; }

        [Column("address1")]
        public string Address1 { get; set; }

        [Column("address2")]
        public string Address2 { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("stateName")]
        public string StateName { get; set; }

        [Column("zip")]
        public string Zip { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("dob")]
        public DateTime? dob { get; set; }

        [Column("photoFileName")]
        public string PhotoFileName { get; set; }

        [Column("idCardType")]
        public string IdCardType { get; set; }

        [Column("idCardStateName")]
        public string IdCardStateName { get; set; }

        [Column("idCardNumber")]
        public string IdCardNumber { get; set; }

        [Column("idCardScanFileName")]
        public string IdCardScanFileName { get; set; }

        [Column("exportedToClient")]
        public bool ExportedToClient { get; set; }

        [Column("heightInch")]
        public double HeightInch { get; set; }

        [Column("weightLb")]
        public double WeightLb { get; set; }

        [Column("sex")]
        public string Sex { get; set; }

        [Column("isEmployee")]
        public bool IsEmployee { get; set; }

        [Column("race")]
        public int Race { get; set; }

        [Column("disabilityStatus")]
        public int DisabilityStatus { get; set; }

        //------------------------------------------------
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }
    }
}
