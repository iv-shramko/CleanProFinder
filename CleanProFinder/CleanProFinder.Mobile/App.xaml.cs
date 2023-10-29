﻿using CleanProFinder.Mobile.Services;

namespace CleanProFinder.Mobile
{
    public partial class App : Application
    {
        public App(IAuthService authService)
        {
            InitializeComponent();
            
            authService.Initialize();

            MainPage = new AppShell();

            if (authService.IsAuthenticated)
            {
                Shell.Current.GoToAsync(authService.IsCustomer
                    ? "//CustomerStartingPage"
                    : "//ServiceProviderStartingPage");
            }
            else
            {
                Shell.Current.GoToAsync("//RolePage");
            }
        }
    }
}