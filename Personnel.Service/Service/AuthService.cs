using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Personnel.Model.Entity;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Service.Interface;
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

        public AuthService(UserManager<AppIdentityUser> userManager, IEmployeeService employeeservice, SignInManager<AppIdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _employeeservice = employeeservice;
            _signInManager = signInManager;
            _configuration = configuration;
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
        public async Task<(bool status, string message, string token, string refreshToken)> Login(string email, string password)
        {
            var appUser = await _userManager.FindByNameAsync(email) ?? await _userManager.FindByEmailAsync(email);
            if (appUser != null)
            {
                var signin = await _signInManager.PasswordSignInAsync(email, password, false, false);
                if (signin.Succeeded)
                {
                    var userdata = await _employeeservice.GetByEmployerIdOrEmail(email);
                    if (userdata != null)
                    {
                        // TODO: Track all login history.                      
                        var refreshToken = _tokenProvider.GenerateRefreshToken();
                        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshExpireDays"]));

                        var tokenModel = _mapper.Map<AuthTokenModel>(userdata);

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
    }
}
