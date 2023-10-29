using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels;

public partial class CustomerStartingViewModel : ObservableObject
{
    public class CustomerStartingMockData
    {
        public string CleaningServiceName { get; set; }
        public string CleaningSpacePhoto { get; set; }
        public float Price { get; set; }
        public string PriceType { get; set; }
        public string Description { get; set; }
    }
    public CustomerStartingViewModel() 
    {
        ServiceProviders = new ObservableCollection<CustomerStartingMockData>()
        {
            new CustomerStartingMockData {
                CleaningServiceName = "Cleaning Service Name",
                CleaningSpacePhoto = "dotnet_bot.png",
                Price = (float)123.45,
                PriceType = "$",
                Description = "Description Description Description Description Description Description Description Description Description Description" },
            new CustomerStartingMockData {
                CleaningServiceName = "Cleaning Service Name",
                CleaningSpacePhoto = "dotnet_bot.png",
                Price = (float)543.21,
                PriceType = "$",
                Description = "Description Description Description Description Description Description Description Description Description Description" },
        };
    }

    [ObservableProperty]
    ObservableCollection<CustomerStartingMockData> _serviceProviders;

    [ObservableProperty]
    private string _searchQuery;

    [RelayCommand]
    private void Search()
    {

    }
}
