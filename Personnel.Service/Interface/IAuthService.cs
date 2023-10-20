﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Interface
{
    public interface IAuthService
    {
        Task<(bool status, string message)> Register(string firstName, string lastName, string phonenumber, string email, string employeeId, string password, string createdby);

        Task<(bool status, string message, string token, string refreshToken)> Login(string email, string password)


    }
} 
