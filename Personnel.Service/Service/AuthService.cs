  using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Personnel.Model.Entity;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Model.ViewModel.ResponseModel;
using Personnel.Service.Interface;
using Personnel.Service.Providers.JWT;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Service
{
    public class AuthService : IAuthService
     
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly IEmployeeService _employeeservice;
        private readonly IConfiguration _configuration;
        private readonly IAuthTokenProvider _tokenProvider;
        public AuthService(UserManager<AppIdentityUser> userManager, IEmployeeService employeeservice, SignInManager<AppIdentityUser> signInManager, IConfiguration configuration, IAuthTokenProvider tokenProvider)
        {
            _userManager = userManager;
            _employeeservice = employeeservice;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenProvider = tokenProvider;
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
            var user = new AppIdentityUser { Email = email, UserName = employeeId, LastName = lastName, FirstName = firstName, PhoneNumber = phonenumber };

            //Create user
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                //save the domain user into the user table                  
                var employeeModel = new EmployeeInformationRequestModel { FirstName = firstName, LastName = lastName, Email = email, CreatedDate = DateTime.Now,CreatedBy = createdby, EmployeeId= employeeId, PhoneNumber= phonenumber};

                var userdata = await _employeeservice.Create(employeeModel);
                if (userdata.Status)
                {
                    
                          return (true, "Your registration was successful!");
                }
                return (false, "Unable to complete the registration. Please try again");
            }
            return (false, "Unable to complete the registration. Please try again");
        }
        public async Task<(bool status, string message, string token, string refreshToken)> Login(string username, string password)
        {
            var appUser = await _userManager.FindByNameAsync(username) ?? await _userManager.FindByEmailAsync(username);
            if (appUser != null)
            {
                var signin = await _signInManager.PasswordSignInAsync(username, password, false, false);
                if (signin.Succeeded)
                {
                    var userdata = await _employeeservice.GetByEmployeeIdOrEmail(username);
                    if (userdata != null)
                    {
                        // TODO: Track all login history.                      
                        var refreshToken = _tokenProvider.GenerateRefreshToken();
                        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshExpireDays"]));

                        
                        var tokenModel = new AuthTokenModel
                        {
                            Id = userdata.Id,
                            Email = userdata.Email,
                            EmployeeId = userdata.EmployeeId,
                            FirstName = userdata.FirstName,
                            LastName = userdata.LastName,
                            UserName = username,
                        };

                        ////add claims
                        var token = _tokenProvider.GenerateJwtToken(tokenModel, out List<Claim> claims);

                        //check ifuser claim exist before
                        var userClaims = await _userManager.GetClaimsAsync(appUser);
                        if (userClaims.Count < 1)
                        {
                            await _userManager.AddClaimsAsync(appUser, claims);
                        }

                        return (true, "You have successfully logged in", token, refreshToken);
                    }
                }
                return (false, "Email or password not correct, check and try again.", null, null);
            }
            return (false, "User not found, check and try again.", null, null);
        }
        public async Task<string> Logout()
        {
            await _signInManager.SignOutAsync();
            return "Logout Successful";
        }
        public async Task<BaseResponseModel> ChangePassword(string employeeid, string oldPassword, string newPassword)
        {
            var employee = await _userManager.FindByNameAsync(employeeid);
            if (employee != null)
            {
                var change = await _userManager.ChangePasswordAsync(employee, oldPassword, newPassword);
                if (change.Succeeded)
                {
                    return new BaseResponseModel() { Status = true, Message = "Password Change Successfull" };
                }
            }
            return new BaseResponseModel() { Status = false, Message = "Operation Failed" };
        }

    }
}
