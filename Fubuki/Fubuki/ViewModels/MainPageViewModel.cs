using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fubuki.Dtos;
using Fubuki.Enums;
using Fubuki.Services.RestServices;
using MvvmHelpers;
using System.Diagnostics;

namespace Fubuki.ViewModels;

public partial class MainPageViewModel(
    RenderClients renderClient) : BaseViewModel
{
    string red = "#FFCCCC";
    string green = "#CCFFCC";
    private readonly RenderClients _renderClient = renderClient;

    [ObservableProperty]
    private bool isPageLoading;

    [ObservableProperty]
    private bool isCollectionRefreshing;
    [ObservableProperty]
    private ObservableRangeCollection<RenderDtos> serviceCardCollection = [];

    [RelayCommand]
    async Task GoToDeploys(Service selectedService)
    {
        if (IsBusy is true) return;
        IsBusy = true;
        try
        {

            await Shell.Current.GoToAsync(
                "Deploys",
                true,
                new Dictionary<string, object>
                {
                    { "SelectedService" , selectedService  }
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine("navigating to deploy error: " + ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task StopService(RenderDtos dto)
    {
        if (IsBusy is true) return;
        IsBusy = true;
        try
        {
            await _renderClient.SuspendServiceById(dto.Service!.Id);
            await RefreshServiceCardCollection();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("error stopping service: " + ex.Message);
            await Shell.Current.DisplayAlertAsync("Error", "error stopping service!", "ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task RestartService(RenderDtos dto)
    {
        if (IsBusy is true) return;
        IsBusy = true;
        try
        {
            await _renderClient.RestartServiceById(dto.Service!.Id);
            await RefreshServiceCardCollection();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("error restarting service: " + ex.Message);
            await Shell.Current.DisplayAlertAsync("Error", "error restarting service!", "ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task StartService(RenderDtos dto)
    {
        if (IsBusy is true) return;
        IsBusy = true;
        try
        {
            await _renderClient.ResumeServiceById(dto.Service!.Id);
            await RefreshServiceCardCollection();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("error starting service: " + ex.Message);
            await Shell.Current.DisplayAlertAsync("Error", "error starting service!", "ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task OpenProjectSite(string url)
    {
        try
        {
            await Browser.Default.OpenAsync(new Uri(url), BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("error launching site: " + ex.Message);
            await Shell.Current.DisplayAlertAsync("Error", "error launching site!", "ok");
        }
    }

    [RelayCommand]
    async Task RefreshServiceCardCollection()
    {
        IsCollectionRefreshing = true;
        try
        {
            var data = await _renderClient.GetAllServices();
            if (data is null || data.Count == 0) return;
            foreach (var s in data)
            {
                if (s.Service!.Suspended != StatusEnum.suspended.ToString())
                {
                    s.IsActive = true;
                    s.ServiceCardStatusColor = green;
                    s.IsNotActive = false;
                }
                else
                {
                    s.IsActive = false;
                    s.IsNotActive = true;
                    s.ServiceCardStatusColor = red;
                }
            }

            ServiceCardCollection.ReplaceRange(data);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsCollectionRefreshing = false;
        }
    }

    async partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;
        IsBusy = true;
        try
        {
            if (ServiceCardCollection.Count is not 0) return;
            var data = await _renderClient.GetAllServices();
            if (data is null || data.Count == 0)
            {
                Console.WriteLine("empty data!");
                return;
            }
            foreach (var s in data)
            {
                if (s.Service!.Suspended != StatusEnum.suspended.ToString())
                {
                    s.IsActive = true;
                    s.ServiceCardStatusColor = green;
                    s.IsNotActive = false;
                }
                else
                {
                    s.IsActive = false;
                    s.IsNotActive = true;
                    s.ServiceCardStatusColor = red;
                }
            }

            ServiceCardCollection.AddRange(data);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsBusy = false;
            IsPageLoading = false;
        }
    }
}
