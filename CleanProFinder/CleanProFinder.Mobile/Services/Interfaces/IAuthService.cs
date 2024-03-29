﻿using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IAuthService
{
    public bool IsAuthenticated { get; }
    public bool IsServiceUser { get; }

    void Initialize();
    Task<ServiceResponse<SignUpResultDto>> SignUpServiceUserAsync(string email, string password);
    Task<ServiceResponse<SignUpResultDto>> SignUpServiceProviderAsync(string email, string password);
    Task<ServiceResponse<SignInResultDto>> SignInAsync(string email, string password);
    Task SaveCurrentUserAsync(string bearerToken);
    void Logout();
}