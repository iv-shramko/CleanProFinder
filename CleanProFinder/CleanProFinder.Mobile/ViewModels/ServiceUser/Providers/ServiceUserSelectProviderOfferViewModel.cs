using CleanProFinder.Mobile.Models;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;

public partial class ServiceUserSelectProviderOfferViewModel : ObservableObject, IQueryAttributable
{
    private readonly IProviderService _providerService;
    private readonly IDialogService _dialogService;

    public ServiceUserSelectProviderOfferViewModel(IProviderService providerService, IDialogService dialogService)
    {
        _providerService = providerService;
        _dialogService = dialogService;
        _providerRequestInteractions = new ObservableCollection<ProviderRequestInteractionInfo>();
        _providerOffers = new ObservableCollection<ProviderOffer>();
    }

    [ObservableProperty]
    private ObservableCollection<ProviderRequestInteractionInfo> _providerRequestInteractions;

    [ObservableProperty]
    private ObservableCollection<ProviderOffer> _providerOffers;

    [ObservableProperty]
    private bool _isRefreshing;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(ProviderRequestInteractions), out var newProviderInteractions))
        {
            var allProviderInteractions = (ICollection<ProviderRequestInteractionInfo>)newProviderInteractions;
            var validProviderInteractions = allProviderInteractions.Where(o => o.Price > 0);
            ProviderRequestInteractions = new ObservableCollection<ProviderRequestInteractionInfo>(validProviderInteractions);
            IsRefreshing = true;
        }

        query.Clear();
    }

    [RelayCommand]
    public async Task LoadServiceProviders()
    {
        IsRefreshing = true;

        var providerOffers = new List<ProviderOffer>();

        foreach (var providerRequestInteraction in ProviderRequestInteractions)
        {
            var response = await _providerService.GetServiceProviderAsync(providerRequestInteraction.ProviderId);

            if (!response.IsSuccess)
            {
                await _dialogService.ShowErrorAlertAsync("Loading Service Providers Failed", response.Error);
                IsRefreshing = false;
                return;
            }

            var providerOffer = new ProviderOffer
            {
                ProviderId = providerRequestInteraction.ProviderId,
                ProviderName = response.Result.Name,
                Description = response.Result.Description,
                Price = providerRequestInteraction.Price,
                Services = response.Result.Services.ToList()
            };

            providerOffers.Add(providerOffer);
        }

        ProviderOffers = new ObservableCollection<ProviderOffer>(providerOffers);

        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task ViewProviderDetails(ProviderOffer providerOffer)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderInfoViewModel.ProviderId), providerOffer.ProviderId }
        };

        await Shell.Current.GoToAsync(nameof(ServiceProviderInfoPage), navigationParameters);
    }

    [RelayCommand]
    private async Task SelectServiceProvider(ProviderOffer providerOffer)
    {
        var providerRequestInteraction = ProviderRequestInteractions.First(i => i.ProviderId == providerOffer.ProviderId);

        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserEditRequestNextViewModel.SelectedServiceProvider), providerRequestInteraction }
        };

        await Shell.Current.GoToAsync("..", navigationParameters);
    }
}