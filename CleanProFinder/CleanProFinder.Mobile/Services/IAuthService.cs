﻿using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public interface IAuthService
{
    public bool IsAuthenticated { get; }
    void Initialize();
    Task<ServiceResponse<SignUpResultDto>> SignUp(string email, string password);
    Task<ServiceResponse<SignInResultDto>> SignIn(string email, string password);
    Task SaveCurrentUserAsync(string bearerToken);
}