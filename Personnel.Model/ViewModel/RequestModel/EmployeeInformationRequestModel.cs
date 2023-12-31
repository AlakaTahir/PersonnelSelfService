﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.RequestModel
{
    public class EmployeeInformationRequestModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        

    }
}
