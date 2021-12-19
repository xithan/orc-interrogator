using System.Net.Mime;
using Newtonsoft.Json;

namespace Interrogator.Game.Analysis;

public struct Edge
{
    public Edge(int source, int target, int value)
    {
        this.Source = source;
        this.Target = target;
        this.Value = value;
    }

    [JsonProperty("source")] public int Source { get; set; }

    [JsonProperty("target")] public int Target { get; set; }

    [JsonProperty("value")] public int Value { get; set; }
}
