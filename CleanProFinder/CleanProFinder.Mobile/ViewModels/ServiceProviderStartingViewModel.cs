using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels;

public partial class ServiceProviderStartingViewModel : ObservableObject
{
    public class ServiceProviderStartingMockData
    {
        public string BuildingAddress { get; set; }
        public string CleaningSpaceType { get; set; }
        public string City { get; set; }
        public float Square { get; set; }
        public string Description { get; set; }
    }
    public ServiceProviderStartingViewModel()
    {
        Premises = new ObservableCollection<ServiceProviderStartingMockData>()
        {
            new ServiceProviderStartingMockData {
                BuildingAddress = "Building Address",
                CleaningSpaceType = "Cleaning Space Type",
                City = "City",
                Square = (float)123.45,
                Description = "Description Description Description Description Description Description Description Description Description Description" },
            new ServiceProviderStartingMockData {
                BuildingAddress = "Building Address",
                CleaningSpaceType = "Cleaning Space Type",
                City = "City",
                Square = (float)543.21,
                Description = "Description Description Description Description Description Description Description Description Description Description" },
        };
    }

    [ObservableProperty]
    ObservableCollection<ServiceProviderStartingMockData> _premises;

    [ObservableProperty]
    private string _searchQuery;

    [RelayCommand]
    private void Search()
    {

    }
}

