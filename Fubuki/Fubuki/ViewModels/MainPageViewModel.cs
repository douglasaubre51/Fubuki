using CommunityToolkit.Mvvm.ComponentModel;
using Fubuki.Dtos;
using Fubuki.Services.RestServices;
using MvvmHelpers;
using System.Diagnostics;

namespace Fubuki.ViewModels;

public partial class MainPageViewModel(
    RenderClients renderClient) : BaseViewModel
{
    private readonly RenderClients _renderClient = renderClient;

    [ObservableProperty]
    private bool isPageLoading;

    [ObservableProperty]
    private ObservableRangeCollection<RenderDtos> serviceCardCollection = [];

    async partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;
        try
        {
            var data = await _renderClient.GetAllServices();
            if(data is null || data.Count == 0)
            {
                Console.WriteLine("empty data!");
                return;
            }

            ServiceCardCollection.AddRange(data);
            foreach(var i in data)
            {
                Console.WriteLine("Service Name: " + i.Service!.Name);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsPageLoading = false;
        }
    }
}
