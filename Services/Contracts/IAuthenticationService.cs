﻿using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserDtoForRegistration userDtoForRegistration);
        Task<bool> ValidateUser(UserDtoForAuthentication userDtoForAuth);
        Task<string> CreateToken();
    }
}
