using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels;

public class ServiceProviderStartingMockData
{
    public string BuildingAddress { get; set; }
    public string CleaningSpaceType { get; set; }
    public string City { get; set; }
    public float Square { get; set; }
    public string Description { get; set; }
}

public partial class ServiceProviderStartingViewModel : ObservableObject
{
    public ServiceProviderStartingViewModel()
    {
        Premises = new ObservableCollection<ServiceProviderStartingMockData>()
        {
            new ServiceProviderStartingMockData {
                BuildingAddress = "Building Address 1 ",
                CleaningSpaceType = "Cleaning Space Type 1",
                City = "City 1",
                Square = (float)123.45,
                Description = "Description Description Description Description Description Description Description Description Description Description" },
            new ServiceProviderStartingMockData {
                BuildingAddress = "Building Address 2",
                CleaningSpaceType = "Cleaning Space Type 2",
                City = "City 2",
                Square = (float)543.21,
                Description = "Description Description Description Description Description Description Description Description Description" },
            new ServiceProviderStartingMockData {
                BuildingAddress = "Building Address 3",
                CleaningSpaceType = "Cleaning Space Type 3",
                City = "City 3",
                Square = (float)343.43,
                Description = "Description Description Description Description Description Description Description Description" },
            new ServiceProviderStartingMockData {
                BuildingAddress = "Building Address 4",
                CleaningSpaceType = "Cleaning Space Type 4",
                City = "City 4",
                Square = (float)1234.62,
                Description = "Description Description Description Description Description Description Description Description Description" },
            new ServiceProviderStartingMockData {
                BuildingAddress = "Building Address 5",
                CleaningSpaceType = "Cleaning Space Type 5",
                City = "City 5",
                Square = (float)33.24,
                Description = "Description Description Description Description Description Description Description Description" },
            new ServiceProviderStartingMockData {
                BuildingAddress = "Building Address 6",
                CleaningSpaceType = "Cleaning Space Type 6",
                City = "City 6",
                Square = (float)862.53,
                Description = "Description Description Description Description Description Description Description Description Description Description" },
            new ServiceProviderStartingMockData {
                BuildingAddress = "Building Address 7",
                CleaningSpaceType = "Cleaning Space Type 7",
                City = "City 7",
                Square = (float)6314.45,
                Description = "Description Description Description Description Description Description Description Description Description" },
        };
    }

    [ObservableProperty]
    private ObservableCollection<ServiceProviderStartingMockData> _premises;

    [ObservableProperty]
    private string _searchQuery;

    [RelayCommand]
    private void Search()
    {

    }
}

