using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Mobile.Views.ServiceUser.Providers;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

public partial class ServiceUserAddRequestNextViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceUserAddRequestNextViewModel(IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
        _serviceProviders = new ObservableCollection<ProviderRequestInteractionInfo>();
    }

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private ObservableCollection<ProviderRequestInteractionInfo> _serviceProviders;

    [ObservableProperty]
    private RequestFullInfoDto _request;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(Request), out var newRequest))
        {
            Request = (RequestFullInfoDto)newRequest;

            if (Request.ProvidersInteractions != null)
            {
                foreach (var provider in Request.ProvidersInteractions)
                {
                    ServiceProviders.Add(provider);
                }
            }
        }

        if (query.TryGetValue(nameof(ServiceProviders), out var newServiceProviders))
        {
            var providers = (ICollection<ProviderRequestInteractionInfo>)newServiceProviders;
            var existingProviderIds = ServiceProviders.Select(existing => existing.ProviderId);
            var providersToAdd = providers.Where(provider => !existingProviderIds.Contains(provider.ProviderId)).ToList();

            foreach (var provider in providersToAdd)
            {
                ServiceProviders.Add(provider);
            }
        }

        query.Clear();
    }

    [RelayCommand]
    private async Task AddRequest()
    {
        var response =
            await _requestService.AddRequestAsync(Request.PremiseId, Request.Services, Description, ServiceProviders);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("//ServiceUserRequestsPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Adding Request failed", response.Error);
    }

    [RelayCommand]
    private async Task SelectServiceProviders()
    {
        Request.Description = Description;

        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserSelectServiceProvidersViewModel.ExistingProviders), ServiceProviders }
        };

        await Shell.Current.GoToAsync(nameof(ServiceUserSelectServiceProvidersPage), navigationParameters);
    }

    [RelayCommand]
    private async Task ViewProviderDetails(ProviderRequestInteractionInfo provider)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderInfoViewModel.ProviderId), provider.ProviderId }
        };

        await Shell.Current.GoToAsync(nameof(ServiceProviderInfoPage), navigationParameters);
    }

    [RelayCommand]
    private void RemoveServiceProvider(ProviderRequestInteractionInfo provider)
    {
        ServiceProviders.Remove(provider);
    }

    [RelayCommand]
    public async Task GoBackToPreviousStep()
    {
        Request.Description = Description;
        Request.ProvidersInteractions = ServiceProviders.ToList();

        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserAddRequestViewModel.Request), Request }
        };

        await Shell.Current.GoToAsync("..", navigationParameters);
    }
}
