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

    private readonly RenderClients _renderClients = renderClient;

    [ObservableProperty]
    private ObservableRangeCollection<DeployDtos> deployCardCollection = [];

    [ObservableProperty]
    private bool isPageLoading;

    [RelayCommand]
    async Task RefreshDeployCardCollection()
    {
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

            DeployCardCollection.ReplaceRange(deployDtos);
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
            IsBusy = true;

            if (deployCardCollection.Count is not 0) return;

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

            DeployCardCollection.ReplaceRange(deployDtos);
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
