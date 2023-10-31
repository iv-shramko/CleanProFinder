using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels;

public partial class ServiceUserStartingViewModel : ObservableObject
{
    public class ServiceUserStartingMockData
    {
        public string CleaningServiceName { get; set; }
        public string CleaningSpacePhoto { get; set; }
        public float Price { get; set; }
        public string PriceType { get; set; }
        public string Description { get; set; }
    }
    public ServiceUserStartingViewModel() 
    {
        ServiceProviders = new ObservableCollection<ServiceUserStartingMockData>()
        {
            new ServiceUserStartingMockData {
                CleaningServiceName = "Cleaning Service Name",
                CleaningSpacePhoto = "dotnet_bot.png",
                Price = (float)123.45,
                PriceType = "$",
                Description = "Description Description Description Description Description Description Description Description Description Description" },
            new ServiceUserStartingMockData {
                CleaningServiceName = "Cleaning Service Name",
                CleaningSpacePhoto = "dotnet_bot.png",
                Price = (float)543.21,
                PriceType = "$",
                Description = "Description Description Description Description Description Description Description Description Description Description" },
        };
    }

    [ObservableProperty]
    ObservableCollection<ServiceUserStartingMockData> _serviceProviders;

    [ObservableProperty]
    private string _searchQuery;

    [RelayCommand]
    private void Search()
    {

    }
}
