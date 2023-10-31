using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels;

public class ServiceUserStartingMockData
{
    public string CleaningServiceName { get; set; }
    public string CleaningSpacePhoto { get; set; }
    public float Price { get; set; }
    public string PriceType { get; set; }
    public string Description { get; set; }
}

public partial class ServiceUserStartingViewModel : ObservableObject
{
    public ServiceUserStartingViewModel() 
    {
        ServiceProviders = new ObservableCollection<ServiceUserStartingMockData>()
        {
            new ServiceUserStartingMockData {
                CleaningServiceName = "Cleaning Service Name 1",
                CleaningSpacePhoto = "dotnet_bot.png",
                Price = (float)123.45,
                PriceType = "$",
                Description = "Description Description Description Description Description Description Description Description Description" },
            new ServiceUserStartingMockData {
                CleaningServiceName = "Cleaning Service Name 2",
                CleaningSpacePhoto = "dotnet_bot.png",
                Price = (float)543.21,
                PriceType = "$",
                Description = "Description Description Description Description Description Description Description Description Description Description" },
            new ServiceUserStartingMockData {
                CleaningServiceName = "Cleaning Service Name 3",
                CleaningSpacePhoto = "dotnet_bot.png",
                Price = (float)341.34,
                PriceType = "$",
                Description = "Description Description Description Description Description Description Description Description" },
            new ServiceUserStartingMockData {
                CleaningServiceName = "Cleaning Service Name 4",
                CleaningSpacePhoto = "dotnet_bot.png",
                Price = (float)823.27,
                PriceType = "$",
                Description = "Description Description Description Description Description Description Description Description Description Description" },
            new ServiceUserStartingMockData {
                CleaningServiceName = "Cleaning Service Name 5",
                CleaningSpacePhoto = "dotnet_bot.png",
                Price = (float)533.43,
                PriceType = "$",
                Description = "Description Description Description Description Description Description Description Description Description" },
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
