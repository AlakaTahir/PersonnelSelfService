using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.ResponseModel
{
    public class EmployeeResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Unit { get; set; }
        public string GradeLevel { get; set; }
    }
}
