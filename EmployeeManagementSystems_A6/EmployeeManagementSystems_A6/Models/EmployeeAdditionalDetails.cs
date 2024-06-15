using System.Net;

namespace EmployeeManagementSystems_A6.Models
{
 
    public class EmployeeAdditionalDetails : BaseEntity
    {
        public string EmployeeBasicDetailsUId { get; set; }
        public string AlternateEmail { get; set; }
        public string AlternateMobile { get; set; }
        public WorkInfo_ WorkInformation { get; set; }
        public PersonalDetails_ PersonalDetails { get; set; }
        public IdentityInfo_ IdentityInformation { get; set; }
    }

    // Models/WorkInfo_.cs
    public class WorkInfo_
    {
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string LocationName { get; set; }
        public string EmployeeStatus { get; set; } // Terminated, Active, Resigned etc
        public string SourceOfHire { get; set; }
        public DateTime DateOfJoining { get; set; }
    }

    // Models/PersonalDetails_.cs
    public class PersonalDetails_
    {
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
    }

    // Models/IdentityInfo_.cs
    public class IdentityInfo_
    {
        public string PAN { get; set; }
        public string Aadhar { get; set; }
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public string PFNumber { get; set; }
    }

    // Models/Address.cs
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }

}
