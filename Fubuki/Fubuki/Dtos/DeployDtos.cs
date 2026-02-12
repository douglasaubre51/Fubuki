using Newtonsoft.Json;

namespace Fubuki.Dtos;

public class DeployDtos
{
    public Deploy? Deploy { get; set; }
    public bool DidBuildFail { get; set; }
    public bool IsDeactivated { get; set; }
    public bool IsCanceled { get; set; }
}


public class Deploy
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("commit")]
    public Commit? Commit { get; set; }

    [JsonProperty("image")]
    public Image? Image;

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("trigger")]
    public string Trigger { get; set; } = string.Empty;

    [JsonProperty("startedAt")]
    public DateTime? StartedAt { get; set; }

    [JsonProperty("finishedAt")]
    public DateTime? FinishedAt { get; set; }

    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
}
public class Commit
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }
}

public class Image
{
    [JsonProperty("ref")]
    public string Ref;

    [JsonProperty("sha")]
    public string Sha;

    [JsonProperty("registryCredential")]
    public string RegistryCredential;
}
