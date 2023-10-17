using Microsoft.AspNetCore.Identity;
using Personnel.Model.Entity;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Service
{
    public class AuthService : IAuthService
     
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IEmployeeService _employeeservice;

        public AuthService(UserManager<AppIdentityUser> userManager, IEmployeeService employeeservice)
        {
            _userManager = userManager;
            _employeeservice = employeeservice;
        }

        //How do we know the HR that creates an employee?
        public async Task<(bool status, string message)> Register(string firstName, string lastName, string phonenumber, string email, string employeeId, string password, string createdby)                   
        {
            //check for existing user
            var checkuser = (email != null) ? await _userManager.FindByEmailAsync(email) : await _userManager.FindByNameAsync(employeeId);
            if (checkuser != null)
            {
                return (false, "emloyee already exist, try another");
            }
            var user = new AppIdentityUser { Email = email, UserName = employeeId, LastName = lastName, FirstName = firstName,  };

            //Create user
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                //save the domain user into the user table                  
                var employeeModel = new EmployeeInformationRequestModel { FirstName = firstName, LastName = lastName, Email = email, CreatedDate = DateTime.Now,CreatedBy = createdby};

                var userdata = await _employeeservice.Create(employeeModel);
                if (userdata.Status)
                {
                    
                          return (true, "Your registration was successful!");
                }
                return (false, "Unable to complete the registration. Please try again");
            }
            return (false, "Unable to complete the registration. Please try again");
        }
    }
}
