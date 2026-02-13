using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fubuki.Dtos;
using Fubuki.Services.RestServices;
using MvvmHelpers;
using System.Diagnostics;

namespace Fubuki.ViewModels;

[QueryProperty(nameof(CurrentService), "SelectedService")]
public partial class DeploysViewModel(RenderClients renderClient) : BaseViewModel
{
    [ObservableProperty]
    private Service? currentService;

    [ObservableProperty]
    private bool isCollectionRefreshing;

    [ObservableProperty]
    private bool didInfiniteScrollRequested;

    private readonly RenderClients _renderClients = renderClient;

    [ObservableProperty]
    private ObservableRangeCollection<DeployDtos> deployCardCollection = [];

    [ObservableProperty]
    private bool isPageLoading;

    [RelayCommand]
    async Task LaunchService()
    {
        try
        {
            Console.WriteLine("swiper no swiping!!!");
            Console.WriteLine(CurrentService.ServiceDetails.Url);
            await Browser.Default.OpenAsync(new Uri(CurrentService!.ServiceDetails!.Url), BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    async Task GetMoreDeploys()
    {
        if (IsCollectionRefreshing is true) return;
        if (DidInfiniteScrollRequested is true) return;
        try
        {
            DidInfiniteScrollRequested = true;
            Console.WriteLine("Infinite scroll requested!");

            List<DeployDtos>? deployDtos = await _renderClients.GetDeploysFromCursorId(
                CurrentService!.Id,
                DeployCardCollection.Last().Cursor);

            if (deployDtos is null || deployDtos.Count == 0) return;

            foreach (var deploy in deployDtos)
            {
                if (deploy.Deploy!.Status == "canceled")
                    deploy.IsCanceled = true;

                if (deploy.Deploy.Status == "deactivated")
                    deploy.IsDeactivated = true;

                if (deploy.Deploy.Status == "build_failed")
                    deploy.DidBuildFail = true;
            }

            DeployCardCollection.AddRange(deployDtos);
            Console.WriteLine($"Deploy card collection: {DeployCardCollection.Count}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            DidInfiniteScrollRequested = false;
        }
    }

    [RelayCommand]
    async Task RefreshDeployCardCollection()
    {
        try
        {
            IsCollectionRefreshing = true;
            List<DeployDtos>? deployDtos = [];

            deployDtos = await _renderClients.GetAllDeploys(CurrentService!.Id);

            Console.WriteLine($"refresh data count: {deployDtos!.Count}");
            Console.WriteLine($"Deploy card collection: {DeployCardCollection.Count}");

            if (deployDtos is null || deployDtos.Count == 0) return;

            foreach (var deploy in deployDtos)
            {
                if (deploy.Deploy!.Status == "canceled")
                    deploy.IsCanceled = true;

                if (deploy.Deploy.Status == "deactivated")
                    deploy.IsDeactivated = true;

                if (deploy.Deploy.Status == "build_failed")
                    deploy.DidBuildFail = true;
            }

            DeployCardCollection.Clear();
            DeployCardCollection.AddRange(deployDtos);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            IsCollectionRefreshing = false;
        }
    }
    async partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;
        try
        {
            IsCollectionRefreshing = true;

            List<DeployDtos>? deployDtos = await _renderClients.GetAllDeploys(CurrentService!.Id);
            if (deployDtos is null || deployDtos.Count == 0) return;

            foreach (var deploy in deployDtos)
            {
                if (deploy.Deploy!.Status == "canceled")
                    deploy.IsCanceled = true;

                if (deploy.Deploy.Status == "deactivated")
                    deploy.IsDeactivated = true;

                if (deploy.Deploy.Status == "build_failed")
                    deploy.DidBuildFail = true;
            }

            DeployCardCollection.AddRange(deployDtos);
            Console.WriteLine($"Deploy card collection: {DeployCardCollection.Count}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsCollectionRefreshing = false;
            IsPageLoading = false;
        }
    }
}
