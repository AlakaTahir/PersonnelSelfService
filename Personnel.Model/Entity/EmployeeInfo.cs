using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.Entity
{
    public class EmployeeInfo
    {
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Unit { get; set; }
        public string GradeLevel { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NextOfKin { get; set; }
        public DateTime? DateOfFirstAppointment { get; set; }
        public string Qualification { get; set; }
        public string MaritalStatus { get; set; }
        public string Dependant { get; set; }
        public string PreviousEmployment { get; set; }
        public string LineManager { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public int MonthsWorked { get; set; }
        public int Salary { get; set; }


	}
}
