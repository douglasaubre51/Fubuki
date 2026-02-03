using Newtonsoft.Json;

namespace Fubuki.Dtos;

public class RenderDtos
{
    public ParentServer? ParentServer { get; set; }
    public Service? Service { get; set; }
    public ServiceDetails? ServiceDetails { get; set; }
    public RegistryCredential? RegistryCredential { get; set; }
}

public class BuildFilter
{
    [JsonProperty("paths")]
    public List<string> Paths { get; set; } = [];

    [JsonProperty("ignoredPaths")]
    public List<string> IgnoredPaths { get; set; } = [];
}

public class IpAllowList
{
    [JsonProperty("cidrBlock")]
    public string CidrBlock = string.Empty;

    [JsonProperty("description")]
    public string Description = string.Empty;
}

public class ParentServer
{
    [JsonProperty("id")]
    public string Id = string.Empty;

    [JsonProperty("name")]
    public string Name = string.Empty;
}

public class Previews
{
    [JsonProperty("generation")]
    public string Generation = string.Empty;
}

public class RegistryCredential
{
    [JsonProperty("id")]
    public string Id = string.Empty;

    [JsonProperty("name")]
    public string Name = string.Empty;
}

public class Root
{
    [JsonProperty("service")]
    public Service? Service;

    [JsonProperty("cursor")]
    public string Cursor = string.Empty;
}

public class Service
{
    [JsonProperty("id")]
    public string Id { get; set; } =  string.Empty;

    [JsonProperty("autoDeploy")]
    public string AutoDeploy { get; set; } =  string.Empty;

    [JsonProperty("branch")]
    public string Branch { get; set; } =  string.Empty;

    [JsonProperty("buildFilter")]
    public BuildFilter? BuildFilter;

    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("dashboardUrl")]
    public string DashboardUrl { get; set; } = string.Empty;

    [JsonProperty("environmentId")]
    public string EnvironmentId { get; set; } = string.Empty;

    [JsonProperty("imagePath")]
    public string ImagePath = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("notifyOnFail")]
    public string NotifyOnFail { get; set; } = string.Empty;

    [JsonProperty("ownerId")]
    public string OwnerId { get; set; } = string.Empty;

    [JsonProperty("registryCredential")]
    public RegistryCredential? RegistryCredential;

    [JsonProperty("repo")]
    public string Repo { get; set; } = string.Empty;

    [JsonProperty("rootDir")]
    public string RootDir = string.Empty;

    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonProperty("suspended")]
    public string Suspended { get; set; } = string.Empty;

    [JsonProperty("suspenders")]
    public List<string> Suspenders = [];

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonProperty("serviceDetails")]
    public ServiceDetails? ServiceDetails { get; set; }
}

public class ServiceDetails
{
    [JsonProperty("buildCommand")]
    public string BuildCommand { get; set; } = string.Empty;

    [JsonProperty("ipAllowList")]
    public List<IpAllowList> IpAllowList = [];

    [JsonProperty("parentServer")]
    public ParentServer? ParentServer;

    [JsonProperty("publishPath")]
    public string PublishPath = string.Empty;

    [JsonProperty("previews")]
    public Previews? Previews;

    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    [JsonProperty("buildPlan")]
    public string BuildPlan = string.Empty;

    [JsonProperty("renderSubdomainPolicy")]
    public string RenderSubdomainPolicy = string.Empty;
}
